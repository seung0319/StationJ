using UnityEngine;
using UnityEngine.EventSystems;

public class ImageDragZoom : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector2 originalSize;
    private Vector2 lastTouchPosition;
    private bool isDragging = false;
    private bool isPinching = false;

    private float minScale = 0.5f;
    private float maxScale = 2.0f;
    private float zoomSpeed = 3f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalSize = rectTransform.sizeDelta;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isPinching) return;
        isDragging = true;
        lastTouchPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging && !isPinching)
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
            Vector2 oldSize = rectTransform.sizeDelta;
            Vector3 oldPos = rectTransform.localPosition;

            if (!isPinching)
            {
                isPinching = true;
                originalSize = rectTransform.sizeDelta;
            }

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

            Vector2 newPos = (touchZero.position + touchOne.position) / 2;
            Vector2 oldPosOnScreen = RectTransformUtility.WorldToScreenPoint(Camera.main, oldPos);
            Vector2 oldScaleFactor = (oldPosOnScreen - newPos) / oldSize;
            Vector2 shift = oldScaleFactor * (newScale - oldSize.x);

            rectTransform.localPosition += (Vector3)shift;
        }
        else
        {
            isPinching = false;
        }
    }
}