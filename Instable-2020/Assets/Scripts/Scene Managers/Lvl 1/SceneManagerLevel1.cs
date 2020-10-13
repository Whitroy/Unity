using UnityEngine;
public class SceneManagerLevel1 : MonoBehaviour
{
    public GameObject Backgroundlayers = null;
    public GameObject BackGroundSurface = null;
    public Hero HeroPrefab = null;
    public Bat BatPrefab;
    public CheckPoint CheckptPrefab = null;
    public GameObject NextlvlWay = null;
    public Canvas PlayerGUI = null;
    private void Awake()
    {
        Instantiate<Canvas>(PlayerGUI);
        if (Backgroundlayers != null && BackGroundSurface != null && HeroPrefab != null && BatPrefab != null && CheckptPrefab != null && NextlvlWay != null)
        {

            Hero Hero1 = Instantiate<Hero>(HeroPrefab);
            Hero1.transform.localPosition = new Vector3(9.49f, HeroPrefab.transform.localPosition.y);

            Instantiate<GameObject>(NextlvlWay, new Vector2(-132.42f, -.68f), Quaternion.identity);

            GameObject BgSurface = Instantiate<GameObject>(BackGroundSurface);
            BgSurface.transform.SetParent(transform);

            for (int i = -1; i < 9; i++)
            {
                GameObject layer = Instantiate<GameObject>(Backgroundlayers, new Vector3(i * (-18.2f), 0f, 0f), Quaternion.identity);
                layer.transform.SetParent(transform);
                if (i != 8 && i != -1)
                {
                    Bat newBat = Instantiate<Bat>(BatPrefab);
                    newBat.transform.SetParent(GameObject.FindGameObjectWithTag("BatManager").transform);
                    newBat.transform.localPosition = new Vector3(layer.transform.localPosition.x, Random.Range(1.5f, 1.65f));
                }
                if (i == 2 || i == 5)
                {
                    Instantiate<CheckPoint>(CheckptPrefab, layer.transform.localPosition, Quaternion.identity);
                }
            }
        }
        else
            Debug.Log("Prefabs not assigned");
    }

    private void Start()
    {
        FindObjectOfType<Hero>().Kills =0;
        AudioManager.instance.Stop("Main Theme");
        AudioManager.instance.Play("Theme1");
        if (PlayerPrefs.HasKey("Reward Greande"))
        {
            FindObjectOfType<Hero>().Grenade+=PlayerPrefs.GetInt("Reward Grenade");
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

        FindObjectOfType<Hero>().Chances = 2;
    }
}
