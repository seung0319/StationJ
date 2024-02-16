using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    

    public GameObject[] panels; // 이 배열에 각각의 패널을 할당합니다.

    // 카테고리 버튼들에 대한 참조
    public Button[] categoryButtons;

    // 현재 선택된 카테고리 버튼에 대한 참조
    private Button selectedCategoryButton;

    private Color selectedColor = new Color32(0, 148, 255, 255); // 선택된 버튼의 색
    private Color unselectedColor = new Color32(185, 188, 190, 255); // 선택되지 않은 버튼의 색

    private void Awake()
    {
        categoryButtons[3].Select();
        categoryButtons[3].GetComponentInChildren<Text>().color = selectedColor;
    }

    public void ShowPanel(int panelIndex) // 이 함수는 각 탭의 OnClick 이벤트에 연결합니다.
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
    public void ShowAllPanels() // 이 함수는 모든 패널을 활성화하는 버튼의 OnClick 이벤트에 연결합니다.
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(true);
            categoryButtons[i].GetComponentInChildren<Text>().color = unselectedColor;
        }
        categoryButtons[3].GetComponentInChildren<Text>().color = selectedColor;
    }

    public void OnCategoryButtonClicked()
    {
        Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        // 이전에 선택된 버튼의 상태를 '선택 안 함'으로 변경
        if (selectedCategoryButton != null)
        {
            // 버튼의 Sprite를 Normal Sprite로 변경합니다.
            selectedCategoryButton.image.sprite = selectedCategoryButton.spriteState.disabledSprite;
        }
        else
        {
            categoryButtons[3].Select();
            categoryButtons[3].GetComponentInChildren<Text>().color = selectedColor;
        }

        // 클릭한 버튼의 Sprite를 Pressed Sprite로 변경
        clickedButton.image.sprite = clickedButton.spriteState.selectedSprite;
        selectedCategoryButton = clickedButton;
    }
}
