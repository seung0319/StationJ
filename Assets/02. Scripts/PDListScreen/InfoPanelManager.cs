using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelManager : MonoBehaviour
{
    public GameObject Panel; // 판넬을 public GameObject로 참조합니다.

    private void Start()
    {
        if (gameObject.name == "DocentContentImage")
            Panel = GameObject.Find("PN_Docent Explain Panel");
        else
            Panel = GameObject.Find("PN_Potozone Explain Panel");
    }
    // 버튼 클릭 이벤트에 대응하는 함수입니다.
    public void OnButtonClick()
    {
        // 판넬의 상태를 토글합니다.
        Panel.SetActive(!Panel.activeSelf);
    }
}
