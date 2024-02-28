using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ������,����Ʈ��,ARī�޶�� ���ѿ�û Ŭ����
/// </summary>
public class PermissionRequester : MonoBehaviour
{
    [SerializeField] GameObject locationPanel;
    [SerializeField] GameObject cameraPanel;

    public void CamaraUseAllow(string NextScene)
    {
        // ī�޶�/����Ʈ, ī�޶�, ����� ��û
        RequestPermission(NextScene);
    }

    public void CamaraUseAllow2(string NextScene)
    {
        // ī�޶�/����Ʈ, ��ġ ��û
        RequestPermission2(NextScene);
    }
    public void LocationUseAllow(string NextScene)
    {
        RequestPermission2(NextScene);
    }

    public void CamaraUseAllow3(string NextScene)
    {
        // AR�׺���̼�, ī�޶� ��û
        RequestPermission3(NextScene);
    }

    //AR ī�޶�(������ɱ��� ����)����� ���� ���ѿ�û�Լ�
    public async void RequestPermission(string NextScene)
    {
        //ī�޶� ���� ��û
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.CAMERA");
        if (result == AndroidRuntimePermissions.Permission.Denied)
        {
            //�ź� �������� ���� ���� ����� �Ұ����� ������ �������� �Ѿ��Ѵٴ� �˾� ����
            //�ȳ������� ���������̵� ��ư ����
            cameraPanel.SetActive(true);
        }
        //����� ���� ��û
        AndroidRuntimePermissions.Permission result2 = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.WRITE_EXTERNAL_STORAGE");
        if (result2 == AndroidRuntimePermissions.Permission.Denied)
        {
            cameraPanel.SetActive(true);
        }
        //�Ѵ� ���� �����϶� ARī�޶�� �̵�
        if (result == AndroidRuntimePermissions.Permission.Granted && result2 == AndroidRuntimePermissions.Permission.Granted)
        {
            SceneManager.LoadScene(NextScene);
        }
    }
    
    //������,����Ʈ ��ġ���⸦ �������� ������ �˻��ϴ� �Լ�
    public async void RequestPermission2(string NextScene)
    {
        //��ġ���� ���� ��û
        AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.ACCESS_FINE_LOCATION");
        if (result == AndroidRuntimePermissions.Permission.Granted)
        {
            //���� �� �̵�
            SceneManager.LoadScene(NextScene);
        }
        else
        {
            //��� ���Խ� �ȳ��˾�
            locationPanel.SetActive(true);
        }
    }

    //MapScreen������ ARī�޶� ���� ����ϱ� ���� ���Ѱ˻� �Լ�
    public async void RequestPermission3(string NextScene)
    {
        //ī�޶� ���� �˻�
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
