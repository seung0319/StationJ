using UnityEngine;
public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    public POIList poiList;
    public (double latitude,double longitude)[] paths;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    void LoadData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("POIInfo");
        poiList = JsonUtility.FromJson<POIList>(jsonFile.text);
    }
}