using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    static AsyncOperation LoadLevel;

    public static void ChangeLevel(string Lvl)
    { 
        Lvl = (System.Convert.ToDecimal(Lvl) + 1).ToString();
        LoadLevel = SceneManager.LoadSceneAsync(Lvl);
        LoadLevel.allowSceneActivation = false;
    }

    public static void LoadScene()
    {
        LoadLevel.allowSceneActivation = true;
    }
}