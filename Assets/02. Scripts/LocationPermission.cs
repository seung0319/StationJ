using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class LocationPermission : MonoBehaviour
{
    public GameObject locationPanel;
    public GameObject markerPanel;
    public GameObject routeFindPanel;
    public GameObject infoPanel;
    private int order = 0;

    private bool locationOK;


    public Text debugger;
    public Text debugger2;

    // Start is called before the first frame update
    void Start()
    {
        locationOK = Permission.HasUserAuthorizedPermission(Permission.FineLocation);
        debugger.text = locationOK.ToString();
        if (!locationOK)
        {
            locationPanel.SetActive(true);
            markerPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        debugger2.text = locationOK.ToString();
    }

    public void LocationInfoAllow(bool Allow)
    {
        if (order == 0)  //ù��° �˾�â�� ���
        {
            if (Allow)  //��ġ���ѿ�û ���
            {
                debugger.text = "allowed";
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
                locationOK = true;
                order++;
            }
            else        //��ġ���ѿ�û ��� ����
            {
                debugger.text = "denied";
                order++;
            }
            markerPanel.SetActive(true);
        }
        else if (order == 1)  //�ι�° �˾�â�� ���
        {
            if (Allow)  //��ġ���ѿ�û ���
            {
                debugger.text = "allowed";
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
                locationOK = true;
                routeFindPanel.SetActive(true);
            }
            else        //��ġ���ѿ�û ��� ����
            {
                infoPanel.SetActive(true);
            }
        }
    }
    public void RouteFind()
    {
        Debug.Log(locationOK.ToString());
        if (!locationOK)
        {
            locationPanel.SetActive(true);
        }
        else
        {
            routeFindPanel.SetActive(true);
        }
    }
}
