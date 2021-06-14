using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    
    //man braucht die Variablen nur hier/es muss nicht public sein, aber man will sie trotzdem im Inspector sehen --> [SerializeField]
    [SerializeField] private Transform player;  //reference a player
    [SerializeField] private Transform respawnPoint;  //reference a respawnPoint
    
    void OnTriggerEnter2D(Collider2D other){    //"Collider other" registers when another GameObject contacts the hitbox
        other.transform.position = respawnPoint.transform.position; //just set the position of the other to the position of the respawnPoint
    }
}
