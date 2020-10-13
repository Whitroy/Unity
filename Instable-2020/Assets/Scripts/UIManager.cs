using UnityEngine.UI;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public Slider health;
    public Text chancesTxt, bullets_Txt, knife_Txt, grenade_Txt;

    private void UpdateUI()
    {
        //update UI
        if (FindObjectOfType<Hero>())
        {
            health.value = FindObjectOfType<Hero>().herohealth;
            chancesTxt.text = "X " + FindObjectOfType<Hero>().Chances.ToString();
            bullets_Txt.text = "X " + FindObjectOfType<Hero>().Bullets.ToString();
            knife_Txt.text = "X " + FindObjectOfType<Hero>().Knife.ToString();
            grenade_Txt.text = "X " + FindObjectOfType<Hero>().Grenade.ToString();
        }
    }

    private void Update()
    {
        UpdateUI();
    }
}
