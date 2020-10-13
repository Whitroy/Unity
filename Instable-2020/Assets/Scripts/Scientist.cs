using System.Collections;
using UnityEngine;
public class Scientist : MonoBehaviour
{
    private bool isfiring = false;
    private bool isdie = false;
    private bool wait = false;
    private int hit = 5;
    private Animator ScieAnim;
    private SpriteRenderer scieSR;


    public Laser laserPf;
    private void OnBecameVisible()
    {
        isfiring = true;
        scieSR = GetComponent<SpriteRenderer>();
        ScieAnim = GetComponent<Animator>();
    }

    private void OnBecameInvisible()
    {
        isfiring = false;
        if(isdie)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isfiring = false;
    }

    private void Update()
    {
        if(isfiring && !isdie)
        {
            if (!wait)
            {
                //start firing after each 3 sec
                wait = true;
                FireLaser();
                if (wait)
                    StartCoroutine(waitforsec());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Knife"))
        {
            Destroy(collision.gameObject);
            hit--;
            if(hit<1)
            {
                isdie = true;
                Destroy(gameObject.GetComponent<Collider2D>());
                FindObjectOfType<Hero>().Kills += 1;
                AudioManager.instance.Play("Scientist die");
                ScieAnim.SetBool("Die", true);
            }
        }
    }

    public void FireLaser()
    {
        //HeroAnim.SetTrigger("attack");
        Vector3 pos = transform.position;
        if (scieSR.flipX)
            pos.x -= 1.38f;
        else
            pos.x += 1.38f;
        pos.y = transform.position.y - 0.41f;
        Laser newlaser = Instantiate<Laser>(laserPf, pos, Quaternion.identity);
        newlaser.setdirection(!scieSR.flipX);
    }

    IEnumerator waitforsec()
    {
        AudioManager.instance.Play("Laser");

        yield return new WaitForSeconds(3f);

        wait = false;
    }
}
