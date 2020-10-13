using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Player player;

    public Text scoreLabel;

    public void StartGame(int mode)
    {
        player.StartGame(mode);
        gameObject.SetActive(false);
    }

    public void EndGame(float distanceTravelled)
    {
        scoreLabel.text = ((int)(distanceTravelled * 10f)).ToString();
        gameObject.SetActive(true);
    }
}
