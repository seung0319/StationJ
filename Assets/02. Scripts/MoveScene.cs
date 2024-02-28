using UnityEngine;
using UnityEngine.SceneManagement;


public class MoveScene : MonoBehaviour
{
    //씬 이름을 확인해서 이동하는 코드
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
