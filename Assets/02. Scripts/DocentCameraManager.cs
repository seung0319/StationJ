using Unity.XR.CoreUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using static UnityEngine.UI.Image;

public class DocentCameraManager : MonoBehaviour
{
   public XROrigin origin;
    public ARSession session;
    //private SerializedObject serializedObject;

    private void Awake()
    {
        //�����Ҷ� ��������
        //gameObject.SetActive(false);
        //session.gameObject.SetActive(false);

        //DontDestroyOnLoad�� ȣ���Ͽ� ������Ʈ�� �� ��ȯ �ÿ� �ı����� �ʵ��� �����մϴ�.
        DontDestroyOnLoad(origin.gameObject);
        DontDestroyOnLoad(session.gameObject);

        // serializedObject�� ��ȿ���� ���� ��� �ٽ� �ʱ�ȭ�մϴ�.
        //if (serializedObject == null || serializedObject.targetObject == null)
        //{
        //    serializedObject = new SerializedObject(this);
        //}

    }
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // XR Origin�� ��Ȱ��ȭ�մϴ�.
        if (origin != null)
        {
            origin.gameObject.SetActive(false);
        }
    }

    /*private static DocentCameraManager instance;
    public XROrigin originPrefab;
    public ARSession sessionPrefab;
    private XROrigin currentOrigin;
    private ARSession currentSession;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(originPrefab.gameObject);
        DontDestroyOnLoad(sessionPrefab.gameObject);
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // ���� ����� ������ XR Origin�� AR Session�� �����ϰų� �����ɴϴ�.
        if (currentOrigin == null)
        {
            currentOrigin = Instantiate(originPrefab);
            DontDestroyOnLoad(currentOrigin.gameObject);
        }

        if (currentSession == null)
        {
            currentSession = Instantiate(sessionPrefab);
            DontDestroyOnLoad(currentSession.gameObject);
        }

        // XR Origin ��Ȱ��ȭ
        currentOrigin.gameObject.SetActive(false);
    }

    public static DocentCameraManager GetInstance()
    {
        return instance;
    }*/
}
