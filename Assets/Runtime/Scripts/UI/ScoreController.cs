using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreNumbers;
    [SerializeField] float speed = 1;

    public void SetScore(int score)
    {
        int currentScore = 0;
        if (System.Int32.TryParse(scoreNumbers.text, out currentScore))
        {
            StartCoroutine(CountScore(speed, currentScore, score));
        }
    }

    IEnumerator CountScore(float speed, int currentScore, int newScore)
    {
        for (int i = 0; i < newScore; i++)
        {
            currentScore += 1;
            scoreNumbers.text = currentScore.ToString();
            yield return new WaitForSeconds(speed);
        }

        yield return null;
    }
}
