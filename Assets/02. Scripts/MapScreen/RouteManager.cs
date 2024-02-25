using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public GameObject directionManager;

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
    }
}
