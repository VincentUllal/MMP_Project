using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : MonoBehaviour
{
    // Start is called before the first frame u
    [SerializeField] private Transform player;  //reference a player
    public int damage = 2;
    public float damageInterval = 2;
    private float timer;
    void Start(){
        timer = damageInterval;
    }
    void Update(){
        timer += Time.deltaTime;
    }
    void OnCollisionStay2D(Collision2D col){
        //float invincible = Time.deltaTime;
        if(col.gameObject.tag == "Player"){
            if(timer > damageInterval){
                Debug.Log("Player touched the DamageBlock");
                var healthComponent = player.GetComponent<Health>(); //the Health Component of Char1
                    if (healthComponent != null){
                        healthComponent.TakeDamage(damage);
                        timer = 0; //this block doesn't deal damage for <damageInterval> seconds
                }
            }
        }
    }
}
