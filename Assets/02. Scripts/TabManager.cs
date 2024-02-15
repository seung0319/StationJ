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

    public GameObject[] panels; // 이 배열에 각각의 패널을 할당합니다.

    // 카테고리 버튼들에 대한 참조
    public Button[] categoryButtons;

    // 현재 선택된 카테고리 버튼에 대한 참조
    private Button selectedCategoryButton;

    public Text[] texts;
    private Color selectedColor = new Color32(54, 148, 215, 255); // 선택된 버튼의 색
    private Color unselectedColor = new Color32(185, 189, 189, 255); // 선택되지 않은 버튼의 색

    private void Start()
    {
        locationOK = Permission.HasUserAuthorizedPermission(Permission.FineLocation);

        if (!locationOK)
        {
            locationPanel.SetActive(true);
            makerPanel.SetActive(false);
        }
    }

    public void ShowPanel(int panelIndex) // 이 함수는 각 탭의 OnClick 이벤트에 연결합니다.
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
    public void ShowAllPanels() // 이 함수는 모든 패널을 활성화하는 버튼의 OnClick 이벤트에 연결합니다.
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
        // 이전에 선택된 버튼의 상태를 '선택 안 함'으로 변경
        if (selectedCategoryButton != null)
        {
            // 버튼의 Sprite를 Normal Sprite로 변경합니다.
            selectedCategoryButton.image.sprite = selectedCategoryButton.spriteState.disabledSprite;
        }

        // 클릭한 버튼의 Sprite를 Pressed Sprite로 변경
        clickedButton.image.sprite = clickedButton.spriteState.pressedSprite;
        selectedCategoryButton = clickedButton;
    }

    public void LocationInfoAllow(bool Allow)
    {
        if(order == 0)  //첫번째 팝업창의 경우
        {
            if (Allow)  //위치권한요청 허용
            {
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
                locationOK = true;
                order++;
            }
            else        //위치권한요청 허용 안함
            {
                order++;
            }
        }
        else if(order == 1)  //두번째 팝업창의 경우
        {
            if (Allow)  //위치권한요청 허용
            {
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.FineLocation);
                locationOK = true;
                routeFindPanerl.SetActive(true);
            }
            else        //위치권한요청 허용 안함
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
