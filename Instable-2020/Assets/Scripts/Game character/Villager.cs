using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    private Animator VilAnim;
    public List<Zombie> Zombies;
    private SpriteRenderer villagerspriteRenderer;
    private void Start()
    {
        VilAnim = GetComponent<Animator>();
        villagerspriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnBecameVisible()
    {
        AudioManager.instance.Play("Help me");
    }

    private void OnBecameInvisible()
    {
        try { AudioManager.instance.Stop("Help me"); }
        catch { Debug.Log("Villager is dead"); }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            if (transform.localPosition.x > FindObjectOfType<Hero>().transform.position.x)
            {
                transform.localPosition = new Vector2(transform.localPosition.x - 4f, transform.localPosition.y);
                villagerspriteRenderer.flipX = false;
            }
            else
            {
                transform.localPosition = new Vector2(transform.localPosition.x + 3f, transform.localPosition.y);
            }
            transform.SetParent(FindObjectOfType<Hero>().transform);
            VilAnim.SetBool("Walk", true);

            AudioManager.instance.Stop("Help me");
        }

        if (collision.gameObject.CompareTag("SafeZone"))
        {
            transform.parent = null;
            AudioManager.instance.Stop("Help me");
            AudioManager.instance.Play("Thank you");
            AudioManager.instance.Play("Safe zone");
            Destroy(gameObject, 0.4f);
            VilAnim.SetBool("Walk", false);
        }

        if (collision.gameObject.CompareTag("Virus") || collision.gameObject.CompareTag("Zombie"))
        {
            int i = Random.Range(0, Zombies.Count);
            Zombie newZombie = Instantiate<Zombie>(Zombies[i]);
            newZombie.transform.localPosition = new Vector2(transform.position.x, Zombies[i].transform.localPosition.y);
            newZombie.transform.SetParent(GameObject.FindGameObjectWithTag("ZombieManager").transform);
            AudioManager.instance.Stop("Help me");
            Destroy(gameObject);
        }
    }
}
