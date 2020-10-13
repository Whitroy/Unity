using UnityEngine;
using TMPro;
using System;

public class ScoreShower : MonoBehaviour
{
    int Reward1,Reward2,Reward3;

    public TextMeshProUGUI No_of_kills, bullet, knife, grenade;

    bool isgiven = false;
    private void Update()
    {
        if(!isgiven)
        {
            if (FindObjectOfType<Hero>().Kills > 7)
            {
                Reward1 = UnityEngine.Random.Range(0, 10);
                Reward2 = UnityEngine.Random.Range(0, 5);
                Reward3 = UnityEngine.Random.Range(0, 2);
            }
            else if (FindObjectOfType<Hero>().Kills > 5)
            {
                Reward1 = UnityEngine.Random.Range(0, 8);
                Reward2 = UnityEngine.Random.Range(0, 4);
                Reward3 = UnityEngine.Random.Range(0, 1);
            }
            else if (FindObjectOfType<Hero>().Kills > 3)
            { 
                Reward1 = UnityEngine.Random.Range(0, 6);
                Reward2 = UnityEngine.Random.Range(0, 3);
                Reward3 = UnityEngine.Random.Range(0, 0);
            }
            else if (FindObjectOfType<Hero>().Kills > 2)
            {
                Reward1 = UnityEngine.Random.Range(0, 5);
                Reward2 = UnityEngine.Random.Range(0, 3);
                Reward3 = UnityEngine.Random.Range(0, 1);
            }
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        No_of_kills.text = FindObjectOfType<Hero>().Kills.ToString();
        if(Convert.ToDecimal( PlayerPrefs.GetString("ContinueScene"))>7)
        {
            PlayerPrefs.SetInt("Reward Grenade", Reward3);
            grenade.text = Reward3.ToString();
        }

        PlayerPrefs.SetInt("Reward Knife", Reward2);
        PlayerPrefs.SetInt("Reward Bullet", Reward1);

        bullet.text = Reward1.ToString();
        knife.text = Reward2.ToString();
        isgiven = true;
    }
}
