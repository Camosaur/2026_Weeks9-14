using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerControls : MonoBehaviour
{
    //Related to handling movement input
    Vector2 positionChange = Vector2.zero;
    public bool isMoving = true;
    
    //Related to speed
    public float baseSpeed = 1;
    public float accelerationBonus = 0;
    public AnimationCurve accelerationCurve;
    
    //Coroutines
    Coroutine movementCooldownCorutine = null;
    Coroutine accelerationCoroutine = null;

    //Changing Sprites
    SpriteRenderer costume;
    public Sprite normal;
    public Sprite stunned;

    //Refrence to the mouth
    public Transform mouth;
    public UnityEvent OnTouchingMouth;

    //Scoretracker referebce
    public ScoreManager scoreManager;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        costume = GetComponent<SpriteRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //---CHECKING FOR GAMER OVER---
        if (Vector2.Distance(transform.position, mouth.position) < 0.5f)
        {
            OnTouchingMouth.Invoke();
        }

        if (isMoving && scoreManager.isPlaying) {
            //---MOVING THE SPRITE IN SCENE---

            //Save the new position temporaraly
            Vector2 tempPos = transform.position + (Vector3)positionChange * (baseSpeed+accelerationBonus) * Time.deltaTime;

            //Use the temp variable and the current position to change the sprite's rotation
            transform.up = tempPos - (Vector2)transform.position;

            //If your are still whithin the bounds of the screen, Set the new position

            //X axis...
            if (!(Camera.main.WorldToScreenPoint(tempPos).x > 0 && Camera.main.WorldToScreenPoint(tempPos).x < Screen.width))
            {
                tempPos.x = transform.position.x;
            }

            //Y axis!
            if (!(Camera.main.WorldToScreenPoint(tempPos).y > 0 && Camera.main.WorldToScreenPoint(tempPos).y < Screen.height))
            {
                tempPos.y = transform.position.y;
            }

            //Set the new position
            transform.position = tempPos;
        }
    }

    //This is responsible for recording the player's input-based movement, and handling it's accleleration coroutine.
    public void OnMove(InputAction.CallbackContext context)
    {
        //When the player inputs for move, record it's intended change in position for this frame
        positionChange = context.ReadValue<Vector2>();

        //When this is started, call the acceleration coroutine
        if (context.started) {
            accelerationCoroutine = StartCoroutine(acceleration());
        }

        //When this is over, end the acceleration corutine
        if (context.canceled)
        {
            StopCoroutine(accelerationCoroutine);
        }

        
    }

    //This is called when a bomb hits the player. It starts/restarts the movementCooldown coroutine.
    public void OnTouchingBomb(float cooldown) {

        //Start OR restart the movement cooldown coroutine
        if (movementCooldownCorutine != null)
        {
            StopCoroutine(movementCooldownCorutine);
        }
        movementCooldownCorutine = StartCoroutine(movementCooldown(cooldown));
    }

    //Make the player unable to move for a certain amount of seconds
    IEnumerator movementCooldown(float cooldown) { 
        
        //Set isMoving to false and change the costume -> wait for a certain time -> then set them back.


        isMoving = false;
        costume.sprite = stunned;

        yield return new WaitForSeconds(cooldown);

        isMoving = true;
        costume.sprite = normal;
    }

    //Make the player "accelerate" by changing it's speed along an animationCurve
    IEnumerator acceleration() {

        float t = 0;

        while (true) { 
            accelerationBonus = accelerationCurve.Evaluate(t);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
