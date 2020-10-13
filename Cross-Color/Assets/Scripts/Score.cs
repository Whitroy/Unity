using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI text;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            text.text = "" + PlayerPrefs.GetInt("Score");
        }
    }
}
