using UnityEngine;
using UnityEngine.Events;

public class Debris : MonoBehaviour
{
    public bool isEaten = false;

    public UnityEvent OnTouchingPlayer;
    public UnityEvent OnTouchingMouth;

    public Transform player;
    public Transform mouth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If it is touching the mouth or player, marks itself as eaton and raises the appropiate event

        //Mouth
        if (Vector2.Distance(transform.position, mouth.position) < 0.5f) {
            isEaten = true;
            OnTouchingMouth.Invoke();
        }


        //Player
        if (Vector2.Distance(transform.position, player.position) < 0.5f)
        {
            isEaten = true;
            OnTouchingPlayer.Invoke();
        }
    }
}
