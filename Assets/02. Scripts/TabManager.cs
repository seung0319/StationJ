using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject[] panels; // 이 배열에 각각의 패널을 할당합니다.
    public Text[] texts;
    private Color selectedColor = new Color32(54, 148, 215, 255); // 선택된 버튼의 색
    private Color unselectedColor = new Color32(185, 189, 189, 255); // 선택되지 않은 버튼의 색

    public void ShowPanel(int panelIndex) // 이 함수는 각 탭의 OnClick 이벤트에 연결합니다.
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == panelIndex)
            {
                panels[i].SetActive(true);
                texts[i].color = selectedColor;
            }
            else
            {
                panels[i].SetActive(false);
                texts[i].color = unselectedColor;
            }
        }
        texts[3].color = unselectedColor;
    }
    public void ShowAllPanels() // 이 함수는 모든 패널을 활성화하는 버튼의 OnClick 이벤트에 연결합니다.
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(true);
            texts[i].color = unselectedColor;
        }
        texts[3].color = selectedColor;
    }
}
