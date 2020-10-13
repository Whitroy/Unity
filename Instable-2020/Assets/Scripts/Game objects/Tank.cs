using UnityEngine;

public class Tank : MonoBehaviour
{
    public Fire FirePrefab;
    public Hero HeroPrefab;

    private Animator TankAnim;
    private Vector3 HeroPos;
    private Vector3 Pos;
    private SpriteRenderer spriteRenderer;
    public ParticleSystem TankExplosion;
    int hit = 2;
    private void Start()
    {
        TankAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("SpawnFire", 0f, 3f);
        HeroPos = (FindObjectOfType<Hero>()).transform.position;
        Pos = transform.position;
    }
    private void SpawnFire()
    {
        if (HeroPos.x < Pos.x + 9f && HeroPos.x > Pos.x - 9f && !FindObjectOfType<Fire>())
        {
            Pos = transform.position;
            TankAnim.SetTrigger("Fire");
            if (spriteRenderer.flipX)
                Pos.x += 1.5f;
            else
                Pos.x -= 1.5f;

            Fire newFire = Instantiate<Fire>(FirePrefab, Pos, Quaternion.identity);
            newFire.setdirection(spriteRenderer.flipX);
            AudioManager.instance.Play("Fire Launch");
        }
    }

    private void Update()
    {
        if(FindObjectOfType<Hero>())
            HeroPos = (FindObjectOfType<Hero>()).transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            TankAnim.SetTrigger("Hurt");
            hit--;
            Destroy(collision.gameObject);
            if (hit < 1)
            {
                AudioManager.instance.Play("Blast");
                Instantiate<ParticleSystem>(TankExplosion, transform.position, Quaternion.identity);
                FindObjectOfType<Hero>().Kills += 1;
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Knife") || collision.gameObject.CompareTag("Bullet"))
            Destroy(collision.gameObject);
    }
}
