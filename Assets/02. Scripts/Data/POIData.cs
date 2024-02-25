using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class POIData : MonoBehaviour
{
    public POI poi;
    Button poiCouponButton;

    public void SetData(POI newPOI)
    {
        poi = newPOI;
    }

    public POI GetData()
    {
        return poi;
    }

    public void SetpoiCouponButton(GameObject couponPanel, Text poiNameText, Text poiAddressText, Text poiOpeningHoursText, Text poiCouponText)
    {
        poiCouponButton = GetComponent<Button>();
        poiCouponButton.onClick.RemoveAllListeners();
        poiCouponButton.onClick.AddListener(() => CouponSet(couponPanel, poiNameText, poiAddressText, poiOpeningHoursText, poiCouponText));
    }

    void CouponSet(GameObject couponPanel, Text poiNameText, Text poiAddressText, Text poiOpeningHoursText, Text poiCouponText)
    {
        //ÄíÆù ÆË¾÷ ÃÊ±âÈ­
        couponPanel.SetActive(true);
        poiNameText.text = poi.name;
        poiAddressText.text = poi.address;
        poiOpeningHoursText.text = poi.description;
        poiCouponText.text = poi.coupon;
    }
}
