using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class Antitode : MonoBehaviour
{
    public GameObject Background, CreditPanel, FinalMsg;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Hero"))
        {
            Background.SetActive(true);
            CreditPanel.SetActive(false);
            FinalMsg.SetActive(true);
            Destroy(gameObject.GetComponent<Collider2D>());
            AudioManager.instance.Stop("Theme1");
            AudioManager.instance.Play("Main Theme");
            StartCoroutine(Wait());
        }

    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3f);
        FinalMsg.SetActive(false);
        CreditPanel.SetActive(true);
    }

    public void close()
    {
        SceneManager.LoadScene("1");
        AudioManager.instance.Play("Main Theme");
    }
}
