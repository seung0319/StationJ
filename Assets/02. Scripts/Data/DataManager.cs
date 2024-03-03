using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// POI 정보, 길찾기 시 경로 등 데이터를 관리 하는 클래스
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

        //POI데이터를 로드 하기 위한 코루틴 실행
        StartCoroutine(LoadData());
    }

    // POI데이터를 로드하기 위한 코루틴
    IEnumerator LoadData()
    {
        //Resource폴더에서 "POIInfo"라는 파일의 텍스트 에셋을 로드
        var request = Resources.LoadAsync<TextAsset>("POIInfo");
        //로드완료를 반환할때까지 대기
        yield return request;

        if (request.asset != null)
        {
            //request.asset을 TextAsset으로 캐스팅 할수 있다면 jsonFile에 초기화
            TextAsset jsonFile = request.asset as TextAsset;
            //제이슨 파일을 파싱하여 poiList에 초기화
            poiList = JsonUtility.FromJson<POIList>(jsonFile.text);

            //초기화 완료까지 대기
            yield return new WaitUntil(() => poiList != null);

            //완료후 혹시 모를상황에 대비 0.5초 딜레이
            yield return new WaitForSeconds(0.5f);

            //로딩완료 판정으로 메인씬(HomeScreen) 이동
            SceneManager.LoadScene("HomeScreen");
        }
    }

    // Naver Driving 5 API에서 값을 받아오기 위한 함수
    // 경로, 길, 걸리는 시간이 각 변수에 반환됨
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
            paths[i] = (jsonPaths[i][1].ToObject<double>(), jsonPaths[i][0].ToObject<double>()); // API에서는 longitude, latitude 순서이지만 튜플에는 latitude, longitude 순서로 저장합니다.
        }
        paths[jsonPaths.Count] = (jsonGoal[1].ToObject<double>(), jsonGoal[0].ToObject<double>()); // 마지막에 jsonGoal의 좌표를 추가합니다.
        distance = jsonDistance;
        duration = jsonDuration;
    }

    // 위치서비스가 실행중인지 확인하는 코루틴을 실행시키는 함수
    public void LocationInfoGetStart()
    {
        if (isStartCoroutine)
            return;

        isStartCoroutine = true;
        StartCoroutine(LocationInfoGet());
    }

    // 위치서비스가 실행중인지 확인하는 코루틴
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

        // 시간 초과
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }
        // 위치 서비스 시작 실패
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
    }
}