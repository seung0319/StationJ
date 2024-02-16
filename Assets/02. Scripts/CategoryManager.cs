using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryManager : MonoBehaviour
{
    public List<Toggle> toggles = new List<Toggle>();
    public List<GameObject> panels = new List<GameObject>();
    public List<Sprite> normalSprites = new List<Sprite>();
    public List<Sprite> selectedSprites = new List<Sprite>();

    public Toggle allOnToggle; // 모든 패널을 켜는 토글
    public Sprite allNormalSprite;
    public Sprite allSelectedSprite;

    private Color selectedColor = new Color32(0, 148, 255, 255); // 선택된 버튼의 색
    private Color unselectedColor = new Color32(185, 188, 190, 255); // 선택되지 않은 버튼의 색

    void Start()
    {
        Dictionary<Toggle, GameObject> togglePanels = new Dictionary<Toggle, GameObject>();

        for (int i = 0; i < Mathf.Min(toggles.Count, panels.Count); i++)
        {
            togglePanels.Add(toggles[i], panels[i]);
        }

        foreach (var pair in togglePanels)
        {
            var toggle = pair.Key;
            var panel = pair.Value;

            toggle.onValueChanged.AddListener((isOn) => {
                panel.SetActive(isOn);

                // 다른 토글이 켜지면 '모든 패널을 켜는 토글'은 꺼진다.
                if (toggle != allOnToggle && isOn && allOnToggle.isOn)
                {
                    allOnToggle.isOn = false;
                }

                // Toggle의 상태에 따라 Sprite 변경
                int index = toggles.IndexOf(toggle);
                if (index != -1)
                {
                    toggle.targetGraphic.GetComponent<Image>().sprite = isOn ? selectedSprites[index] : normalSprites[index];
                    toggle.GetComponentInChildren<Text>().color = isOn ? selectedColor : unselectedColor;
                }
            });
        }

        // '모든 패널을 켜는 토글'에 대한 처리
        allOnToggle.onValueChanged.AddListener((isOn) => {
            foreach (var panel in togglePanels.Values)
            {
                panel.SetActive(isOn);
            }

            // '모든 패널을 켜는 토글'의 상태에 따라 Sprite 변경
            allOnToggle.targetGraphic.GetComponent<Image>().sprite = isOn ? allSelectedSprite : allNormalSprite;
            allOnToggle.GetComponentInChildren<Text>().color = isOn ? selectedColor : unselectedColor;
        });
    }
}
