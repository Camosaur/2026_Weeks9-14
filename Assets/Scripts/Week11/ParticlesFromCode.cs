using UnityEngine;
using UnityEngine.InputSystem;

public class ParticlesFromCode : MonoBehaviour
{
    public ParticleSystem particles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            particles.Emit(800);
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            particles.Play();
        }

        if (Mouse.current.middleButton.wasPressedThisFrame)
        {
            particles.Stop();
        }
    }
}
