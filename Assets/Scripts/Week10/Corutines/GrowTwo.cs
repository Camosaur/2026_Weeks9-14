using UnityEngine;
using System.Collections;


public class GrowTwo : MonoBehaviour
{
    public Transform rockTransform; //The transform of the rock, so I can change it's scale
    public Transform sigilTransform; //The transform of the sigil, so I can change it's scale

    private Coroutine growingCoroutine;
    Coroutine rockGrowCoroutine;
    Coroutine sigilGrowCoroutine;

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

    public void startRockGrowing()
    {

        if (growingCoroutine != null) { StopCoroutine(growingCoroutine); }
        if (rockGrowCoroutine != null) { StopCoroutine(rockGrowCoroutine); }
        if (sigilGrowCoroutine != null) { StopCoroutine(sigilGrowCoroutine); }

        growingCoroutine = StartCoroutine(grow());
    }

    IEnumerator grow()
    {


        //Reset the values of the objects, so that they can be full-size in the editor
        rockTransform.localScale = Vector2.zero;
        sigilTransform.localScale = Vector2.zero;

        yield return rockGrowCoroutine = StartCoroutine(growRock());
        yield return sigilGrowCoroutine= StartCoroutine(growSigil());
    }

    IEnumerator growRock()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;
            rockTransform.localScale = Vector2.one * t;
            yield return null;
        }
    }
    IEnumerator growSigil()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;
            sigilTransform.localScale = Vector2.one * t;
            yield return null;
        }
    }
}
