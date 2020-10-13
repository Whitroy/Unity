using UnityEngine;
using System.Collections;
public class Gernade : MonoBehaviour
{
    public ParticleSystem Explosion;
    int i = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //wait for 1.5sec
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        Time.timeScale = 0.4f;
        yield return new WaitForSeconds(1.5f);

        //instaniate explosion
        ParticleSystem P = Instantiate<ParticleSystem>(Explosion, new Vector3(transform.position.x,transform.position.y+1f), Quaternion.identity);
        Destroy(P.gameObject,2f);
        //explosion sound
        if (i == 0)
        {
            AudioManager.instance.Play("Explosion");
            i++;
        }
        yield return new WaitForSeconds(2f);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }

}
