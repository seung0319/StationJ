using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json.Linq;

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

    public void LoadPath()
    {
        string path = Path.Combine(Application.dataPath, "04. Resources/path.json");
        string json = File.ReadAllText(path);

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

        // ��ġ ���� ���
        foreach (var points in paths)
        {
            Debug.Log($"Longitude: {points.longitude}, Latitude: {points.latitude}");
        }
    }
}