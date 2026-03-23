using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToDraw : MonoBehaviour
{
    public LineRenderer lr;
    public Vector2 practice;
    public List<Vector2> points;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        points = new List<Vector2>();
        points.Add(transform.position);

        updateLineRendererList();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //add a new point into the line
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            points.Add((Vector2)mousePos);
        }

        //Remove a point from the line
        if (Mouse.current.rightButton.wasPressedThisFrame) {
            points.RemoveAt(0);
        }


        updateLineRendererList();
    }

    public void updateLineRendererList()
    {
        lr.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            lr.SetPosition(i, points[i]);
        }
    }





    //This was just for practice. This script does not use this input system.
    public void OnPoint(InputAction.CallbackContext context) {
        practice = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
}
