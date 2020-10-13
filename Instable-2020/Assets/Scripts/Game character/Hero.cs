using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Hero : MonoBehaviour
{
    public Animator HeroAnim;
    public float movementspeed = 0.1f;
    public Knife KnifePrefab;
    public Bullet bulletPrefab;
    public GameObject GernadePf;

    //Vector3 Target, mousepos;
    private Rigidbody2D rbHero;
    private float input_x;
    private SpriteRenderer spriteRenderer;

    bool isStart = false;
    bool isgernadelaunch = false;

    public Sounds[] sounds;

    //UI
    private Vector2 intialspawnpos;
    private int HeroHealth = 10;
    private int chances=2;
    private int bullets = 40;
    private int knife = 30;
    private int grenade = 0;
    private int kills;

    public Vector2 IntialSpawn_X
    {
        get
        {
            return intialspawnpos;
        }
    }
    public int herohealth
    {
        set
        {
            HeroHealth = value;
        }

        get
        {
            return HeroHealth;
        }
    }

    public int Chances
    {
        set
        {
            chances = value;
        }

        get
        {
            return chances;
        }
    }
    public int Knife
    { 
        set
        {
            knife = value;
        }

        get
        {
            return knife;
        }

    }

    public int Bullets
    {
        set
        {
            bullets = value;
        }

        get
        {
            return bullets;
        }
    }

    public int Grenade
    {
        set
        {
            grenade = value;
        }

        get
        {
            return grenade;
        }
    }
    public int Kills
    {
        set
        {
            kills = value;
        }

        get
        {
            return kills;
        }

    }

    public int Input_move
    {
        set
        {
            input_x = value;
        }
    }
    private void Awake()
    {
        PlayerPrefs.SetString("ContinueScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        foreach (Sounds s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Audio;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.pitch;
            s.Source.loop = s.loop;
            s.Source.outputAudioMixerGroup = s.Output;
        }

        rbHero = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rbHero.gravityScale = 0.3f;


        if (PlayerPrefs.HasKey("Purchased Grenade"))
        {
            if(PlayerPrefs.GetInt("Purchased Grenade")>0)
            {
                Grenade += PlayerPrefs.GetInt("Purchased Grenade");
                PlayerPrefs.SetInt("Purchased Grenade", 0);
            }

        }
        else
        {
            Debug.Log("No purchased item");
        }
    }
    private void Start()
    {
        intialspawnpos = this.transform.position;
    }

    void Play(string name)
    {
        foreach (Sounds s in sounds)
        {
            if (s.name == name)
            {
                s.Source.Play();
                break;
            }
        }
    }

    void Stop(string name)
    {
        foreach (Sounds s in sounds)
        {
            if (s.name == name)
            {
                s.Source.Stop();
                break;
            }
        }
    }

    private void Update()
    {
        if (isStart)
        {
            if(Time.timeScale<.25f)
            {
                Stop("Hurt");
                Stop("Walk");
            }
            HeroAnim.SetFloat("Speed", Mathf.Abs(rbHero.velocity.x));

            if (input_x > 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (input_x < 0)
            {
                spriteRenderer.flipX = false;
            }

        }

        if(Time.timeScale!=1f)
        {
            StartCoroutine(NormalTime());
        }

        if(HeroHealth<0)
        {
            Joysticks[] joysticks = FindObjectsOfType<Joysticks>();
            foreach (Joysticks j in joysticks)
            {
                j.Pressed = false;
            }
            FindObjectOfType<GameOver>().afterdestroy(GetComponent<Hero>());
        }
    }

    IEnumerator NormalTime()
    {
        yield return new WaitForSeconds(5f);
        Time.timeScale = 1f;
    }

    public void GernadeThrow()
    {
        if (!isgernadelaunch && this.Grenade>0)
        {
            isgernadelaunch = true;
            Gernade();
            StartCoroutine(LaunchAgain());
            this.Grenade--;
        }
        else
        {
            Debug.Log("Grenade is empty");
        }
    }
    
    IEnumerator LaunchAgain()
    {
        yield return new WaitForSeconds(3.5f);

        isgernadelaunch = false;
    }

    private void FixedUpdate()
    {
        if (isStart)
        {
            Vector3 pos = transform.position;
            if (transform.position.x < 9.59f && transform.position.x > -137.22f)
            {
                rbHero.velocity = new Vector2(input_x * movementspeed, 0f);
                if (input_x == 0)
                {
                    Play("Walk");
                    foreach (Sounds S in sounds)
                    {
                        if (S.name == "Walk")
                        {
                            S.Source.volume = 0f;
                        }
                    }
                }
                else
                {
                    foreach (Sounds S in sounds)
                    {
                        if (S.name == "Walk")
                        {
                            S.Source.volume = .2f;
                        }
                    }
                }
            }
            else
            {
                rbHero.velocity = Vector2.zero;
                if (pos.x > 0)
                    pos.x -= 0.1f;
                else
                    pos.x += 0.1f;
                transform.position = pos;
            }

            if (transform.position.y < -3.9f)
            {
                pos.y = -3.9f;
                transform.position = pos;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            if (collision.gameObject)
            {
                HeroAnim.SetTrigger("Hurt");

                foreach (Sounds S in sounds)
                {
                    if (S.name == "Hero Hurt")
                    {
                        S.Source.volume = 0.15f;
                        S.Source.loop = true;
                        StartCoroutine(StopSound(S));
                    }
                }
                Play("Hero Hurt");
            }
        }

        if (collision.gameObject.CompareTag("Knife"))
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        if (collision.gameObject.CompareTag("Fire") || collision.gameObject.CompareTag("Virus"))
        {
            HeroAnim.SetTrigger("Hurt");
            Destroy(collision.gameObject);
            foreach (Sounds S in sounds)
            {
                if (S.name == "Hero Hurt")
                {
                    S.Source.volume = 0.5f;
                    S.Source.loop = false;
                }
            }
            Play("Hero Hurt");
            HeroHealth--;
        }

        if (collision.gameObject.CompareTag("BackgroundSurface"))
        {
            if (!isStart)
            {
                AudioManager.instance.Play("Ground hit");
                isStart = true;
                rbHero.gravityScale = 12f;
            }
        }
    }

    private IEnumerator StopSound(Sounds S)
    {
        yield return new WaitForSeconds(3f);
        S.Source.loop = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Virus") || collision.gameObject.CompareTag("Acid") || collision.gameObject.CompareTag("Fire") || collision.gameObject.CompareTag("Laser"))
        {
            HeroAnim.SetTrigger("Hurt");
            if (collision.gameObject.CompareTag("Virus") || collision.gameObject.CompareTag("Fire") || collision.gameObject.CompareTag("Laser"))
            {
                Destroy(collision.gameObject);
                HeroHealth--;
            }
            foreach (Sounds S in sounds)
            {
                if (S.name == "Hero Hurt")
                {
                    S.Source.volume = 0.5f;
                    if (!collision.gameObject.CompareTag("Acid"))
                        S.Source.loop = false;
                    else
                        S.Source.loop = true;
                }
            }
            Play("Hero Hurt");
            HeroHealth--;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Acid"))
        {
            HeroAnim.SetTrigger("Hurt");
            HeroHealth--;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            HeroAnim.SetTrigger("Hurt");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            foreach (Sounds S in sounds)
            {
                if (S.name == "Hero Hurt")
                {
                    S.Source.volume = 0f;
                    S.Source.loop = false;
                }
            }
        }
    }
    public void SpawnBullet()
    {
        if (FindObjectsOfType<Bullet>().Length < 5)
        {
            if (this.Bullets > 0)
            {
                HeroAnim.SetTrigger("attack");
                Vector3 pos = transform.position;
                if (spriteRenderer.flipX)
                    pos.x += 1.36f;
                else
                    pos.x -= 1.36f;
                pos.y = FindObjectOfType<Hero>().transform.position.y - 0.41f;
                Bullet newbullet = Instantiate<Bullet>(bulletPrefab, pos, Quaternion.identity);
                newbullet.setdirection(spriteRenderer.flipX);
                Play("Bullet Fire");
                this.Bullets--;
            }
            else
                Debug.Log("Gun is Empty");
        }
    }

    public void SpawnKnife(Vector2 dir,int move)
    {
        if (this.Knife > 0)
        {
            Vector3 pos = transform.position;
            if (move > 0)
                pos.x += 1.6f;
            else if (move < 0)
                pos.x -= 1f;
            Knife newknife = Instantiate<Knife>(KnifePrefab, pos, Quaternion.identity);
            //Target = mousepos - transform.position;
            newknife.LaunchedForce(dir);
            Play("Knife throwing");
            this.Knife--;
        }
        else
            Debug.Log("Knife Crate is empty");
    }

    public void Gernade()
    {
        int direction = 0;
        Vector3 pos = transform.position;
        if (spriteRenderer.flipX)
            pos.x += .36f;
        else
            pos.x -= .36f;

        if (spriteRenderer.flipX)
            direction = 1;
        else
            direction = -1;
        pos.y = FindObjectOfType<Hero>().transform.position.y + 1f;
        GameObject G = Instantiate<GameObject>(GernadePf, pos, Quaternion.identity);

        Rigidbody2D rigidbody = G.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(new Vector2(direction*Mathf.Cos(Mathf.PI / 4), Mathf.Sin(Mathf.PI / 4)).normalized * 8f,ForceMode2D.Impulse);
    }
}
