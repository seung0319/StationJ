using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// POI ����, ��ã�� �� ��� �� �����͸� ���� �ϴ� Ŭ����
/// </summary>
public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    public POIList poiList;
    public POI selectedPoi;
    public (double latitude, double longitude)[] paths;
    public int distance;
    public int duration;

    bool isStartCoroutine = false;
    public bool fromPhodocent = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        //POI�����͸� �ε� �ϱ� ���� �ڷ�ƾ ����
        StartCoroutine(LoadData());
    }

    // POI�����͸� �ε��ϱ� ���� �ڷ�ƾ
    IEnumerator LoadData()
    {
        //Resource�������� "POIInfo"��� ������ �ؽ�Ʈ ������ �ε�
        var request = Resources.LoadAsync<TextAsset>("POIInfo");
        //�ε�ϷḦ ��ȯ�Ҷ����� ���
        yield return request;

        if (request.asset != null)
        {
            //request.asset�� TextAsset���� ĳ���� �Ҽ� �ִٸ� jsonFile�� �ʱ�ȭ
            TextAsset jsonFile = request.asset as TextAsset;
            //���̽� ������ �Ľ��Ͽ� poiList�� �ʱ�ȭ
            poiList = JsonUtility.FromJson<POIList>(jsonFile.text);

            //�ʱ�ȭ �Ϸ���� ���
            yield return new WaitUntil(() => poiList != null);

            //�Ϸ��� Ȥ�� �𸦻�Ȳ�� ��� 0.5�� ������
            yield return new WaitForSeconds(0.5f);

            //�ε��Ϸ� �������� ���ξ�(HomeScreen) �̵�
            SceneManager.LoadScene("HomeScreen");
        }
    }

    // Naver Driving 5 API���� ���� �޾ƿ��� ���� �Լ�
    // ���, ��, �ɸ��� �ð��� �� ������ ��ȯ��
    public void ParseJson(string jsonString)
    {
        JObject data = JObject.Parse(jsonString);
        JArray jsonPaths = (JArray)data["route"]["traoptimal"][0]["path"];
        JArray jsonGoal = (JArray)data["route"]["traoptimal"][0]["summary"]["goal"]["location"];
        int jsonDistance = data["route"]["traoptimal"][0]["summary"]["distance"].ToObject<int>();
        int jsonDuration = data["route"]["traoptimal"][0]["summary"]["duration"].ToObject<int>();

        paths = new (double, double)[jsonPaths.Count + 1];

        for (int i = 0; i < jsonPaths.Count; i++)
        {
            paths[i] = (jsonPaths[i][1].ToObject<double>(), jsonPaths[i][0].ToObject<double>()); // API������ longitude, latitude ���������� Ʃ�ÿ��� latitude, longitude ������ �����մϴ�.
        }
        paths[jsonPaths.Count] = (jsonGoal[1].ToObject<double>(), jsonGoal[0].ToObject<double>()); // �������� jsonGoal�� ��ǥ�� �߰��մϴ�.
        distance = jsonDistance;
        duration = jsonDuration;
    }

    // ��ġ���񽺰� ���������� Ȯ���ϴ� �ڷ�ƾ�� �����Ű�� �Լ�
    public void LocationInfoGetStart()
    {
        if (isStartCoroutine)
            return;

        isStartCoroutine = true;
        StartCoroutine(LocationInfoGet());
    }

    // ��ġ���񽺰� ���������� Ȯ���ϴ� �ڷ�ƾ
    IEnumerator LocationInfoGet()
    {
        Input.location.Start(1, 1);

        YieldInstruction delay = new WaitForSeconds(1);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return delay;
            maxWait--;
        }

        // �ð� �ʰ�
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }
        // ��ġ ���� ���� ����
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
    }
}