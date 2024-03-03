using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// HomeScreen의 버튼에 터치중이거나, 마우스를 올렸을 때
/// 해당 버튼이 제일 마지막 자식으로 변경되는 클래스 
/// </summary>
public class ButtonOrder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pressed = false;
    // Update is called once per frame
    void Update()
    {
        if(pressed)
        {
            gameObject.transform.SetAsLastSibling();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}
