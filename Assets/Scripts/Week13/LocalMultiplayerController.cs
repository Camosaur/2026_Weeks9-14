using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiplayerController : MonoBehaviour
{
    public Vector2 moveInput;
    public float speed = 5;
    PlayerInput input;

    private void Start()
    {
        input = GetComponent<PlayerInput>();

        GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)moveInput * speed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context) { 
    
        moveInput = context.ReadValue<Vector2>();

    }

    public void OnAttack(InputAction.CallbackContext context) {

        if (context.performed) { Debug.Log("Player "+(input.playerIndex+1)+": Attacks!"); }

    }
}
