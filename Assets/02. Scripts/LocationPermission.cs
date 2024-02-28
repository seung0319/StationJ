using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// MapScreen씬용 위치정보 클래스(Strat가 필요하기 때문에 스크립트 2개로 분리)
/// </summary>
public class LocationPermission : MonoBehaviour
{
    public GameObject markerPanel;
    public GameObject routeFindPanel;
    public GameObject categoryPanel;
    public GameObject infoPanel;
    public GameObject routeManager;
    public GameObject locationUpdater;
    public GameObject playerMarker;
    public GameObject locationPanel;


    // Start is called before the first frame update
    void Start()
    {
        // MapScreen 씬 시작 시 위치요청
        RequestPermission();
    }

    //비동기로 사용자에게 위치권한을 요청해 그값을 리턴받아 값에 따른 코드를 실행하는 함수
    public async void RequestPermission()
    {
        //권한 요청코드
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.ACCESS_FINE_LOCATION");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            //허용했을때
            LocationInfoAllow();
        }
        else
        {
            //허용하지 않았을때
            LocationInfoNotAllow();
        }
    }
    void LocationInfoAllow()
    {
        // 허용됐을 시 액션
        locationUpdater.SetActive(true);
        playerMarker.SetActive(true);
    }
    void LocationInfoNotAllow()
    {
        // 거부 시 아무것도 안함
    }

    //길안내 버튼을 누른후 허용하지 않음을 눌러도 되는 처음권한요청과 달리 허용하지 않음이면 기능이 작동하지 않아야 하는
    //함수기 때문에 분리
    public void RouteButtonClick()
    {
        RequestPermission2();
    }
    public async void RequestPermission2()
    {
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.ACCESS_FINE_LOCATION");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            //허용했을때 다음 스텝으로 이동
            locationUpdater.SetActive(true);
            playerMarker.SetActive(true);
            markerPanel.SetActive(false);
            categoryPanel.SetActive(false);
            infoPanel.SetActive(false);
            routeManager.SetActive(true);
            routeFindPanel.SetActive(true);
        }
        else
        {
            //허용하지 않았을때 사용자에게 권한을 수동으로 켜야한다는 팝업(사용자가 한번 거부 누르면 다시 안물어봄)
            //팝업에 설정에서 뭘 켜야 하며 설정으로 이동버튼 존재
            locationPanel.SetActive(true);
        }
    }

    public void BackButtonCheck()
    {
        if (AndroidRuntimePermissions.CheckPermission("android.permission.ACCESS_FINE_LOCATION"))
        {
            locationPanel.SetActive(false);
            locationUpdater.SetActive(true);
            playerMarker.SetActive(true);
        }
    }
}
