using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwitchPanel : MonoBehaviour
{
    public GameObject[] panels;

    public void Switch(int panelIndex) // 이 함수는 각 탭의 OnClick 이벤트에 연결합니다.
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == panelIndex)
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }
}
