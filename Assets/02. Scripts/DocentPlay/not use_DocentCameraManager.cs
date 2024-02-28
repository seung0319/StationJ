using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using static UnityEngine.UI.Image;

/// <summary>
/// 도슨트 플레이 씬에서 p&d씬으로 넘어갔다가 다시 도슨트 씬으로 돌아오면
/// ar배경(임시 작업용) 및 카메라가 파괴된다는 오류가 떠서 수정하기 위해 작업한 코드
/// -> 모바일에서는 정상작동 되어 사용하지 않음
/// </summary>
public class DocentCameraManager : MonoBehaviour
{
    /*public XROrigin origin;
    public ARSession session;
    //private SerializedObject serializedObject;

    private void Awake()
    {
        //초기화
        origin.gameObject.SetActive(false);
        session.gameObject.SetActive(false);

        // serializedObject가 유효하지 않은 경우 다시 초기화합니다.
        //if (serializedObject == null || serializedObject.targetObject == null)
        //{
        //    serializedObject = new SerializedObject(this);
        //}

    }
    private void Start()
    {
        //시작시 활성화
        origin.gameObject.SetActive(true);
        session.gameObject.SetActive(true);


        //DontDestroyOnLoad를 호출하여 오브젝트가 씬 전환 시에 파괴되지 않도록 설정합니다.
        DontDestroyOnLoad(origin.gameObject);
        DontDestroyOnLoad(session.gameObject);
    }
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // XR Origin을 비활성화합니다.
        if (origin != null)
        {
            origin.gameObject.SetActive(false);
        }
    }*/

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

    private static DocentCameraManager instance; // 싱글톤 인스턴스

    private void Awake()
    {
        // 인스턴스가 이미 존재하는 경우 현재 게임 오브젝트를 파괴합니다.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 인스턴스가 없는 경우 현재 게임 오브젝트를 싱글톤으로 설정합니다.
        instance = this;

        // 초기화
        XROrigin origin = GetComponent<XROrigin>();
        ARSession session = GetComponent<ARSession>();
        origin.gameObject.SetActive(false);
        session.gameObject.SetActive(false);

        // 시작 시 활성화
        origin.gameObject.SetActive(true);
        session.gameObject.SetActive(true);
    }

    private void Start()
    {
        // 씬 전환 시에 파괴되지 않도록 설정합니다.
        DontDestroyOnLoad(gameObject);
    }
}
