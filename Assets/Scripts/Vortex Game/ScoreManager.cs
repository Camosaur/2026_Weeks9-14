using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; //What's the score

    public bool isPlaying = false; //Is the game going, or are we in a "Game Over" state?

    public GameObject pressPlayUI; //The UI element that will only appear if we are in a Game Over state

    public TextMeshProUGUI scoreText; //The UI text which will display score

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame() {

        //DO NOT START REPEAT GAMES
        if (isPlaying)
        {
            return;
        }
    
        //Reset the score
        score = 0;

        //You are playing now
        isPlaying = true;

        //Hide the play button
        pressPlayUI.SetActive(false);

        //Start the scoreCounter
        StartCoroutine(scoreCounter());

    }

    public void gameOver() { 
    
        isPlaying = false;
        pressPlayUI.SetActive(true);
    }

    IEnumerator scoreCounter() {

        //Every 1 second until the game ends, count score by 1
        yield return new WaitForSeconds(1);

        while (isPlaying)
        {
            score++;
            scoreText.text = "Score: "+score;
            yield return new WaitForSeconds(1);
        }
    }

}
