using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Mechanics
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerController : KinematicObject
    {
        public Animator animator;
        public float maxSpeed = 8;
        public float jumpTakeOffSpeed = 8;
        public JumpState jumpState = JumpState.Grounded;

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
            if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
            {
                jumpState = JumpState.PrepareToJump;
                //jump = true;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                stopJump = true;
            }
            UpdateJumpState();

            base.Update();


            //Crouching
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Vector3 characterScale = transform.localScale;
                characterScale.y = 0.5f;
                transform.localScale = characterScale;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Vector3 characterScale = transform.localScale;
                characterScale.y = 1f;
                transform.localScale = characterScale;
                //ToDo
                //Rescale bugs us into the ground.
                    //Kinematic Movement Collision Handler will deal with that.
            }
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
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
                    velocity.y *= 0.5f;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            nextVelocity = move * maxSpeed;
            //for Animator
            if (Mathf.Abs(nextVelocity.x) > 0 )
            {
                animator.SetFloat("Speed", 1);
            }
            else 
            {
                animator.SetFloat("Speed", -1);
            }
            
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    
    }
}