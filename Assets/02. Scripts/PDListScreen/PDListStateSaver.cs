using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ���������� ī�޶�,��ġ���� �̵��� Ż��� �������� ���¸� �����ϰ� �����۽� �ʱ�ȭ
/// </summary>
public class PDListStateSaver : MonoBehaviour
{
    public static POI statesaverPOI;
    public static bool docentOn;

    public Toggle docentToggle;

    public InfoPanelManager infoPanelManager;

    private void Start()
    {
        //�������� ó���� �ƴ��� �˻�
        if (statesaverPOI == null)
            return;

        docentToggle.isOn = docentOn;
        infoPanelManager.SetPanel(statesaverPOI);
    }

    public void PoiNull()
    {
        docentOn = false;
        statesaverPOI = null;
    }
}
