using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiplayerMAnager : MonoBehaviour
{
    public List<Sprite> playerSprites;

    public List<PlayerInput> players;

    public GameObject dirt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerAttack(PlayerInput attacker) {

        //Var keyword was autocomplete but I looked it up!
        //It's datatype is determined by the compiler when it is first assigned, then it becomes a fixed variable
        //It only works for local variables
        //Neat!

        if (players.Count == 1 || attacker.GetComponent<LocalMultiplayerController>().isDead)
        {
            return;
        }

        foreach (var target in players) {

            if (attacker == target) continue;
            

            if (Vector2.Distance(attacker.transform.position, target.transform.position) <= 0.5f) {

                Debug.Log("Player "+ (attacker.playerIndex+1) + " hit Player " + (target.playerIndex + 1)+"!!");

                GetComponent<CinemachineImpulseSource>().GenerateImpulseWithForce(0.5f);

                LocalMultiplayerController targetBrain = target.GetComponent<LocalMultiplayerController>();

                if (!targetBrain.isDashing) targetBrain.health--;

                Destroy(Instantiate(dirt, target.GetComponent<Transform>().position, Quaternion.identity), 2);

            }

        
        }
    
    }

    public void OnPlayerJoined(PlayerInput player) { 
    
        players.Add(player);
        
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = playerSprites[player.playerIndex];

        LocalMultiplayerController controller = player.GetComponent<LocalMultiplayerController>();
        controller.manager = this;

    }
}
