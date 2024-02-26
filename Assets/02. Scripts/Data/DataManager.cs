using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Traoptimal
{
    public double[][] path;
}

[System.Serializable]
public class Route
{
    public Traoptimal[] traoptimal;
}

[System.Serializable]
public class Root
{
    public Route route;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    public POIList poiList;
    public POI selectedPoi;
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

    /// <summary>
    /// ����� Json ������ �о ������ �����ϴ� �Լ�
    /// </summary>
    public void LoadPath()
    {
        //string path = Path.Combine(Application.dataPath, "04. Resources/path.json");
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Path.Combine(Application.persistentDataPath, "path.json");
        }
        else
        {
            path = Path.Combine(Application.dataPath, "04. Resources/path.json");
        }
        //string json = File.ReadAllText(path);
        string json = File.ReadAllText(DirectionManager.json);

        Root root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(json);  // Newtonsoft.Json�� ����� �Ľ�

        if (root == null)
        {
            Debug.LogError("Failed to parse JSON data.");
            return;
        }

        if (root.route.traoptimal == null)
        {
            Debug.LogError("Failed to parse 'route.traoptimal' in JSON data.");
            return;
        }

        // "path" �׸��� (double, double) Ʃ�� �迭�� ����
        int pathCount = root.route.traoptimal.Sum(t => t.path.Length);
        paths = new (double latitude, double longitude)[pathCount];

        int i = 0;
        foreach (var traoptimal in root.route.traoptimal)
        {
            if (traoptimal.path == null)
            {
                Debug.LogError("Path is null in 'traoptimal'");
                continue;
            }

            foreach (var coord in traoptimal.path)
            {
                paths[i] = (latitude: coord[1], longitude: coord[0]);
                i++;
            }

        }
        
        // ��� ����
        foreach (var points in paths)
        {
            Debug.Log($"Longitude: {points.longitude}, Latitude: {points.latitude}");
        }

        
    }

    public void ParseJson(string jsonString)
    {
        JObject data = JObject.Parse(jsonString);
        JArray jsonPaths = (JArray)data["route"]["traoptimal"][0]["path"];

        paths = new (double, double)[jsonPaths.Count];

        for (int i = 0; i < jsonPaths.Count; i++)
        {
            paths[i] = (jsonPaths[i][1].ToObject<double>(), jsonPaths[i][0].ToObject<double>()); // API������ longitude, latitude ���������� Ʃ�ÿ��� latitude, longitude ������ �����մϴ�.
        }
    }

}