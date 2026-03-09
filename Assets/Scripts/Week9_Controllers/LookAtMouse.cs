using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class LookAtMouse : MonoBehaviour
{
    public Vector2 mousePos; //The position of the mouse

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Find the direction to the mouse cursor
        Vector2 direction = mousePos - (Vector2)transform.position;

        //Make the rotation point at that!
        transform.up = direction;
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
}
