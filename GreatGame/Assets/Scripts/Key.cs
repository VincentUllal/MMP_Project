using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //isUsed makes sure that a key can only increase the score by one;
    //sometimes OnTriggerEnter2D is called twice (probably because
    //the Char1 consists of multiple hitboxes)
    private bool isUsed;
    void Start(){
        isUsed = false;
    }
    void OnTriggerEnter2D(Collider2D col){
        if(!isUsed){
            //Debug.Log("touched the key");
            if(col.gameObject.tag == "Player"){
                isUsed = true;
                Destroy(this.gameObject);
                Score.scoreAmount += 1;
            }
        }
    }
}
