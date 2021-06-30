using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Mechanics
{
    public class DamageBlock : MonoBehaviour
    {
        // Start is called before the first frame u
        private GameObject player;
        public int damage = 2;
        public float damageInterval = 2;
        private float timer;

        void Start()
        {
            timer = damageInterval;
            player = GameObject.Find("Player");

        }

        void Update()
        {
            timer += Time.deltaTime;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            //float invincible = Time.deltaTime;
            if (collision.gameObject == player)
            {
                if (timer > damageInterval)
                {
                    Debug.Log("Player touched the DamageBlock");
                    var healthComponent = player.GetComponent<PlayerHealth>(); //the Health Component of Char1
                    if (healthComponent != null)
                    {
                        healthComponent.ChangeHealth(-damage);
                        timer = 0; //this block doesn't deal damage for <damageInterval> seconds
                    }
                }
            }
        }

    }
}
