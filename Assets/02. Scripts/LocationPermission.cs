using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// MapScreen���� ��ġ���� Ŭ����(Strat�� �ʿ��ϱ� ������ ��ũ��Ʈ 2���� �и�)
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
        // MapScreen �� ���� �� ��ġ��û
        RequestPermission();
    }

    //�񵿱�� ����ڿ��� ��ġ������ ��û�� �װ��� ���Ϲ޾� ���� ���� �ڵ带 �����ϴ� �Լ�
    public async void RequestPermission()
    {
        //���� ��û�ڵ�
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.ACCESS_FINE_LOCATION");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            //���������
            LocationInfoAllow();
        }
        else
        {
            //������� �ʾ�����
            LocationInfoNotAllow();
        }
    }
    void LocationInfoAllow()
    {
        // ������ �� �׼�
        locationUpdater.SetActive(true);
        playerMarker.SetActive(true);
    }
    void LocationInfoNotAllow()
    {
        // �ź� �� �ƹ��͵� ����
    }

    //��ȳ� ��ư�� ������ ������� ������ ������ �Ǵ� ó�����ѿ�û�� �޸� ������� �����̸� ����� �۵����� �ʾƾ� �ϴ�
    //�Լ��� ������ �и�
    public void RouteButtonClick()
    {
        RequestPermission2();
    }
    public async void RequestPermission2()
    {
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.ACCESS_FINE_LOCATION");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            //��������� ���� �������� �̵�
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
            //������� �ʾ����� ����ڿ��� ������ �������� �Ѿ��Ѵٴ� �˾�(����ڰ� �ѹ� �ź� ������ �ٽ� �ȹ��)
            //�˾��� �������� �� �Ѿ� �ϸ� �������� �̵���ư ����
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
