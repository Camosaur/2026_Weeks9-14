using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VortexManager : MonoBehaviour
{
    //FOR VORTEX STRENGTH
    public float DefaultVortexStrength = 0.5f; //How strong the vortex is by default. Affected objects will be pulled this much every second.
    float VortexStrengthAddon = 0; //Adding onto the vortex strength, so that it can change during the game
    float VortexStrength; //The actual vortex strength, calculated in update for ease of use.

    //REFRENCES
    public Transform mouthTransform;
    public GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(pullToPoint(mouthTransform, player));
    }

    // Update is called once per frame
    void Update()
    {
        VortexStrength = DefaultVortexStrength + VortexStrengthAddon;
    }

    public void ChangeStrength(int ChangeByThisMuch) {
        
        //If the vortex will be greater than 0, change it by that much.
        if (VortexStrength + ChangeByThisMuch > 0) { 
            VortexStrengthAddon += ChangeByThisMuch;
        }
    }

    public void StartGame() {

        //Reset the vortex
        VortexStrengthAddon = 0;

        //Put the player at 0,0- it's starting position
        player.transform.position = Vector3.zero;
    }

    IEnumerator pullToPoint(Transform mouthTransform, GameObject pulledObject) {

        //While the object isn't marked as eaten
        while (!IsThisEaten(pulledObject))
        {
            //Find the direction to the spot
            Vector2 direction = mouthTransform.position - pulledObject.transform.position;

            //So that it goes the intended speed
            direction = Vector2.Normalize(direction);

            //Move toward that spot at a rate of VortexStength per second
            pulledObject.transform.position += (Vector3)direction * Time.deltaTime * VortexStrength;

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
        return pulledObject.GetComponent<Debris>().isEaten;

    }
}
