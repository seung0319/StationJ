using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouteManager : MonoBehaviour
{
    public GameObject directionManager;

    public Text debugger;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnRouteFindButtonClick()
    {
        GameObject.Find("Selected").transform.Find("selectedMarker").gameObject.SetActive(true);
        //directionManager.SetActive(true);
        DataManager.instance.LoadPath();
        //debugger.text = "Read";
    }
}
