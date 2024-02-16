using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject[] panels; // �� �迭�� ������ �г��� �Ҵ��մϴ�.

    // ī�װ� ��ư�鿡 ���� ����
    public Toggle[] categoryButtons;

    private Color selectedColor = new Color32(0, 148, 255, 255); // ���õ� ��ư�� ��
    private Color unselectedColor = new Color32(185, 188, 190, 255); // ���õ��� ���� ��ư�� ��


    public void ShowPanel(int panelIndex) // �� �Լ��� �� ���� OnClick �̺�Ʈ�� �����մϴ�.
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == panelIndex)
            {
                panels[i].SetActive(true);
                categoryButtons[i].GetComponentInChildren<Text>().color = selectedColor;
            }
            else
            {
                panels[i].SetActive(false);
                categoryButtons[i].GetComponentInChildren<Text>().color = unselectedColor;
            }
        }
        categoryButtons[3].GetComponentInChildren<Text>().color = unselectedColor;
    }
    public void ShowAllPanels() // �� �Լ��� ��� �г��� Ȱ��ȭ�ϴ� ��ư�� OnClick �̺�Ʈ�� �����մϴ�.
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(true);
            categoryButtons[i].GetComponentInChildren<Text>().color = unselectedColor;
        }
        categoryButtons[3].GetComponentInChildren<Text>().color = selectedColor;
    }
}
