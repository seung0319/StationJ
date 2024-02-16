using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonOrder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Animator animator;
    private bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Pressed"))
        if(pressed)
        {
            gameObject.transform.SetAsLastSibling();
        }
    }

    public void IPointerDownHandler()
    {

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
