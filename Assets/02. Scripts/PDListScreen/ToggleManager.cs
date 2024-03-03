using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PDListScreen 씬의 위쪽에 포토존과 도슨트 탭을 구현하기 위한 클래스
/// </summary>
public class ToggleManager : MonoBehaviour
{
    private Toggle toggle;
    public Sprite selectedSprite;
    public Sprite originalSprite;

    void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });
        //if (toggle.gameObject.name == "ShowAllBtn")
        //    toggle.isOn = true;
    }

    // 토글의 상태에 따라 sprite 변경
    void ToggleValueChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {
            // Toggle이 켜져 있을 때 Sprite 변경
            toggle.targetGraphic.GetComponent<Image>().sprite = selectedSprite;
        }
        else
        {
            // Toggle이 꺼져 있을 때 원래 Sprite로 변경(원래 Sprite를 저장해두어야 합니다)
            toggle.targetGraphic.GetComponent<Image>().sprite = originalSprite;
        }
    }
}
