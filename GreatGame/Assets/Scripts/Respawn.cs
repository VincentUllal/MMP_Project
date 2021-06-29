using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    
    //man braucht die Variablen nur hier/es muss nicht public sein, aber man will sie trotzdem im Inspector sehen --> [SerializeField]
    [SerializeField] private Transform player;  //reference a player
    [SerializeField] private Transform ball;  //reference a ball
    [SerializeField] private Transform respawnPoint;  //reference a respawnPoint
    [SerializeField] private Transform BBrespawnPoint;  //reference a basketball respawnPoint
    void OnTriggerEnter2D(Collider2D other){    //"Collider other" registers when another GameObject contacts the hitbox
    //A TRIGGER is not solid, only triggers an event
        if(other.tag == "Player")
        {
            var healthComponent = player.GetComponent<Health>(); //the Health Component of Char1
            if (healthComponent != null){
                healthComponent.TakeDamage(1);
            }
            player.transform.position = respawnPoint.transform.position; //just set the position of the player to the position of the respawnPoint
            Debug.Log("Player Respawned");
        }else if(other.tag == "Ball")   //wenn ein Ball reinfällt
        {
            ball.transform.position = BBrespawnPoint.transform.position;
            Debug.Log("BasketBall respawned");
        }
    }
}
