using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelManager : MonoBehaviour
{
    public GameObject Panel; // �ǳ��� public GameObject�� �����մϴ�.

    private void Start()
    {
        if (gameObject.name == "DocentContentImage")
            Panel = GameObject.Find("PN_Docent Explain Panel");
        else
            Panel = GameObject.Find("PN_Potozone Explain Panel");
    }
    // ��ư Ŭ�� �̺�Ʈ�� �����ϴ� �Լ��Դϴ�.
    public void OnButtonClick()
    {
        // �ǳ��� ���¸� ����մϴ�.
        Panel.SetActive(!Panel.activeSelf);
    }
}
