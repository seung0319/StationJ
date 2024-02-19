using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabButton : MonoBehaviour
{
    public bool controlsPanel1; // 이 버튼이 판넬1을 제어하는지 여부

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(TogglePanel);
    }

    private void TogglePanel()
    {
        if (controlsPanel1)
        {
            PanelManager.Instance.TogglePanel1();
        }
        else
        {
            PanelManager.Instance.TogglePanel2();
        }
    }
}
