using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwitchPanel : MonoBehaviour
{
    public GameObject[] panels;

    public void Switch(int panelIndex) // �� �Լ��� �� ���� OnClick �̺�Ʈ�� �����մϴ�.
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
