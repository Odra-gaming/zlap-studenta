using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Text scoreText;

    public Text lifeText;

    public Text congratsText;


    public void setWin()
    {
        congratsText.gameObject.SetActive(true);
        Invoke(nameof(noWinYet), 3.0f);

    }
    public void noWinYet()
    {
        congratsText.gameObject.SetActive(false);

    }
    public void setScore(int score)
    {
        scoreText.text = "SCORE: " + score;

    }

    public void setLives(int lives)
    {
        lifeText.text = "LIVES: " + lives;
    }

}
