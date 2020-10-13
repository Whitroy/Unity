using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SceneManagerLevel4 : MonoBehaviour
{
    public GameObject Backgroundlayers = null;
    public GameObject BackGroundSurface = null;
    public Hero HeroPrefab = null;
    public Bat BatPrefab;
    public CheckPoint CheckptPrefab = null;
    public GameObject NextlvlWay = null;
    public GameObject EntryWay = null;
    public List<Zombie> Zombies;
    public SafeZone SafeZonePrefab = null;
    public List<Villager> Villagers;
    public Cage CagePrefab;
    public Canvas PlayerGUI = null;
    public GameObject[] Story;
    private void Awake()
    {
        Instantiate<Canvas>(PlayerGUI);
        if (Backgroundlayers != null && BackGroundSurface != null && HeroPrefab != null && BatPrefab != null && CheckptPrefab != null && NextlvlWay != null && EntryWay != null)
        {
            Hero Hero2 = Instantiate<Hero>(HeroPrefab);
            Hero2.transform.localPosition = new Vector3(-137.1f, 3.8f);

            Instantiate<GameObject>(EntryWay, new Vector2(-136.8f, 8.1f), Quaternion.identity);
            Instantiate<GameObject>(NextlvlWay,new Vector3(8.02f,-8.35f),Quaternion.identity);

            GameObject BgSurface = Instantiate<GameObject>(BackGroundSurface);
            BgSurface.transform.SetParent(transform);
            BgSurface.transform.localPosition = new Vector2(-15.84f, -4.98f);

            for (int x = 0; x < 2; x++)
            {
                Cage newCage = Instantiate<Cage>(CagePrefab, new Vector2(x * -31f - 60f, CagePrefab.transform.position.y), Quaternion.identity);
            }

            for (int i = -1; i < 9; i++)
            {
                GameObject layer = Instantiate<GameObject>(Backgroundlayers, new Vector3(i * (-18.2f), 0f, 0f), Quaternion.identity);
                layer.transform.SetParent(transform);

                if (i == 2 || i == 5)
                {
                    Instantiate<CheckPoint>(CheckptPrefab, layer.transform.localPosition, Quaternion.identity);
                }

                if (i == 4)
                {
                    SafeZone newSafeZone = Instantiate<SafeZone>(SafeZonePrefab);
                    newSafeZone.transform.localPosition = new Vector2(layer.transform.localPosition.x, -3.66f);
                    newSafeZone.transform.SetParent(transform);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                Bat newBat = Instantiate<Bat>(BatPrefab);
                newBat.transform.SetParent(GameObject.FindGameObjectWithTag("BatManager").transform);
                newBat.transform.localPosition = new Vector3(-21 - 31.5f * i, 2.5f);

                if (Villagers.Count > 0)
                {
                    int randomvil = Random.Range(0, Villagers.Count);
                    for (int j = 0; j < 2; j++)
                    {
                        Villager newVillager = Instantiate<Villager>(Villagers[randomvil]);
                        newVillager.transform.localPosition = new Vector2(newBat.transform.position.x + j * 1.5f, Villagers[randomvil].transform.localPosition.y);
                        newVillager.transform.SetParent(GameObject.FindGameObjectWithTag("Villagers").transform);
                    }
                }

            }
        }
        else
            Debug.Log("Prefabs not assigned");

        //Zombies Spawning
        if (Zombies.Count > 0)
        {
            int no_of_zombies;
            float j, k = -50.2f;
            for (int x = 1; x < 4; x++)
            {
                if (x == 2)
                    no_of_zombies = 3;
                else
                    no_of_zombies = 2;
                for (int i = 0; i < no_of_zombies; i++)
                {
                    if (i == 1)
                        j = 1.3f;
                    else
                        j = 1.4f;
                    if (x == 3)
                        k = -39.2f;
                    Zombie newZombie = Instantiate<Zombie>(Zombies[i]);
                    newZombie.transform.localPosition = new Vector2((x - 1) * k - 24.35f + i * j, Zombies[i].transform.localPosition.y);
                    newZombie.transform.SetParent(GameObject.FindGameObjectWithTag("ZombieManager").transform);
                }
            }
        }
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
