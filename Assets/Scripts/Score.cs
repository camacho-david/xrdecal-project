using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    int score = 0;
    bool over = false;

	// Start is called before the first frame update
	void Start()
        {
            scoreText.text = score.ToString();
        over = false;

        }

    public void AddPoint()
	{
        if (!over)
        {
            score += 1;
            scoreText.text = score.ToString();
        }

    }
    public void GameOver(int total)
    {
        scoreText.text = "Game Over" + "\n" + score.ToString() + "/" + total.ToString();

        score = 0;
        over = true;
    }
}
