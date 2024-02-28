using UnityEngine;
using UnityEngine.SceneManagement;


public class MoveScene : MonoBehaviour
{
    //씬 이름을 확인해서 이동하는 코드
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void MapScreenChangeScene()
    {
        if (PDListStateSaver.statesaverPOI != null)
        {
            SceneManager.LoadScene("PDListScreen");
            DataManager.instance.paths = null;
        }
    }
}
