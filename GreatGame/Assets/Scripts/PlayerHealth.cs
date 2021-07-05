using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MMP.Mechanics
{
    public class PlayerHealth : MonoBehaviour
    {
        public int maxHealth = 5;
        private int currentHealth;

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void ChangeHealth(int amount) //damange is a negative change
        {
            currentHealth += amount;
            if (currentHealth <= 0) Death();
            else if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        public void SetHealth(int amount)
        {
            currentHealth = amount > maxHealth ? maxHealth : amount;
            if (currentHealth <= 0) Death();
        }

        public int GetHealth()
        {
            return currentHealth;
        }

        private void Death()
        {
            Debug.Log("You Died");
            SceneManager.LoadScene("MenuScene"); //< primitiv respawn

            //ToDo
            //Take away player controll
            //play death animation
            //either respawn or menu
        }
    }
}
