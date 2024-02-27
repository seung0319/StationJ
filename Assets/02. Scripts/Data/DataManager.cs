using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    public POIList poiList;
    public POI selectedPoi;
    public (double latitude, double longitude)[] paths;
    public int distance;
    public int duration;

    bool isStartCoroutine = false;

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

    public void ParseJson(string jsonString)
    {
        JObject data = JObject.Parse(jsonString);
        JArray jsonPaths = (JArray)data["route"]["traoptimal"][0]["path"];
        JArray jsonGoal = (JArray)data["route"]["traoptimal"][0]["summary"]["goal"]["location"];
        int jsonDistance = data["route"]["traoptimal"][0]["summary"]["distance"].ToObject<int>();
        int jsonDuration = data["route"]["traoptimal"][0]["summary"]["duration"].ToObject<int>();

        paths = new (double, double)[jsonPaths.Count + 1];

        for (int i = 0; i < jsonPaths.Count; i++)
        {
            paths[i] = (jsonPaths[i][1].ToObject<double>(), jsonPaths[i][0].ToObject<double>()); // API에서는 longitude, latitude 순서이지만 튜플에는 latitude, longitude 순서로 저장합니다.
        }
        paths[jsonPaths.Count] = (jsonGoal[1].ToObject<double>(), jsonGoal[0].ToObject<double>()); // 마지막에 jsonGoal의 좌표를 추가합니다.
        distance = jsonDistance;
        duration = jsonDuration;
    }

    public void LocationInfoGetStart()
    {
        if (isStartCoroutine)
            return;

        isStartCoroutine = true;
        StartCoroutine(LocationInfoGet());
    }

    IEnumerator LocationInfoGet()
    {
        Input.location.Start(1,1);

        YieldInstruction delay = new WaitForSeconds(1);

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return delay;
            maxWait--;
        }

        // 시간 초과
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }
        // 위치 서비스 시작 실패
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
    }
}