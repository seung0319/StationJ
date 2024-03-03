using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 포토존에서 카메라,위치보기 이동후 탈출시 포토존의 상태를 저장하고 씬시작시 초기화
/// </summary>
public class PDListStateSaver : MonoBehaviour
{
    public static POI statesaverPOI;
    public static bool docentOn;

    public Toggle docentToggle;

    public InfoPanelManager infoPanelManager;

    private void Start()
    {
        //포토존이 처음이 아닌지 검사
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
