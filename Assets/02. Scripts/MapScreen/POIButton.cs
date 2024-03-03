using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Ŀ�� ��ư ������Ʈ�� ���� Ŭ����
/// �������� ���� �浵�� �ʱ�ȭ�ϰų�
/// �󼼼���â�� �����͸� ������Ʈ ��Ű�� Ŭ����
/// </summary>
public class POIButton : MonoBehaviour
{
    public POIData poiData;
    private POIInfoPanelManager displayPOI;
    public GameObject selectedMarker;
    POIData sourceComponent;
    public GameObject directionManager;

    public Text debugger;

    // ��Ŀ�� �����Ǹ� ��Ŀ�� ���� ��Ŀ�� DirectionManager.cs �� ����Ǵ� directionManager ������Ʈ�� �ʱ�ȭ ��Ų��.
    private void Start()
    {
        displayPOI = FindObjectOfType<POIInfoPanelManager>();

        sourceComponent = gameObject.GetComponent<POIData>();
        selectedMarker = GameObject.Find("Selected").transform.Find("selectedMarker").gameObject;
        directionManager = GameObject.Find("DirectionManagerP").transform.Find("DirectionManager").gameObject;
    }

    // ��Ŀ�� ������ ���α׷��� �������� �����ǰ�, �󼼼���â�� �����͸� ������Ʈ�ϰ�, DirectionManager.cs�� Ȱ��ȭ ��Ų��.
    public void OnClick()
    {
        MarkerSelected();
        displayPOI.SetPanel(poiData.GetData());
        directionManager.SetActive(true);
    }
    void MarkerSelected()
    {
        DataManager.instance.selectedPoi = poiData.poi;
        DirectionManager.destLatitude = poiData.poi.latitude.ToString();
        DirectionManager.destLongitude = poiData.poi.longitude.ToString();
        selectedMarker.transform.position = sourceComponent.transform.position;
    }
}