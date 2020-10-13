using UnityEngine;

public class Bat : MonoBehaviour
{
    private Rigidbody2D rbBat;
    private SpriteRenderer spriteRenderer;
    private Vector3 Pos;
    private float movementspeed = 1.5f;
    private float direction = 1f;
    private Vector3 Target;

    public Virus Prefab;
    public Hero PrefabHero;

    private Vector3 HeroPos;

    private void Start()
    {
        int RandomTime = Random.Range(1, 4);
        Pos = transform.position;
        rbBat = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;
        InvokeRepeating("SpawnVirus", 0f, (float)RandomTime);
        HeroPos = (FindObjectOfType<Hero>()).transform.position;
    }

    private void SpawnVirus()
    {
        if (HeroPos.x < Pos.x + 5 && HeroPos.x > Pos.x - 5f)
        {
            if (FindObjectOfType<Hero>())
            {
                Virus newVirus = Instantiate<Virus>(Prefab, transform.position, Quaternion.identity);
                Target = HeroPos - transform.position;
                newVirus.LaunchedForce(Target);
                newVirus.transform.SetParent(transform);
            }
        }
    }

    private void OnBecameVisible()
    {
        AudioManager.instance.Play("Bat flying");
    }

    private void FixedUpdate()
    {
        if (transform.position.x > Pos.x + 5f)
        {
            direction = -1f;
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x < Pos.x - 5f)
        {
            direction = 1f;
            spriteRenderer.flipX = true;
        }

        rbBat.velocity = new Vector3(direction * movementspeed, 0f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            AudioManager.instance.Play("Bat dying");
            AudioManager.instance.Stop("Bat flying");
            FindObjectOfType<Hero>().Kills += 1;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(FindObjectOfType<Hero>())
            HeroPos = (FindObjectOfType<Hero>()).transform.position;
    }
}
