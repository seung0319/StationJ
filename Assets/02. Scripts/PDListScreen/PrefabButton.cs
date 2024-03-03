using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 포토존/도슨트 프리팹의 버튼 컴포넌트에 들어가는 클래스
/// 목적지의 위도 경도를 초기화하거나
/// 상세설명창의 데이터를 업데이트 시키는 클래스
/// </summary>
public class PrefabButton : MonoBehaviour
{
    public POIData poiData;
    public InfoPanelManager panel;

    private void Start()
    {
        panel = FindObjectOfType<InfoPanelManager>();
    }

    // 포토존/도슨트 프리팹을 누르면 프로그램의 목적지가 설정되고, 상세설명창의 데이터를 업데이트한다.
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
        if(poiData.poi.type == "도슨트")
            PDListStateSaver.docentOn = true;
    }
}
