using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GUI_Manager : MonoBehaviour
{
    /// <summary>
    /// Panels
    /// </summary>
    public GameObject OuterTheme;
    public GameObject MenuPanel;
    public GameObject SettingPanel;
    public GameObject CreditPanel;
    public GameObject ExitPanel;
    public GameObject ContinuePanel;
    public GameObject LoadingPanel;
    public GameObject StorePanel;

    public GameObject StoryPanel = null;

    private void Start()
    {
        FirstLoad();
        LoadingPanel.SetActive(false);
    }

    private void NewGameStart()
    {
        OuterTheme.SetActive(false);
        MenuPanel.SetActive(false);
        SettingPanel.SetActive(false);
        CreditPanel.SetActive(false);
        ExitPanel.SetActive(false);
        ContinuePanel.SetActive(false);
        StoryPanel.SetActive(true);
    }
    public void OnClickNewGameButton()
    {
        NewGameStart();
        AudioManager.instance.Play("Button Click");
    }


    public void FirstLoad()
    {
        OuterTheme.SetActive(true);
        if(!AudioManager.instance.GetComponent<AudioSource>().isPlaying)
            AudioManager.instance.Play("Main Theme");
    }


    public void OnHomeButtonClick()
    {
        OuterTheme.SetActive(true);
        MenuPanel.SetActive(false);
        SettingPanel.SetActive(false);
        CreditPanel.SetActive(false);
        ExitPanel.SetActive(false);
        ContinuePanel.SetActive(false);

        AudioManager.instance.Play("Button Click");
    }

    public void OnClickTaptoPlay()
    {
        OuterTheme.SetActive(false);
        MenuPanel.SetActive(true);
        SettingPanel.SetActive(false);
        CreditPanel.SetActive(false);
        ExitPanel.SetActive(false);
        ContinuePanel.SetActive(false);
        AudioManager.instance.Play("Button Click");
    }

    public void OnClickSettingButton()
    {
        OuterTheme.SetActive(false);
        MenuPanel.SetActive(false);
        SettingPanel.SetActive(true);
        CreditPanel.SetActive(false);
        ExitPanel.SetActive(false);
        ContinuePanel.SetActive(false);

        AudioManager.instance.Play("Button Click");
    }

    public void OnClickCreditButton()
    {
        OuterTheme.SetActive(false);
        MenuPanel.SetActive(false);
        SettingPanel.SetActive(false);
        CreditPanel.SetActive(true);
        ExitPanel.SetActive(false);
        ContinuePanel.SetActive(false);

        AudioManager.instance.Play("Button Click");
    }

    public void OnClickMenuButton()
    {
        OuterTheme.SetActive(false);
        MenuPanel.SetActive(true);
        SettingPanel.SetActive(false);
        CreditPanel.SetActive(false);
        ExitPanel.SetActive(false);
        ContinuePanel.SetActive(false);
        StorePanel.SetActive(false);

        AudioManager.instance.Play("Button Click");
    }

    public void OnClickExit()
    {
        OuterTheme.SetActive(false);
        MenuPanel.SetActive(false);
        SettingPanel.SetActive(false);
        CreditPanel.SetActive(false);
        ExitPanel.SetActive(true);
        ContinuePanel.SetActive(false);

        AudioManager.instance.Play("Button Click");
    }

    public void OnClickYes()
    {
        Application.Quit();
        AudioManager.instance.Play("Button Click");
    }

    float elapsedtime = 0f;
    float delayedtime = 6000f;

    bool istart = false;
    private void Update()
    {
        if (istart)
        {
            elapsedtime += Time.time;
            //   Debug.Log(elapsedtime);

            if (elapsedtime > delayedtime)
            {
                istart = false;
                LoadingPanel.SetActive(false);
                SceneManager.LoadScene(PlayerPrefs.GetString("ContinueScene"));
                elapsedtime = 0f;
            }
        }
    }
    public void OnClickContinue()
    {
        OuterTheme.SetActive(false);
        MenuPanel.SetActive(false);
        SettingPanel.SetActive(false);
        CreditPanel.SetActive(false);
        ExitPanel.SetActive(false);

        if(PlayerPrefs.HasKey("ContinueScene"))
        {
            string Scene = PlayerPrefs.GetString("ContinueScene");
            if(Scene=="1" || Scene=="2")
                ContinuePanel.SetActive(true);
            else
            {
                istart = true;
                LoadingPanel.SetActive(true);
            }
        }
        else
            ContinuePanel.SetActive(true);

        AudioManager.instance.Play("Button Click");
    }

    public void OnClickSave()
    {
        PlayerPrefs.Save();
        StartCoroutine(Wait());
    }

    public void OnClickStore()
    {
        MenuPanel.SetActive(false);
        StorePanel.SetActive(true);
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        OnClickMenuButton();
    }

}
