using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Mechanics
{
    public class MovingPlatformSimple : MonoBehaviour
    {
        private Vector3 pos1, pos2;
        public Vector2 pathwayDirection;
        public float speed = 1f;

        private void Start()
        {
            pos1 = this.transform.position;
            pos1.z = 0;
            Vector3 temp = pathwayDirection; //implicid z=0
            pos2 = pos1 + temp;

        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));
        }
    }
}
