using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject locationPanel;
    public GameObject makerPanel;
    public GameObject routeFindPanerl;
    private int order = 0;

    private bool locationOK;

    public GameObject[] panels; // �� �迭�� ������ �г��� �Ҵ��մϴ�.

    // ī�װ� ��ư�鿡 ���� ����
    public Button[] categoryButtons;

    // ���� ���õ� ī�װ� ��ư�� ���� ����
    private Button selectedCategoryButton;

    public Text[] texts;
    private Color selectedColor = new Color32(54, 148, 215, 255); // ���õ� ��ư�� ��
    private Color unselectedColor = new Color32(185, 189, 189, 255); // ���õ��� ���� ��ư�� ��

    private void Start()
    {
        locationOK = Permission.HasUserAuthorizedPermission(Permission.FineLocation);

        if (!locationOK)
        {
            locationPanel.SetActive(true);
            makerPanel.SetActive(false);
        }
    }

    public void ShowPanel(int panelIndex) // �� �Լ��� �� ���� OnClick �̺�Ʈ�� �����մϴ�.
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == panelIndex)
            {
                panels[i].SetActive(true);
                texts[i].color = selectedColor;
            }
            else
            {
                panels[i].SetActive(false);
                texts[i].color = unselectedColor;
            }
        }
        texts[3].color = unselectedColor;
    }
    public void ShowAllPanels() // �� �Լ��� ��� �г��� Ȱ��ȭ�ϴ� ��ư�� OnClick �̺�Ʈ�� �����մϴ�.
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(true);
            texts[i].color = unselectedColor;
        }
        texts[3].color = selectedColor;
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

        // Ŭ���� ��ư�� Sprite�� Pressed Sprite�� ����
        clickedButton.image.sprite = clickedButton.spriteState.pressedSprite;
        selectedCategoryButton = clickedButton;
    }

    public void LocationInfoAllow(bool Allow)
    {
        if(order == 0)  //ù��° �˾�â�� ���
        {
            if (Allow)  //��ġ���ѿ�û ���
            {
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
                locationOK = true;
                order++;
            }
            else        //��ġ���ѿ�û ��� ����
            {
                order++;
            }
        }
        else if(order == 1)  //�ι�° �˾�â�� ���
        {
            if (Allow)  //��ġ���ѿ�û ���
            {
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
                locationOK = true;
                routeFindPanerl.SetActive(true);
            }
            else        //��ġ���ѿ�û ��� ����
            {
                
            }
        }
    }
    public void RouteFind()
    {
        if (!locationOK)
        {
            locationPanel.SetActive(true);
        }
        else
        {
            routeFindPanerl.SetActive(true);
        }
    }
}
