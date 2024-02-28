using UnityEngine;
using UnityEngine.SceneManagement;


public class MoveScene : MonoBehaviour
{
    //�� �̸��� Ȯ���ؼ� �̵��ϴ� �ڵ�
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
