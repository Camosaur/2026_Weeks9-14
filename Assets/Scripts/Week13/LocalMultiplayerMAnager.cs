using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiplayerMAnager : MonoBehaviour
{
    public List<Sprite> playerSprites;

    public List<PlayerInput> players;

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
        //It's datatype is determined by the compiler when it is assigned, then it becomes a fixed variable
        //Neat!
        foreach (var target in players) {

            if (attacker == target) continue;
            

            if (Vector2.Distance(attacker.transform.position, target.transform.position) <= 0.5f) {

                Debug.Log("Player "+ (attacker.playerIndex+1) + " hit Player " + (target.playerIndex + 1)+"!!");

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
