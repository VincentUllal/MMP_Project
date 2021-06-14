using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Mechanics
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController_woJState : KinematicObject
    {
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;

        private SpriteRenderer spriteRenderer;
        private bool jump;
        private bool stopJump;

       [SerializeField] private Vector2 move;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void Update()
        {
            move.x = Input.GetAxis("Horizontal");
            if (IsGrounded && Input.GetButtonDown("Jump"))
            {
                jump = true;
                print("Jumped");
            }
            else if (Input.GetButtonDown("Jump"))
            {
                stopJump = true;
                print("Jumped");
            }            

            base.Update();            
        }
        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * 1.5f;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            nextVelocity = move * maxSpeed;
        }
    }
}