using UnityEngine.SceneManagement;
using UnityEngine;

public class PausedPanel : MonoBehaviour
{
    bool ispaused = false;
    public GameObject GUI;
    public GameObject PausePanel;
    public GameObject StorePanel;
    private void Start()
    {
        PausePanel.SetActive(false);
        GUI.SetActive(true);
        StorePanel.SetActive(false);
    }
    private void Update()
    {
        if (ispaused)
        {
            Time.timeScale = 0f;
            AudioManager.instance.StopALL();
        }
    }

    public void OnClickPausedButton()
    {
        ispaused = true;
        GUI.SetActive(false);
        PausePanel.SetActive(true);
        //Admanager.Instance.ShowBanner();
    }

    public void OnClickHomeButton()
    {
        ispaused = false;
        Time.timeScale = 1f;
        AudioManager.instance.Stop("Death Panel");
        AudioManager.instance.Play("Main Theme");
        SceneManager.LoadScene("1");
        //Admanager.Instance.DestroyBanner();
        //Admanager.Instance.ShowInterstitial();
    }

    public void OnClickResumeButton()
    {
        ispaused = false;
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
        GUI.SetActive(true);
        AudioManager.instance.Play("Theme1");
        //Admanager.Instance.DestroyBanner();
    }

    public void OnClickRetryButton()
    {
        ispaused = false;
        Time.timeScale = 1f;
        AudioManager.instance.Stop("Death Panel");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Admanager.Instance.DestroyBanner();
        //Admanager.Instance.ShowInterstitial();
    }

    public void OnCLickAddButton()
    {
        //Admanager.Instance.ShowRewardedVideo();
    }

    public void onclickStoreButton()
    {
        StorePanel.SetActive(true);
    }

    public void onclickCloseButton()
    {
        StorePanel.SetActive(false);
    }
}
