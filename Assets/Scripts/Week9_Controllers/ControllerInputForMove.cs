using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ControllerInputForMove : MonoBehaviour
{
    public float speed = 5;
    public Vector2 movement;
    public Vector2 position;
    public AudioSource SFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)position * speed * Time.deltaTime;
        //transform.position = position;
    }

    public void OnPoint(InputAction.CallbackContext context) { 
        movement = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
    public void OnMove(InputAction.CallbackContext context) {
        position = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context) {
        if (context.performed) { 
            SFX.Play();
        }
    }
}
