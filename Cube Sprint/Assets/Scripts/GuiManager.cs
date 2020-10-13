using UnityEngine.UI;
using UnityEngine;

public class GuiManager : MonoBehaviour
{
    public Text Titletxt;
    public Text GameOverTxt;
    public Text InstructionTxt;

    private void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        GameOverTxt.enabled = false;
        
    }

    void GameStart()
    {
        Titletxt.enabled = false;
        GameOverTxt.enabled = false;
        enabled = false;
    }

    void GameOver()
    {
        Titletxt.enabled = true;
        GameOverTxt.enabled = true;
        enabled = true;
    }

}
