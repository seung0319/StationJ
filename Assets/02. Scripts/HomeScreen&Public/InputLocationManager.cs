using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ġ �����Ͱ� �޾����� �ִ��� Ȯ���ϴ� Ŭ����
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
