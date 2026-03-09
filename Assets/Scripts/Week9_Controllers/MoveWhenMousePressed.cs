using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveWhenMousePressed : MonoBehaviour
{
    public bool isMousePressed;
    public float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMousePressed)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    public void OnClick(InputAction.CallbackContext context) { 
        isMousePressed = context.performed;
    }

    public void OnSprint(InputAction.CallbackContext context) {
        speed++;
    }
}
