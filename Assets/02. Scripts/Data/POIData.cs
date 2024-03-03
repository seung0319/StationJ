using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// POI 들의 데이터를 저장하는 클래스
/// </summary>
public class POIData : MonoBehaviour
{
    public POI poi;
    [SerializeField] Button poiCouponButton;
    [SerializeField] Text poiObjectName;

    // 해당 POI의 데이터를 초기화하는 함수
    public void SetData(POI newPOI)
    {
        poi = newPOI;
        if(poiCouponButton != null)
        {
            poiObjectName.text = poi.name;
        }
    }

    // 해당 POI의 데이터를 반환하는 함수
    public POI GetData()
    {
        return poi;
    }

    // ARNavgation 씬에서, POI 쿠폰 버튼을 위한 함수
    public void SetpoiCouponButton(PathPoiCreator pathPoiCreator)
    {
        poiCouponButton.onClick.RemoveAllListeners();
        poiCouponButton.onClick.AddListener(() => CouponSet(pathPoiCreator));
    }

    // ARNavigation 씬에서, POI의 값을 초기화 하는 함수
    void CouponSet(PathPoiCreator pathPoiCreator)
    {
        //쿠폰 팝업 초기화
        pathPoiCreator.couponPanel.SetActive(true);
        pathPoiCreator.poiNameText.text = poi.name;
        pathPoiCreator.poiAddressText.text = poi.address;
        pathPoiCreator.poiOpeningHoursText.text = poi.description;
        pathPoiCreator.poiCouponText.text = poi.coupon;
    }
}
