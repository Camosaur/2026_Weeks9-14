using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DanceManager : MonoBehaviour
{
    public AnimationCurve danceCurve;
    
    public Button player1Button;
    public Button player2Button;

    public bool isPlayer1Turn = true;
    public bool isPlayer2Turn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateButtons(isPlayer1Turn, isPlayer2Turn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateButtons(bool player1, bool player2) { 
        player1Button.interactable = player1;
        player2Button.interactable = player2;
    }

    public void startDancing(Transform playerPos) {

        StartCoroutine(dance(playerPos));
        
    }

    IEnumerator dance(Transform playerPos) {
        
        //Set both buttons to off interactible
        updateButtons(false, false);

        //Switch the player turn
        isPlayer1Turn = !isPlayer1Turn;
        isPlayer2Turn = !isPlayer2Turn;

        //Do the dance, and wait for it to finish
        yield return StartCoroutine(shimmy(playerPos, 1));
        yield return StartCoroutine(shimmy(playerPos, -1));

        //Set the correct button interactivity
        updateButtons(isPlayer1Turn, isPlayer2Turn);
    }

    IEnumerator shimmy(Transform playerPos, float dir) {
        float t = 0;
        Vector3 startPos = playerPos.position;

        while (t <= 1) {
            playerPos.position = Vector3.LerpUnclamped(startPos, startPos + new Vector3(1*dir, 0, 0), danceCurve.Evaluate(t));
            t += Time.deltaTime;
            yield return null;
        }

        
    }
}
