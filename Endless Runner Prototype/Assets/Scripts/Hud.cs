using UnityEngine;
using UnityEngine.UI;
public class Hud : MonoBehaviour
{
    public Text Distancetravelled;
    public Text Velocity;

    public void SetValue(float distancetravelled,float velocity)
    {
        Distancetravelled.text = ((int)(distancetravelled*10f)).ToString();
        Velocity.text = ((int)(velocity*10f)).ToString();
    }
}
