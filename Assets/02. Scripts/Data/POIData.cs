using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class POIData : MonoBehaviour
{
    public POI poi;
    [SerializeField] Button poiCouponButton;
    [SerializeField] Text poiObjectName;

    public void SetData(POI newPOI)
    {
        poi = newPOI;
        if(poiCouponButton != null)
        {
            poiObjectName.text = poi.name;
        }
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
        //ÄíÆù ÆË¾÷ ÃÊ±âÈ­
        pathPoiCreator.couponPanel.SetActive(true);
        pathPoiCreator.poiNameText.text = poi.name;
        pathPoiCreator.poiAddressText.text = poi.address;
        pathPoiCreator.poiOpeningHoursText.text = poi.description;
        pathPoiCreator.poiCouponText.text = poi.coupon;
    }
}
