using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    

    public GameObject[] panels; // �� �迭�� ������ �г��� �Ҵ��մϴ�.

    // ī�װ� ��ư�鿡 ���� ����
    public Button[] categoryButtons;

    // ���� ���õ� ī�װ� ��ư�� ���� ����
    private Button selectedCategoryButton;

    private Color selectedColor = new Color32(0, 148, 255, 255); // ���õ� ��ư�� ��
    private Color unselectedColor = new Color32(185, 188, 190, 255); // ���õ��� ���� ��ư�� ��

    private void Awake()
    {
        categoryButtons[3].Select();
        categoryButtons[3].GetComponentInChildren<Text>().color = selectedColor;
    }

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

    public void OnCategoryButtonClicked()
    {
        Button clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        // ������ ���õ� ��ư�� ���¸� '���� �� ��'���� ����
        if (selectedCategoryButton != null)
        {
            // ��ư�� Sprite�� Normal Sprite�� �����մϴ�.
            selectedCategoryButton.image.sprite = selectedCategoryButton.spriteState.disabledSprite;
        }
        else
        {
            categoryButtons[3].Select();
            categoryButtons[3].GetComponentInChildren<Text>().color = selectedColor;
        }

        // Ŭ���� ��ư�� Sprite�� Pressed Sprite�� ����
        clickedButton.image.sprite = clickedButton.spriteState.selectedSprite;
        selectedCategoryButton = clickedButton;
    }
}
