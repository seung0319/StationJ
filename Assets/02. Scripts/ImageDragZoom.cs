using UnityEngine;
using UnityEngine.EventSystems;

public class ImageDragZoom : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector2 originalSize;
    private Vector2 lastTouchPosition;
    private bool isDragging = false;
    private bool isPinching = false; // 핀치 중인지 확인하는 플래그 추가

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
        if (isPinching) return; // 핀치 중이라면 드래그를 시작하지 않음
        isDragging = true;
        lastTouchPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && !isPinching) // 핀치 중이 아니라면 드래그 수행
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
            isPinching = true; // 두 손가락 터치가 감지되면 핀치 상태로 설정
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
            isPinching = false; // 두 손가락 터치가 끝나면 핀치 상태 해제
        }
    }
}