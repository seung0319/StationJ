using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance; // �̱��� �ν��Ͻ�

    public GameObject Panel1; // ������ �ǳ�1
    public GameObject Panel2; // ������ �ǳ�2

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
