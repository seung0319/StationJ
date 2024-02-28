using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PDListStateSaver : MonoBehaviour
{
    public static POI statesaverPOI;
    public static bool docentOn;

    public Toggle docentToggle;

    public InfoPanelManager infoPanelManager;

    private void Start()
    {
        if (statesaverPOI == null)
            return;

        docentToggle.isOn = docentOn;
        docentOn = false;

        infoPanelManager.SetPanel(statesaverPOI);
        statesaverPOI = null;
    }
}
