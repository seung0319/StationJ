using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 위치 데이터가 받아지고 있는지 확인하는 클래스
/// </summary>
public class InputLocationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DataManager.instance.LocationInfoGetStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
