using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiplayerController : MonoBehaviour
{
    public Vector2 moveInput;
    public float speed = 5;
    PlayerInput input;
    public LocalMultiplayerMAnager manager;
    public AnimationCurve attackCurve;
    Coroutine attackSquish;
    Coroutine dashCoroutine;
    public GameObject dashTrail;

    public int health = 5;
    public bool isDashing = false;
    public bool isDead = false;

    private void Start()
    {
        input = GetComponent<PlayerInput>();
        dashTrail.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            isDead = true;
        }
        else { 
            isDead = false;
        }

        if (isDead)
        {
            GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            transform.position += (Vector3)moveInput * speed * Time.deltaTime;
        }            
    }

    public void OnMove(InputAction.CallbackContext context) { 
    
        moveInput = context.ReadValue<Vector2>();

    }

    public void OnInteract(InputAction.CallbackContext context) {
      
        if (context.started)
        {

            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }

            dashCoroutine = StartCoroutine(dash());

        }

    }

    public void OnAttack(InputAction.CallbackContext context) {

        if (context.performed) {

            if (attackSquish != null) { 
                StopCoroutine(attackSquish);
            }

            attackSquish = StartCoroutine(attackAnim());
            
            manager.PlayerAttack(input);
        
        }

    }

    IEnumerator attackAnim() { 
        
        float t = 0;

        while (t <= 1) {

            transform.localScale = Vector2.Lerp(Vector2.one, new Vector2(0.5f, 0.6f), t);

            t += Time.deltaTime *5;

            

            yield return null;
        }

        transform.localScale = Vector2.one;
    }

    IEnumerator dash() {

       
        speed *= 2;
        dashTrail.SetActive(true);

        yield return new WaitForSeconds(0.6f);
        

        speed /= 2;
        dashTrail.SetActive(false);
    }
}
