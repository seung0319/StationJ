using UnityEngine;
using UnityEngine.EventSystems;

public class ImageDragZoom : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector2 originalSize;
    private Vector2 lastTouchPosition;
    private bool isDragging = false;
    private bool isPinching = false; // ��ġ ������ Ȯ���ϴ� �÷��� �߰�

    private float minScale = 0.5f;
    private float maxScale = 2.0f;
    private float zoomSpeed = 10f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalSize = rectTransform.sizeDelta;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isPinching) return; // ��ġ ���̶�� �巡�׸� �������� ����
        isDragging = true;
        lastTouchPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && !isPinching) // ��ġ ���� �ƴ϶�� �巡�� ����
        {
            Vector2 delta = eventData.position - lastTouchPosition;
            rectTransform.anchoredPosition += delta;
            lastTouchPosition = eventData.position;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            isPinching = true; // �� �հ��� ��ġ�� �����Ǹ� ��ġ ���·� ����
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            float newScale = rectTransform.sizeDelta.x - deltaMagnitudeDiff * zoomSpeed;
            newScale = Mathf.Clamp(newScale, originalSize.x * minScale, originalSize.x * maxScale);

            rectTransform.sizeDelta = new Vector2(newScale, newScale);
        }
        else
        {
            isPinching = false; // �� �հ��� ��ġ�� ������ ��ġ ���� ����
        }
    }
}