/// <summary>
/// Json�� ������ Ŭ����
/// �̸�, �ǹ�����, �ǹ�����, ����, �浵, �ּ�, ���� �� ������ �ֽ��ϴ�.
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
/// �� �׸���� �迭�� �����ϴ� Ŭ����
/// </summary>
[System.Serializable]
public class POIList
{
    public POI[] pois;
}
