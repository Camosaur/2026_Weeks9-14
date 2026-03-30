using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class VortexManager : MonoBehaviour
{
    //FOR VORTEX STRENGTH
    public float DefaultVortexStrength = 0.5f; //How strong the vortex is by default. Affected objects will be pulled this much every second.
    float VortexStrengthAddon = 0; //Adding onto the vortex strength, so that it can change during the game
    public float VortexStrength; //The actual vortex strength, calculated in update for ease of use.

    //REFRENCES
    public Transform mouthTransform;
    public GameObject player;

    public GameObject defaultCandy;
    public GameObject defaultBomb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Pull the player
        StartCoroutine(pullToPoint(mouthTransform, player));

        //Spawn debris
        StartCoroutine(spawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        //Update the vortex strength
        VortexStrength = DefaultVortexStrength + VortexStrengthAddon;

        //if (Mouse.current.leftButton.wasPressedThisFrame)
        //{

        //    spawnDebris(6);

        //}
    }

    public void ChangeStrength(float ChangeByThisMuch) {
        
        //Change it, but don't go below the default value.
        if (VortexStrengthAddon + ChangeByThisMuch > 0) { 
            VortexStrengthAddon += ChangeByThisMuch;
        }
    }

    public void StartGame() {

        //DO NOT START REPEAT GAMES
        if (GetComponent<ScoreManager>().isPlaying)
        {
            return;
        }

        //Reset the vortex
        VortexStrengthAddon = 0;

        //Put the player at it's starting position
        player.transform.position = new Vector3(0, 4, 0);
    }

    IEnumerator pullToPoint(Transform mouthTransform, GameObject pulledObject) {

        //While the object isn't marked as eaten
        while (!IsThisEaten(pulledObject))
        {
            if (GetComponent<ScoreManager>().isPlaying) {

                //Find the direction to the spot
                Vector2 direction = mouthTransform.position - pulledObject.transform.position;

                //So that it goes the intended speed
                direction = Vector2.Normalize(direction);

                //Move toward that spot at a rate of VortexStength per second
                pulledObject.transform.position += (Vector3)direction * Time.deltaTime * VortexStrength;

            }

            //Wait for the next frame
            yield return null;
        }

        //When the gameObject marks itself as eaton, destroy it and end the coroutine. This will never happen for the player
        GameObject.Destroy(pulledObject);
    }

    public bool IsThisEaten(GameObject pulledObject)
    {
        //This function exists to determine weather this object is the player (return false for not eaten)
        //If it isn't return false if it is marked as eaten

        //Is this the player?
        if (pulledObject.GetComponent<Debris>() == null) {
            return false;
        }

        //It is not the player. Return weather this debris is eaten

        //Also return true if the game is over
        if (!GetComponent<ScoreManager>().isPlaying)
        {
            return true;
        }

        return pulledObject.GetComponent<Debris>().isEaten;

    }

    public void spawnDebris(int amount) {
        
        //Randomize the spawn between bomb and candy. 1 in x chance for candy
        GameObject newDebris;

        if (Random.Range(0, 2) < 1)
        {
            newDebris = defaultCandy;
        }
        else {
            newDebris = defaultBomb;
        }

        //Determine weather you are spawning on the sides or top/bottem
        float startXPos;
        float startYPos;

        if (Random.Range(0, 2) < 1)
        {
            //We're spawning on the sides
            startXPos = -10;
            if (Random.Range(0, 2) < 1)
            {
                startXPos *= -1;
            }

            startYPos = Random.Range(-6, 6);
        }
        else {
            //We're spawning on the top/bottem
            startYPos = -6;
            if (Random.Range(0, 2) < 1)
            {
                startYPos *= -1;
            }
            startXPos = Random.Range(-10, 10);
        }


        //Instantiate that object, and a corutine to handle it's movement and deletion
        StartCoroutine(pullToPoint(mouthTransform, Instantiate(newDebris, new Vector3(startXPos, startYPos, 0), Quaternion.identity)));

        //Recurse until the amount is satisfied
        if (amount > 1) {
            spawnDebris(amount - 1);
        }
        
    }

    IEnumerator spawnEnemies() {

        ScoreManager scoreTracker = GetComponent<ScoreManager>();

        while (true) {

            yield return new WaitForSeconds(Random.Range(0.5f, 6-Mathf.Clamp(scoreTracker.score / 10, 0, 5)));
            if (scoreTracker.isPlaying) {
                spawnDebris(Random.Range(1, 2));
            }
        
        }
    
    }
}
