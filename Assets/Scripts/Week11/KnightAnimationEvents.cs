using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class KnightAnimationEvents : MonoBehaviour
{
    public AudioSource SFX;
    public float speed;

    Coroutine vortexMovement;

    public Vector2 mousePos;

    public Animator animator;

    public List<AudioClip> stepSounds;

    public SilentKnight grassChecker;

    public GameObject stomp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(moveToPoint(new Vector2(0, 0)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPoint(InputAction.CallbackContext context) {

        mousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void OnClick(InputAction.CallbackContext context) {

        if (grassChecker.isThisOnGrass(mousePos))
        {
            return;
        }

        if (vortexMovement != null) { 
            StopCoroutine(vortexMovement);
        }

        vortexMovement = StartCoroutine(moveToPoint(mousePos));

    }

    public void Footsteps() {

        if (grassChecker.isThisOnGrass(transform.position)) {
            return;
        }

        grassChecker.changeTile();
        GameObject woosh = Instantiate(stomp, transform.position, Quaternion.identity);
        Destroy(woosh, 1);
        SFX.pitch = Random.Range(1, 2);
        SFX.clip = stepSounds[Random.Range(0, stepSounds.Count)];
        SFX.Play();
        GetComponent<CinemachineImpulseSource>().GenerateImpulseWithForce(1);
    }

    //Use this in the project.... You'll have to remove the animation stuff, but it's perfect!
    IEnumerator moveToPoint(Vector2 pointToMoveTo) {
        animator.SetBool("IsMoving", true);
        //Find the direction to the spot
        Vector2 direction = pointToMoveTo - (Vector2)transform.position;

        //flip the rotation correctly
        if (direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else {
            GetComponent<SpriteRenderer>().flipX = false;
        }

            //So that it goes the intended speed
            direction = Vector2.Normalize(direction);

        //While it isn't at that spot
        while (Vector2.Distance(transform.position, pointToMoveTo) > 0.5f) {

            //Move toward that spot
            transform.position += (Vector3)direction * Time.deltaTime * speed;

            //Wait for the next frame
            yield return null;
        }

        animator.SetBool("IsMoving", false);

    }
}
