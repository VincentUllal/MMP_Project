using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Mechanics
{
    public class Respawn : MonoBehaviour
    {
        //man braucht die Variablen nur hier/es muss nicht public sein, aber man will sie trotzdem im Inspector sehen --> [SerializeField]
        private GameObject player;

        private void Start()
        {
            player = GameObject.Find("Player");
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.gameObject == player)
            {
                TriggerRespawn(other);
            }
            else
            {
                other.gameObject.SetActive(false);
            }
        }

        public void TriggerRespawn(Collider2D other) // allows external access
        {
            Vector3 newPosition = player.GetComponent<PlayerController>().respawnPoint;
            other.transform.SetPositionAndRotation(newPosition, new Quaternion());
            other.attachedRigidbody.velocity = new Vector2();
        }
    }
}
