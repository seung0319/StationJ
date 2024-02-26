/// <summary>
/// Json을 저장할 클래스
/// 이름, 건물종류, 건물사진, 위도, 경도, 주소, 설명 를 가지고 있습니다.
/// </summary>
[System.Serializable]
public class POI
{
    public string name;
    public string type;
    public string image;
    public double latitude;
    public double longitude;
    public string address;
    public string description;
    public string coupon;
}

/// <summary>
/// 위 항목들의 배열을 저장하는 클래스
/// </summary>
[System.Serializable]
public class POIList
{
    public POI[] pois;
}
