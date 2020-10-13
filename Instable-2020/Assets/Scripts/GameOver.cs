using UnityEngine;
using System.Collections;
public class GameOver : MonoBehaviour
{
    public Hero heropb;
    private GameObject PlayerGUI;

    private CheckPoint[] checkpoints;
    private int bullet;
    private int chances;
    private int knife;
    private int grenade;
    private int kills;
    private Vector2 spawnpos;

    public GameObject Camera;
    public Hero FakeHero;
    float lastcheckpoint_pos = Mathf.Infinity;
    Vector2 Fakepos;
    private void Start()
    {
        PlayerGUI = FindObjectOfType<PausedPanel>().gameObject;
        PlayerGUI.transform.GetChild(1).gameObject.SetActive(false);
        PlayerGUI.transform.GetChild(3).gameObject.SetActive(false);
    }
    public void afterdestroy(Hero hero)
    {
        if (hero.Chances > 0)
        {
            chances = hero.Chances;
            bullet = hero.Bullets;
            knife = hero.Knife;
            grenade =hero.Grenade;
            spawnpos = hero.IntialSpawn_X;
            kills = hero.Kills;
            chances--;

            Fakepos = hero.transform.position;
            Destroy(hero.gameObject);
            AudioManager.instance.StopALL();
            if (System.Convert.ToDecimal(PlayerPrefs.GetString("ContinueScene")) < 11)
            {
                Camera.SetActive(true);
            }
            else
            {
                if (!FindObjectOfType<Boss>() && System.Convert.ToDecimal(PlayerPrefs.GetString("ContinueScene")) == 12)
                {
                    Camera.SetActive(true);
                    spawnpos = Fakepos;
                    heropb = FakeHero;
                }
            }
            checkpoints = FindObjectsOfType<CheckPoint>();
            foreach(CheckPoint C in checkpoints)
            {
                if(!C.GetComponent<BoxCollider2D>())
                {
                    if (C.transform.localPosition.x < lastcheckpoint_pos)
                        lastcheckpoint_pos = C.transform.localPosition.x;
                }
            }
            PlayerGUI.transform.GetChild(0).gameObject.SetActive(false);
            PlayerGUI.transform.GetChild(3).gameObject.SetActive(true);
            StartCoroutine(Wait());
        }
        else
        {
            Destroy(hero.gameObject);
            PlayerPrefs.SetInt("Chancesleft",2);
            AudioManager.instance.StopALL();
            if (System.Convert.ToDecimal(PlayerPrefs.GetString("ContinueScene")) < 11)
            {
                Camera.SetActive(true);
            }
            else
            {
                if (!FindObjectOfType<Boss>() && System.Convert.ToDecimal(PlayerPrefs.GetString("ContinueScene")) == 12)
                {
                    Camera.SetActive(true);
                    spawnpos = Fakepos;
                    heropb = FakeHero;
                }
            }
            PlayerGUI.transform.GetChild(1).gameObject.SetActive(true);
            PlayerGUI.transform.GetChild(0).gameObject.SetActive(false);
            AudioManager.instance.Play("Death Panel");
            //Admanager.Instance.ShowInterstitial();
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
        PlayerGUI.transform.GetChild(0).gameObject.SetActive(true);
        PlayerGUI.transform.GetChild(3).gameObject.SetActive(false);
        Camera.SetActive(false);
        Hero Hero1 = Instantiate<Hero>(heropb);
        AudioManager.instance.Play("Respawn");
        AudioManager.instance.Play("Theme1");
        if (lastcheckpoint_pos != Mathf.Infinity)
        {
            if(System.Convert.ToDecimal(PlayerPrefs.GetString("ContinueScene"))==6)
                Hero1.transform.localPosition = new Vector3(lastcheckpoint_pos, spawnpos.y);
            else
                Hero1.transform.localPosition = new Vector3(lastcheckpoint_pos, heropb.transform.localPosition.y);
        }
        else
            Hero1.transform.localPosition = spawnpos;

        if (FindObjectOfType<Boss>())
            AudioManager.instance.Play("Boss idle");

        Hero1.Chances = chances;
        Hero1.Bullets = bullet;
        Hero1.Grenade = grenade;
        Hero1.Knife = knife;
        Hero1.Kills = kills;
    }
}
