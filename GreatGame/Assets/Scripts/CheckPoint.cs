using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Mechanics
{
    public class CheckPoint : MonoBehaviour
    {
        private GameObject player;

        private void Start()
        {
            player = GameObject.Find("Player");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject != player)
                return;

            player.GetComponent<PlayerController>().respawnPoint = this.transform.position;
            this.GetComponent<AudioSource>().Play();
        }
    }
}
