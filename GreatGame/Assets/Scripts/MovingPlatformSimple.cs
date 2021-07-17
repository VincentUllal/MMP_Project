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

        public float sleepTimer = 0.25f;
        private float moveTimer = 0f, waitTimer = 0f; 
        private bool flag = false;

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
            if (flag && (transform.position - pos1).magnitude < 0.01f)
            {
                waitTimer = Time.time + sleepTimer;
                flag = false;
                return;
            }
            if (!flag && (transform.position - pos2).magnitude < 0.01f)
            {
                waitTimer = Time.time + sleepTimer;
                flag = true;
                return;
            }

            if (waitTimer <= Time.time)
            {
                moveTimer += Time.deltaTime;
                float x = Mathf.PingPong(moveTimer * speed, 0.99f);
                transform.position = Vector3.Lerp(pos1, pos2, Mathf.Pow(Mathf.Sin(x * Mathf.PI),2));
            }
        }
    }
}
