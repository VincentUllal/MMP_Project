using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Mechanics
{
    public class HealingBlock : MonoBehaviour
    {
        private GameObject player;
        public int healingAmount = 2;

        void Start()
        {
            player = GameObject.Find("Player");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                var healthcomponent = player.GetComponent<PlayerHealth>();

                if (healthcomponent.GetHealth() == healthcomponent.maxHealth)
                    return;

                healthcomponent.ChangeHealth(healingAmount);
                Destroy(this.gameObject);

                Debug.Log("New Health" + healthcomponent.GetHealth());
            }
        }
    }
}
