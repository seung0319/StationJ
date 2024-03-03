using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// HomeScreen�� ��ư�� ��ġ���̰ų�, ���콺�� �÷��� ��
/// �ش� ��ư�� ���� ������ �ڽ����� ����Ǵ� Ŭ���� 
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
