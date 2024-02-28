using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (toggle.gameObject.name == "ShowAllBtn")
            toggle.isOn = true;
    }

    void ToggleValueChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {
            // Toggle�� ���� ���� �� Sprite ����
            toggle.targetGraphic.GetComponent<Image>().sprite = selectedSprite;
        }
        else
        {
            // Toggle�� ���� ���� �� ���� Sprite�� ����(���� Sprite�� �����صξ�� �մϴ�)
            toggle.targetGraphic.GetComponent<Image>().sprite = originalSprite;
        }
    }
}
