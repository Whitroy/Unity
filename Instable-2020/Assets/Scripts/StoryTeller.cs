using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StoryTeller : MonoBehaviour
{
    public GameObject[] LevelOne;
    int BlockCount = 0;

    private void Start()
    {
        LevelOne[BlockCount].SetActive(true);

        StartCoroutine(Wait());
    }

    public IEnumerator Wait()
    {

        yield return new WaitForSeconds(2f);

        if (BlockCount < 2)
        {
            LevelOne[BlockCount].SetActive(false);
        }
        else
        {

        }
        BlockCount++;
        if (BlockCount < LevelOne.Length)
        {
            LevelOne[BlockCount].SetActive(true);
            StartCoroutine(Wait());
        }
        else
            StartCoroutine(Wait3());
    }

    public IEnumerator Wait3()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("2");
    }

}
