using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Animator ZombieAnim;
    private SpriteRenderer spriteRendererBat;
    private bool ismovement = false, canstart = false;
    float Heropos_x;
    private Rigidbody2D rbZombie;
    int input_x;
    float movementspeed = 0.2f;
    int deathcount = 2;


    float timer = 0;
    bool isreducinghealth = false;


    public Sounds Sound, Soun1;
    private void Start()
    {
        ZombieAnim = GetComponent<Animator>();
        spriteRendererBat = GetComponent<SpriteRenderer>();
        rbZombie = GetComponent<Rigidbody2D>();
        rbZombie.gravityScale = 5f;
        ismovement = false;
        canstart = false;


        Sound.Source = gameObject.AddComponent<AudioSource>();
        Sound.Source.clip = Sound.Audio;
        Sound.Source.loop = Sound.loop;
        Sound.Source.volume = Sound.Volume;
        Sound.Source.pitch = Sound.pitch;
        Sound.Source.outputAudioMixerGroup = Sound.Output;

        Soun1.Source = gameObject.AddComponent<AudioSource>();
        Soun1.Source.clip = Soun1.Audio;
        Soun1.Source.loop = Soun1.loop;
        Soun1.Source.volume = Soun1.Volume;
        Soun1.Source.pitch = Soun1.pitch;
        Soun1.Source.outputAudioMixerGroup = Soun1.Output;

    }

    private void OnBecameVisible()
    {
        ismovement = true;
        Sound.Source.Play();
    }

    private void OnBecameInvisible()
    {
        ismovement = false;
        Sound.Source.Stop();
    }
    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        if (transform.position.x < 9.59f && transform.position.x > -137.22f && ismovement && canstart)
            rbZombie.velocity = new Vector2(input_x * movementspeed, 0f);
        else
        {
            rbZombie.velocity = Vector3.zero;
        }

        if (transform.position.y < -3.9f)
        {
            pos.y = -3.9f;
            transform.position = pos;
        }
    }

    private void Update()
    {
        if (ismovement && canstart)
        {
            if (Time.timeScale < .25f)
            {
                Sound.Source.Stop();
                Soun1.Source.Stop();
            }

            if (FindObjectOfType<Hero>())
            {
                Heropos_x = FindObjectOfType<Hero>().transform.localPosition.x;
                ZombieAnim.SetFloat("speed", Mathf.Abs(rbZombie.velocity.x));

                if (Heropos_x < transform.localPosition.x)
                {
                    spriteRendererBat.flipX = true;
                    input_x = -1;
                }
                else
                {
                    spriteRendererBat.flipX = false;
                    input_x = 1;
                }
            }
        }

        if (transform.position.y < -3.63f && canstart)
        {
            Vector3 pos = transform.localPosition;
            pos.y = -3.63f;
            transform.localPosition = pos;
        }

        if(isreducinghealth)
        {
            timer += Time.time;
            if(timer>200f)
            {
                FindObjectOfType<Hero>().herohealth--;
                timer = 0;
            }
        }

        if(!FindObjectOfType<Hero>())
        {
            Sound.Source.Stop();
            Soun1.Source.Stop();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Knife") || collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            deathcount--;
            if (deathcount < 1)
            {
                transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.45f);
                ZombieAnim.SetTrigger("Die");
                AudioManager.instance.Play("Zombie Die");
                FindObjectOfType<Hero>().CancelInvoke("Hurt");
                FindObjectOfType<Hero>().Kills+= 1;
                Destroy(gameObject, 0.7f);
            }
        }

        if (collision.gameObject.CompareTag("BackgroundSurface"))
        {
            canstart = true;
            rbZombie.gravityScale = 1f;
        }

        if (collision.gameObject.CompareTag("Hero"))
        {
            Sound.Source.Stop();
            ZombieAnim.SetTrigger("Attack");
            Soun1.Source.Play();
            isreducinghealth = true;
            timer = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            ZombieAnim.SetTrigger("Attack");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            Sound.Source.Play();
            Soun1.Source.Stop();
            isreducinghealth = false;
            timer = 0;
        }
    }
}
