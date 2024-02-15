using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public GameObject[] panels;

    int panelsOnIndex = -1;

    private void Start()
    {
        if(!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            panels[0].SetActive(true);
            panelsOnIndex = 0;
        }
        else
        {
            panels[1].SetActive(true);
            panelsOnIndex = 1;
        }
    }

    public void SwichPanel(int NextPanelIndex)
    {
        if(panelsOnIndex != -1)
            panels[panelsOnIndex].SetActive(false);

        panels[NextPanelIndex].SetActive(true);
        panelsOnIndex = NextPanelIndex;
    }

    public void LocationInfoAllow(bool Allow)
    {
        
    }
}
