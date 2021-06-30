using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Mechanics
{
    public class DeathZone : MonoBehaviour
    {
        private GameObject player;
        public int damageToPlayer = 1;

        private void Start()
        {
            player = GameObject.Find("Player");
        }
        void OnTriggerEnter2D(Collider2D collision)
        {    //"Collider other" registers when another GameObject has contact with the DeathZone
            if (collision.gameObject == player)
            {
                player.GetComponent<PlayerHealth>().ChangeHealth(-damageToPlayer);
            }
        }
    }
}
