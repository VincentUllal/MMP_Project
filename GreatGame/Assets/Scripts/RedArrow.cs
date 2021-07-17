using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Mechanics
{
    public class RedArrow : MonoBehaviour
    {
        private GameObject player;
        void Start()
        {
            player = GameObject.Find("Player");
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
