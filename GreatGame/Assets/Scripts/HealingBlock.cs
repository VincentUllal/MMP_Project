using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBlock : MonoBehaviour
{
    // Start is called before the first frame u
    [SerializeField] private Transform player;  //reference a player
    public int healing = 2;
    void Update(){

    }
    void OnTriggerEnter2D(Collider2D col){
        //float invincible = Time.deltaTime;
        if(col.gameObject.tag == "Player"){
            Debug.Log("Player touched the HealingBlock");
            var healthComponent = player.GetComponent<Health>(); //the Health Component of Char1
            Debug.Log(healthComponent.currentHealth);
            Debug.Log(healthComponent.maxHealth);
            if (healthComponent != null && healthComponent.currentHealth < healthComponent.maxHealth){
                healthComponent.Heal(healing);
                Destroy(this.gameObject);
            }
        }
    }
}
