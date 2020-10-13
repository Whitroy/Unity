using UnityEngine;

public class Knife : MonoBehaviour
{
    private Rigidbody2D rbKnife;
    private Vector3 TargetPos;
    bool isfired = false;

    private void Start()
    {
        rbKnife = GetComponent<Rigidbody2D>();
    }

    public void LaunchedForce(Vector3 Target)
    {
        isfired = true;
        TargetPos = Target;
    }

    private void FixedUpdate()
    {
        if (isfired)
        {
            rbKnife.AddForce(TargetPos.normalized*9f, ForceMode2D.Impulse);
            isfired = false;
        }

        Destroy(gameObject, 2f);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bat") || collision.gameObject.CompareTag("Pipe"))
            Destroy(gameObject);
    }

}
