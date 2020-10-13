using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevelWay : MonoBehaviour
{
    float elapsedtime = 0f;
    float delayedtime = 6000f;

    bool istart = false, pausesound = false;

    public GameObject LoadingPanel=null;
    public GameObject ScorePanel=null;

    private void Start()
    {
        if (LoadingPanel != null)
            LoadingPanel.SetActive(false);
        if(ScorePanel != null)
            ScorePanel.SetActive(false);
    }
    private void Update()
    {
        if(istart)
        {
            elapsedtime += Time.time;
         //   Debug.Log(elapsedtime);
            if(elapsedtime>delayedtime)
            {
                istart = false;
                if(ScorePanel!=null)
                    ScorePanel.SetActive(false);
                LoadingPanel.SetActive(false);
                elapsedtime = 0f;
                GameManager.LoadScene();
             //   Admanager.Instance.DestroyBanner();
            }
        }

        if (pausesound)
        {
            AudioManager.instance.StopALL();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            AudioManager.instance.Play("Lvl clear");
            if(ScorePanel!=null)
                ScorePanel.SetActive(true);
            FindObjectOfType<PausedPanel>().gameObject.SetActive(false);
            pausesound = true;
            PlayerPrefs.SetInt("Chancesleft", FindObjectOfType<Hero>().Chances);
            AudioManager.instance.StopALL();
            Time.timeScale = 0f;
        }
    }

    public void OnClickContinue()
    {
        Debug.Log("Pressed Continue");
        Time.timeScale = 1f;
        istart = true;
        if(ScorePanel!=null)
            ScorePanel.SetActive(false);
        if(LoadingPanel!=null)
            LoadingPanel.SetActive(true);
        GameManager.ChangeLevel(SceneManager.GetActiveScene().name);
        AudioManager.instance.StopALL();
        //Admanager.Instance.ShowBanner();
    }

    public void OnClickHome()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("ContinueScene", (System.Convert.ToDecimal(SceneManager.GetActiveScene().name)+1).ToString());
        PlayerPrefs.Save();
        SceneManager.LoadScene("1");
     //   Admanager.Instance.ShowInterstitial();
    }
}
