using System.Collections;
using UnityEngine;

public class GrowManager : MonoBehaviour
{
    public Transform rockTransform;
    public Transform sigilTransform;

    public float sigilDelay = 1;
    bool isRunning = false; //Is the corutine running? This makes it ok to spam the button, so that there is only ever one instance of the corunine running at a time.


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rockTransform.localScale = Vector2.zero;
        sigilTransform.localScale = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startRockGrowing() {

            StartCoroutine(growRock());

        
    }

    IEnumerator growRock()
    {
        //if there is another instance of this corutine running, exit the corutine without yeilding
        if (isRunning)
        {
            yield break; //Thank you tooltips, I would not have known about this line
        }
        rockTransform.localScale = Vector2.zero;
        sigilTransform.localScale = Vector2.zero;
        float t = 0;

        isRunning = true;

        while (t < 1) { 
            t += Time.deltaTime;
            rockTransform.localScale = Vector2.one * t; 
            yield return null;
        }

        yield return new WaitForSeconds(sigilDelay);

        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            sigilTransform.localScale = Vector2.one * t;
            yield return null;
        }

        isRunning =false;
    }
}
