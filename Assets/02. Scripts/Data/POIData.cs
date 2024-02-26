using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class POIData : MonoBehaviour
{
    public POI poi;
    [SerializeField] Button poiCouponButton;

    public void SetData(POI newPOI)
    {
        poi = newPOI;
    }

    public POI GetData()
    {
        return poi;
    }

    public void SetpoiCouponButton(PathPoiCreator pathPoiCreator)
    {
        poiCouponButton.onClick.RemoveAllListeners();
        poiCouponButton.onClick.AddListener(() => CouponSet(pathPoiCreator));
    }

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
