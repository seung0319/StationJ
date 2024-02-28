using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 포토존,도슨트존,AR카메라용 권한요청 클래스
/// </summary>
public class PermissionRequester : MonoBehaviour
{
    [SerializeField] GameObject locationPanel;
    [SerializeField] GameObject cameraPanel;

    public void CamaraUseAllow(string NextScene)
    {
        // 카메라/도슨트, 카메라, 저장소 요청
        RequestPermission(NextScene);
    }

    public void CamaraUseAllow2(string NextScene)
    {
        // 카메라/도슨트, 위치 요청
        RequestPermission2(NextScene);
    }
    public void LocationUseAllow(string NextScene)
    {
        RequestPermission2(NextScene);
    }

    public void CamaraUseAllow3(string NextScene)
    {
        // AR네비게이션, 카메라 요청
        RequestPermission3(NextScene);
    }

    //AR 카메라(사진기능까지 포함)사용을 위한 권한요청함수
    public async void RequestPermission(string NextScene)
    {
        //카메라 권한 요청
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.CAMERA");
        if (result == AndroidRuntimePermissions.Permission.Denied)
        {
            //거부 눌렀을시 권한 없이 사용이 불가능해 권한을 수동으로 켜야한다는 팝업 생성
            //안내사진과 설정으로이동 버튼 존재
            cameraPanel.SetActive(true);
        }
        //저장소 권한 요청
        AndroidRuntimePermissions.Permission result2 = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.WRITE_EXTERNAL_STORAGE");
        if (result2 == AndroidRuntimePermissions.Permission.Denied)
        {
            cameraPanel.SetActive(true);
        }
        //둘다 허용된 상태일때 AR카메라씬 이동
        if (result == AndroidRuntimePermissions.Permission.Granted && result2 == AndroidRuntimePermissions.Permission.Granted)
        {
            SceneManager.LoadScene(NextScene);
        }
    }
    
    //포토존,도슨트 위치보기를 눌렀을때 권한을 검사하는 함수
    public async void RequestPermission2(string NextScene)
    {
        //위치정보 권한 요청
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.ACCESS_FINE_LOCATION");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            //허용시 씬 이동
            SceneManager.LoadScene(NextScene);
        }
        else
        {
            //허용 안함시 안내팝업
            locationPanel.SetActive(true);
        }
    }

    //MapScreen씬에서 AR카메라 만을 사용하기 위한 권한검사 함수
    public async void RequestPermission3(string NextScene)
    {
        //카메라 권한 검사
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.CAMERA");
        if (result == AndroidRuntimePermissions.Permission.Denied)
        {
            cameraPanel.SetActive(true);
        }
        else if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            SceneManager.LoadScene(NextScene);
        }
    }
}
