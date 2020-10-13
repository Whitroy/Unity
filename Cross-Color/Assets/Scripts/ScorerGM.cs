using TMPro;
using UnityEngine;

public class ScorerGM : MonoBehaviour
{
    public TextMeshProUGUI score=null;

    public TextMeshProUGUI highScorer=null;

    int Score,high;
    // Start is called before the first frame update
    void Start()
    {
        Score = PlayerPrefs.GetInt("Score");
        high = PlayerPrefs.GetInt("HighScore");
        if (score != null && highScorer != null)
        {
            Debug.Log(high);
            if (Score >= high)
            {
                highScorer.text = "New High Score!";
            }
            else
            {
                highScorer.text = "";
            }
            score.text = "" + Score;
            return;
        }

        if (highScorer != null)
        {
            highScorer.text = "High\n\t\tScore :- " + high;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
