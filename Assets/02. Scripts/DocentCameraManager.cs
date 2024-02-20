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
        //시작할때 오프상태
        //gameObject.SetActive(false);
        //session.gameObject.SetActive(false);

        //DontDestroyOnLoad를 호출하여 오브젝트가 씬 전환 시에 파괴되지 않도록 설정합니다.
        DontDestroyOnLoad(origin.gameObject);
        DontDestroyOnLoad(session.gameObject);

        // serializedObject가 유효하지 않은 경우 다시 초기화합니다.
        //if (serializedObject == null || serializedObject.targetObject == null)
        //{
        //    serializedObject = new SerializedObject(this);
        //}

    }
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // XR Origin을 비활성화합니다.
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
        // 씬이 변경될 때마다 XR Origin과 AR Session을 생성하거나 가져옵니다.
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

        // XR Origin 비활성화
        currentOrigin.gameObject.SetActive(false);
    }

    public static DocentCameraManager GetInstance()
    {
        return instance;
    }*/
}
