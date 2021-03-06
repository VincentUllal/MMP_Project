using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Mechanics
{
    public class PlayerController : KinematicObject
    {
        public Vector3 respawnPoint;

        public volatile bool controlEnabled = true;

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

        protected override void Start()
        {
            respawnPoint = this.transform.position;
            base.Start();
        }

        protected override void Update()
        {
            if (controlEnabled)
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
            //character is running && not in air
            //idle --> running
            if (Mathf.Abs(velocity.x) > 0 && Mathf.Abs(velocity.y) < 0.1)
            {
                animator.SetFloat("xSpeed", 1); // just a number > 0
                //animator.SetFloat("ySpeed", -1); // just a number > 0
            }
            //character is running && in air (no jump animation)
            //running --> idle
            else if (Mathf.Abs(velocity.x) > 0 && Mathf.Abs(velocity.y) > 0.1)
            {
                animator.SetFloat("xSpeed", -1);
                //animator.SetFloat("ySpeed", 1);
            }
            //character is standing --> idle
            else {
                animator.SetFloat("xSpeed", -1); // just a number < 0
                //animator.SetFloat("ySpeed", -1); // just a number < 0
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