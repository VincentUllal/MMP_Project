using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 5;
    public float currentHealth;
    void Start()
    {
        currentHealth = maxHealth; //leben initialisieren
    }
    // Update is called once per frame
    public void TakeDamage(int amount){
            currentHealth -= amount;
            if (currentHealth <= 0){
            Debug.Log("You Died");
            //play death animation
            //go back to title screen or respawn
            }
    }
    public void Heal(int amount){
        currentHealth += amount;
        if (currentHealth >= maxHealth){
            currentHealth = maxHealth;
        }
    }
}
