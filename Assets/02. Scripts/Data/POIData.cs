using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// POI ���� �����͸� �����ϴ� Ŭ����
/// </summary>
public class POIData : MonoBehaviour
{
    public POI poi;
    [SerializeField] Button poiCouponButton;
    [SerializeField] Text poiObjectName;

    // �ش� POI�� �����͸� �ʱ�ȭ�ϴ� �Լ�
    public void SetData(POI newPOI)
    {
        poi = newPOI;
        if(poiCouponButton != null)
        {
            poiObjectName.text = poi.name;
        }
    }

    // �ش� POI�� �����͸� ��ȯ�ϴ� �Լ�
    public POI GetData()
    {
        return poi;
    }

    // ARNavgation ������, POI ���� ��ư�� ���� �Լ�
    public void SetpoiCouponButton(PathPoiCreator pathPoiCreator)
    {
        poiCouponButton.onClick.RemoveAllListeners();
        poiCouponButton.onClick.AddListener(() => CouponSet(pathPoiCreator));
    }

    // ARNavigation ������, POI�� ���� �ʱ�ȭ �ϴ� �Լ�
    void CouponSet(PathPoiCreator pathPoiCreator)
    {
        //���� �˾� �ʱ�ȭ
        pathPoiCreator.couponPanel.SetActive(true);
        pathPoiCreator.poiNameText.text = poi.name;
        pathPoiCreator.poiAddressText.text = poi.address;
        pathPoiCreator.poiOpeningHoursText.text = poi.description;
        pathPoiCreator.poiCouponText.text = poi.coupon;
    }
}
