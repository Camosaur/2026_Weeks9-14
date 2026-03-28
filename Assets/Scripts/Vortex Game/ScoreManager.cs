using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; //What's the score

    public bool isPlaying = false; //Is the game going, or are we in a "Game Over" state?

    public GameObject pressPlayUI; //The UI element that will only appear if we are in a Game Over state

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame() { 
    
        //Reset the score
        score = 0;

        //You are playing now
        isPlaying = true;

        //Hide the play button
        pressPlayUI.SetActive(false);

    }

    public void gameOver() { 
    
        isPlaying = false;
        pressPlayUI.SetActive(true);
    }

}
