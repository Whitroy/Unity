using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SceneManagerLevel5 : MonoBehaviour
{
    public GameObject Backgroundlayers = null;
    public GameObject BackGroundSurface = null;
    public Hero HeroPrefab = null;
    public Bat BatPrefab;
    public CheckPoint CheckptPrefab = null;
    public GameObject NextlvlWay = null;
    public GameObject EntryWay = null;
    public List<Zombie> Zombies;
    public Canvas PlayerGUI = null;
    public GameObject[] Story;
    private void Awake()
    {
        Instantiate<Canvas>(PlayerGUI);
        if (Backgroundlayers != null && BackGroundSurface != null && HeroPrefab != null && BatPrefab != null && CheckptPrefab != null && NextlvlWay != null && EntryWay != null)
        {
            Hero Hero2 = Instantiate<Hero>(HeroPrefab);
            Hero2.transform.localPosition = new Vector3(9.49f, 3.8f);

            Instantiate<GameObject>(EntryWay, new Vector2(9.72f, 8.1f), Quaternion.identity);
            Instantiate<GameObject>(NextlvlWay, new Vector2(NextlvlWay.transform.localPosition.x, -7.81f), Quaternion.identity);

            GameObject BgSurface = Instantiate<GameObject>(BackGroundSurface);
            BgSurface.transform.SetParent(transform);

            for (int i = -1; i < 9; i++)
            {
                GameObject layer = Instantiate<GameObject>(Backgroundlayers, new Vector3(i * (-18.2f), 0f, 0f), Quaternion.identity);
                layer.transform.SetParent(transform);

                if (i == 2 || i == 5)
                {
                    Instantiate<CheckPoint>(CheckptPrefab, layer.transform.localPosition, Quaternion.identity);
                }

                /*if (i == 1 || i == 3 || i == 5 || i == 6)
                {
                    Zombie newZombie = Instantiate<Zombie>(Zombies[Random.Range(0, Zombies.Count)]);
                    newZombie.transform.parent = null;
                    newZombie.transform.localPosition = layer.transform.GetChild(8).GetChild(1).GetChild(0).transform.position;
                    newZombie.transform.SetParent(layer.transform.GetChild(8).GetChild(1).GetChild(0).transform);
                }*/
            }

            for (int i = 0; i < 4; i++)
            {
                Bat newBat = Instantiate<Bat>(BatPrefab);
                newBat.transform.SetParent(GameObject.FindGameObjectWithTag("BatManager").transform);
                newBat.transform.localPosition = new Vector3(-21 - 31.5f * i, 2.5f);
            }
        }
        else
            Debug.Log("Prefabs not assigned");

    }

    private void Start()
    {
        FindObjectOfType<Hero>().Kills = 0;
        ScreenPlay();
        if (PlayerPrefs.HasKey("Reward Greande"))
        {
            FindObjectOfType<Hero>().Grenade += PlayerPrefs.GetInt("Reward Grenade");
            PlayerPrefs.SetInt("Reward Grenade", 0);
        }
        if (PlayerPrefs.HasKey("Reward Bullet"))
        {
            FindObjectOfType<Hero>().Bullets += PlayerPrefs.GetInt("Reward Bullet");
            PlayerPrefs.SetInt("Reward Bullet", 0);
        }
        if (PlayerPrefs.HasKey("Reward Knife"))
        {
            FindObjectOfType<Hero>().Knife += PlayerPrefs.GetInt("Reward Knife");
            PlayerPrefs.SetInt("Reward Knife", 0);
        }
        if (PlayerPrefs.HasKey("Chancesleft"))
        {
            if (PlayerPrefs.GetInt("Chancesleft") > 0)
                FindObjectOfType<Hero>().Chances = PlayerPrefs.GetInt("Chancesleft");
            else
                FindObjectOfType<Hero>().Chances = 2;

        }
    }

    void ScreenPlay()
    {
        int i = 0;
        Story[i].SetActive(true);
        i++;
        StartCoroutine(StroyPlay(i));
    }

    private IEnumerator StroyPlay(int i)
    {
        for (i = 1; i < Story.Length; i++)
        {
            Story[i].SetActive(true);
            yield return new WaitForSeconds(3f);
        }

        yield return new WaitForSeconds(3f);
        for (i--; i >= 0; i--)
        {
            Story[i].SetActive(false);
        }

        AudioManager.instance.Play("Theme1");
        AudioManager.instance.Stop("Main Theme");
    }
}
