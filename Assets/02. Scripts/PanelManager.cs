using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance; // ½Ì±ÛÅæ ÀÎ½ºÅÏ½º

    public GameObject Panel1; // °ü¸®ÇÒ ÆÇ³Ú1
    public GameObject Panel2; // °ü¸®ÇÒ ÆÇ³Ú2

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TogglePanel1()
    {
        Panel1.SetActive(!Panel1.activeSelf);
    }

    public void TogglePanel2()
    {
        Panel2.SetActive(!Panel2.activeSelf);
    }
}
