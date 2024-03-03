using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ������/����Ʈ �������� ��ư ������Ʈ�� ���� Ŭ����
/// �������� ���� �浵�� �ʱ�ȭ�ϰų�
/// �󼼼���â�� �����͸� ������Ʈ ��Ű�� Ŭ����
/// </summary>
public class PrefabButton : MonoBehaviour
{
    public POIData poiData;
    public InfoPanelManager panel;

    private void Start()
    {
        panel = FindObjectOfType<InfoPanelManager>();
    }

    // ������/����Ʈ �������� ������ ���α׷��� �������� �����ǰ�, �󼼼���â�� �����͸� ������Ʈ�Ѵ�.
    public void OnClick()
    {
        panel.SetPanel(poiData.GetData());
        ButtonSelected();

    }

    void ButtonSelected()
    {
        DataManager.instance.selectedPoi = poiData.poi;
        DirectionManager.destLatitude = poiData.poi.latitude.ToString();
        DirectionManager.destLongitude = poiData.poi.longitude.ToString();
        PDListStateSaver.statesaverPOI = poiData.poi;
        if(poiData.poi.type == "����Ʈ")
            PDListStateSaver.docentOn = true;
    }
}
