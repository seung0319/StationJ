using UnityEngine;
using UnityEngine.SceneManagement;


public class MoveScene : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
