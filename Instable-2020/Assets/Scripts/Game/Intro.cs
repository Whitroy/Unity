using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class Intro : MonoBehaviour
{
    AsyncOperation LoadMainMenu;

    private float Progress = 0;

    private bool isloaded = false;
    private void Start()
    {
        LoadMainMenu = SceneManager.LoadSceneAsync("1");
        LoadMainMenu.allowSceneActivation = false;

        Progress = LoadMainMenu.progress;
    }

    private void Update()
    {
        if (Progress > .85f)
        {
            if (!LoadMainMenu.isDone && !isloaded)
            {
                StartCoroutine(Wait());
                isloaded = true;
                return;
            }
        }
        Progress = LoadMainMenu.progress;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.3f);
        LoadMainMenu.allowSceneActivation = true;
    }
}
