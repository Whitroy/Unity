using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class BossScientist : MonoBehaviour
{ 
    Animator BossAnim;

    public string[] Dialog;
    public Laser laserPf;
    public GameObject Dialouge_Box;
    public TextMeshProUGUI Dialogues;
    public Antitode antitode;
    public Slider healthslider;

    public GameObject Joystick1, joystick2, FireButton, CounterButton,Grenadebutton;
    // Start is called before the first frame update
    int health = 30;

    private Vector3 Pos;
    private Vector3 TargetPos;

    private SpriteRenderer thisobjSR;

    bool firepos = false,firetime=false,isstart=false;
    int i;
    void Start()
    {
        AudioManager.instance.Play("Theme1");
        AudioManager.instance.Stop("Main Theme");
        Joystick1.SetActive(false);
        joystick2.SetActive(false);
        FireButton.SetActive(!true);
        CounterButton.SetActive(!true);
        Grenadebutton.SetActive(!true);
        BossAnim = GetComponent<Animator>();
        BossAnim.SetTrigger("Boss");
        thisobjSR = GetComponent<SpriteRenderer>();
        Invoke("StartIdlemotion", 6f);
        FindObjectOfType<Hero>().Grenade = 3;
    }

    void StartIdlemotion()
    {
        Dialouge_Box.SetActive(true);
        StartCoroutine(PrintDialog());
    }

    IEnumerator PrintDialog()
    {
        for (i = 0; i < 3; i++)
        {
            foreach (char ch in Dialog[i].ToCharArray())
            {
                Dialogues.text += ch;
                yield return new WaitForSeconds(.15f);
            }
            Dialogues.text += '\n';
        }

        Dialouge_Box.SetActive(false);
        Joystick1.SetActive(true);
        joystick2.SetActive(true);
        FireButton.SetActive(true);
        CounterButton.SetActive(true);
        Grenadebutton.SetActive(true);
        BossAnim.SetBool("Idle2", true);
        AudioManager.instance.Play("Jetpack");
        yield return new WaitForSeconds(1f);
        isstart = true;
    }

    void Fire()
    {
        if(firepos && isstart && FindObjectOfType<Hero>())
        {
            Vector3 direction = (TargetPos - Pos).normalized;

            Laser laser = Instantiate<Laser>(laserPf, new Vector2(transform.position.x - 1f, transform.position.y), Quaternion.identity);

            laser.setdirectionBoss(direction);

            AudioManager.instance.Play("Laser");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Pos = transform.position;
        if(FindObjectOfType<Hero>())
            TargetPos = FindObjectOfType<Hero>().transform.position;
        firepos = thisobjSR.flipX;

        if(!firetime)
        {
            firetime = true;
            Fire();

            StartCoroutine(Waitfornextattack());
        }

        healthslider.value = health;
    }

    IEnumerator Waitfornextattack()
    {
        yield return new WaitForSeconds(2.5f);

        firetime = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Knife") || collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Range"))
            health -=2;

        if (health == 0)
        {
            isstart = false;
            BossAnim.SetBool("Idle2", false);
            AudioManager.instance.Stop("Jetpack");
            AudioManager.instance.Play("Scientist die");
            Dialouge_Box.SetActive(true);

            Destroy(GetComponent<Collider2D>());
            StartCoroutine(PrintEndDialog());
        }

        if (collision.gameObject.CompareTag("Hero"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.GetComponent<Collider2D>());
            FindObjectOfType<Hero>().herohealth--;
        }
    }


    IEnumerator PrintEndDialog()
    {
        Dialogues.text = "";
      foreach (char ch in Dialog[i].ToCharArray())
        {   Dialogues.text += ch;
                yield return new WaitForSeconds(.15f);
        }
        Dialouge_Box.SetActive(false);
        Instantiate<Antitode>(antitode, new Vector3(transform.position.x, -4.98f), Quaternion.identity);

        Destroy(gameObject);
    }
}
