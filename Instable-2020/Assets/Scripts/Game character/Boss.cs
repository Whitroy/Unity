using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Boss : MonoBehaviour
{
    private Vector3 StartPos;
    private Vector3 target;
    public float speed;
    public GameObject Nextlvl;
    public GameObject BossFightTag=null;

    private bool moveUp;
    int i = 0;
    Vector3 centerPos;
    float radius;

    public int no_of_object = 8;
    public BossVirus Virusprefab;
    public Animator BossAnim;
    public Slider BossUIHealth;

    private int health = 250;
    private bool isdie = false, ismore = false,isStart=false;
    private Rigidbody2D rbBoss;
    private Vector3 forcedirection, Aim, direction;

    public string LevelPhase;

    public GameObject LabBg;

    private float timer = 0f;

    public GameObject[] Story;

    private void Awake()
    {
        AudioManager.instance.Stop("Main Theme");
        AudioManager.instance.Play("Theme1");
        FindObjectOfType<Hero>().Grenade = 2;
        FindObjectOfType<Hero>().Kills = 0;
        centerPos = transform.position;
        BossUIHealth.value = 250;
        rbBoss = GetComponent<Rigidbody2D>();
        AudioManager.instance.Play("Boss idle");
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
    }
    private void Start()
    {
        if (LevelPhase == "Phase1")
        {
            StartPos = new Vector2(-7.369f, -6.37f);
            target = new Vector2(transform.position.x, 0f);
            radius = 2f;
            moveUp = true;
            if (BossFightTag != null)
            {
                Invoke("TagRemove", 1f);
                isStart = true;
            }
            else
                isStart = true;
        }
        if (LevelPhase == "Phase2")
        {
            health = 150;
            BossUIHealth.value = health;
            no_of_object = 10;
        }
    }

    void TagRemove()
    {
        BossFightTag.SetActive(false);
        isStart = true;
    }

    private void Update()
    {
        if (!isdie && isStart)
        {
            if (LevelPhase == "Phase1")
            {
                float step = speed * Time.deltaTime;

                if (transform.position == target)
                {
                    moveUp = false;
                }
                else if (transform.position == StartPos)
                {
                    moveUp = true;
                }
                if (moveUp)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target, step);
                }
                else
                    transform.position = Vector3.MoveTowards(transform.position, StartPos, step);

                if ((centerPos.y < 2.03f && centerPos.y > 2f) || (centerPos.y > -2.03f && centerPos.y < -2f))
                {
                    if (FindObjectOfType<Hero>())
                    {
                        circleInstantiate();
                        BossAnim.SetTrigger("SpawnVirus");
                        AudioManager.instance.Play("Virus Spread");
                    }
                }
            }
        }
        centerPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (LevelPhase == "Phase2" && !isdie)
        {
            Jump();
        }

        timer += Time.deltaTime;
    }


    void Jump()
    {
        float angle = Mathf.PI / 2.2f;
        if (transform.position.y < -2f)
        {
            forcedirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
        else
        {
            forcedirection = new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle));
        }

        if (transform.position.x < -20f)
        {
            forcedirection.x *= 1;
        }
        else if (transform.position.x > -13f)
        {
            forcedirection.x *= -1;
        }
        Aim = new Vector2(transform.position.x + forcedirection.x, transform.position.y + forcedirection.y);
        direction = Aim - transform.position;

        rbBoss.AddForce((0.5f) * direction);

        if ((timer > 30f && timer < 40f) || (timer > 50f && timer < 60f))
        {
            ismore = true;
        }
        else
        {
            ismore = false;
        }

        if (transform.position.y < -3.7f && transform.position.y > -3.705f)
        {
            if (FindObjectOfType<Hero>())
            {
                circleInstantiate();
                AudioManager.instance.Play("Virus Spread");
            }
        }


        if (transform.position.y < -0.3f && transform.position.y > -.305f && ismore)
        {
            if (FindObjectOfType<Hero>())
            {
                circleInstantiate();
                AudioManager.instance.Play("Virus Spread");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            if (i % 2 == 0)
            {
                transform.position = new Vector3(-19.19f, 5.09f);
                StartPos = new Vector3(-19.19f, 6.37f);
                target.x = -19.19f;
                target.y = -4f;
                moveUp = true;
            }
            else
            {
                transform.position = new Vector3(-7.369f, -5.09f);
                StartPos = new Vector3(-7.369f, -6.37f);
                target.x = -7.369f;
                target.y = 4f;
                moveUp = true;
            }
            i++;
        }

        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Knife") || collision.gameObject.CompareTag("Range"))
        {
            BossAnim.SetTrigger("Hurt");
            if (collision.gameObject.CompareTag("Range"))
                health -= 10;
            else
                health -= 5;
            BossUIHealth.value = health;
            if (health < 151 && LevelPhase == "Phase1")
            {
                BossAnim.SetTrigger("Death");
                Nextlvl.SetActive(true);
                AudioManager.instance.Stop("Boss idle");
                AudioManager.instance.Play("Scientist die");
                ScreenPlay();
                Destroy(gameObject, 7f);
                isdie = true;
            }
            if (health < 1 && LevelPhase == "Phase2")
            {
                BossAnim.SetTrigger("Death");
                BossUIHealth.gameObject.SetActive(false);
                AudioManager.instance.Stop("Boss idle");
                AudioManager.instance.Play("Scientist die");
                Destroy(gameObject, 7f);
                ScreenPlay();
                isdie = true;
                LabBg.GetComponent<EdgeCollider2D>().enabled = false;
                FindObjectOfType<Camera>().transform.SetParent(FindObjectOfType<Hero>().transform);
            }

            Destroy(collision.gameObject);
        }
    }

    void circleInstantiate()
    {
        float angle = 2 * Mathf.PI / no_of_object;
        for (int i = 0; i < no_of_object; i++)
        {
            Vector3 direction = new Vector3(Mathf.Cos(angle * i), Mathf.Sin(angle * i)).normalized;
            Vector3 Position = centerPos + (direction * radius);

            BossVirus newVirus = Instantiate(Virusprefab, Position, Quaternion.identity);
            newVirus.direction(direction);
        }
    }
}
