using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LodingEnd", 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LodingEnd()
    {
        SceneManager.LoadScene("HomeScreen");
    }
}
