using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    
    //man braucht die Variablen nur hier/es muss nicht public sein, aber man will sie trotzdem im Inspector sehen --> [SerializeField]
    [SerializeField] private Transform respawnPoint;  //reference a respawnPoint
    
    void OnTriggerEnter2D(Collider2D other){    //"Collider other" registers when another GameObject contacts the hitbox
        TriggerRespawn(other);
    }

    public void TriggerRespawn(Collider2D other) // allows external access
    {
        other.transform.SetPositionAndRotation(respawnPoint.transform.position, new Quaternion());
        other.attachedRigidbody.velocity = new Vector2();
    }
    
    public void TriggerRespawn(Collider2D other, Transform respawnPoint) // custom respawn point.
    {
        other.transform.SetPositionAndRotation(respawnPoint.transform.position, new Quaternion());
        other.attachedRigidbody.velocity = new Vector2();
    }
    
    public void TriggerRespawn(Collider2D other, Vector2 respawnPoint) // by direct coordinates.
    {
        other.transform.SetPositionAndRotation(respawnPoint, new Quaternion());
        other.attachedRigidbody.velocity = new Vector2();
    }
}
