/*
 * KinematicObejct class contains all data and behaviours that every moving object in the game requieres.
 * - In particular: This calculates all move-functions, inheriting classes tell this one where they want to move to.
 * - PerformMovement uses a RayCast Solution to predict possible collisions beforhand, taking the randomnes of unity's collision handling out of the game.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Namespaces : Two code files within the same namespace see eachother, this is preferable to placing everything in global namespace.
 * To access a diffrent namespace. use 'using'
 */
namespace MMP.Mechanics
{
    public class KinematicObject : MonoBehaviour
    {
        /* Adjustable variables:
         * - fallSpeedModifier: Lets the Object fall faster.
         * - minGroundnormalY: sets the stepest floor angle for the object to sit on. More than that and it will slip.
         */
        public float fallSpeedModifier = 1f;
        public float minGroundnormalY = 0.65f;

        /* Mechanical Limits.
         * Values smaller than that are ignored.
         */
        protected const float minMoveDistance = 0.001f;
        protected const float shellRadius = 0.01f;

        [SerializeField] protected Vector2 velocity;
        protected Vector2 nextVelocity;
        protected Vector2 groundNormal;

        protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
        protected ContactFilter2D contactFilter;
        protected Rigidbody2D body;

        protected bool IsGrounded = false;

        /*
         * Three standart functions for scripted manipulations of an objects velocity and position.
         */
        public void Bounce(float value)
        {
            velocity.y = value;
        }
        public void Bounce(Vector2 dir)
        {
            velocity = dir;
        }
        public void Teleport(Vector3 position)
        {
            body.position = position;
            velocity *= 0;
            body.velocity *= 0;
        }



        /*
         * isKinematic = true, tells unity that, while it shall interact with other physics obejcts, i'll handle its moving behaviour on my own.
         */
        protected void OnEnable()
        {
            body = GetComponent<Rigidbody2D>();
            body.isKinematic = true;
        }
        protected void OnDisable()
        {
            body.isKinematic = false;
        }

        /* Initiate ContactFilter2D
         * automaticly filters the results returned by RayCast2D for us.
         * - useTriggers: requieres a collider (Collisionbox) to have been involved
         * - SetLayerMask: There are up to 32 collision layers an object can be in. The gameObject shall only interact with others in its own layer.
         */
        protected void Start()
        {
            contactFilter.useTriggers = false;
            contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
            contactFilter.useLayerMask = true;
        }

        /* Called every frame
         * Nulls the nextVelocity vector, before starting its recalculation for the next frame
         */
        protected virtual void Update()
        {
            nextVelocity = Vector2.zero;
            ComputeVelocity();
        }

        /*
         * Virtual : implementation case specific by inheriting class.
         */
        protected virtual void ComputeVelocity()
        {
            ; //<- Tipp: a ';' alone does nothing, but informs other coders that the space is deliberatly empty.
        }


        /* FixedUpdate gets called at fixed intervals, roughly every frame. Great for everything physics
         * 1) Applies a downward movement due to gravity (amplified if already moving downward)
         *      independet on whether we can move down or not.
         * 2) copies over horizontal speed as caluclated once per frame in ComputeVelocity.
         * 3) Assume that we are falling, test for later and in every frame anew
         * 4) Calculate the portion of nextVelocity along the ground angle (RayCast2D gives us the angle of slopes) -  PerfomMovement
         * 5) Caluclate vertical Speed, test for collision in PerformMovement
         */
        protected void FixedUpdate()
        {
            // 1
            if (velocity.y < 0)
                velocity += fallSpeedModifier * Physics2D.gravity * Time.deltaTime;
            else
                velocity += Physics2D.gravity * Time.deltaTime;

            // 2
            velocity.x = nextVelocity.x;
            
            // 3
            IsGrounded = false;

            // 4
            var deltaPosition = velocity * Time.deltaTime;
            var move = new Vector2(groundNormal.y, -groundNormal.x) * deltaPosition.x;
            PerformMovement(move, false);

            // 5
            move = Vector2.up * deltaPosition.y;
            PerformMovement(move, true);
        }

        /* PerformMovement tests and adjusts for future collisions, split into horizontal and vertical.
         * 1) Test if length of adjusted nextVector is above minimum
         * 2) Cast (from RayCast2D)  projects the Collider along the move vector (normalized) and distance.
         *      returns the count of all other Colliders encountered along the way, stored in hitBuffer[], filtered by contactFilter.
         *      iterates through all colliders found.
         * 3) .normal returns a vector perpendicular to the hit surface of the collider.
         * 4) if ground it is flat enough to sit on. we'll be grounded, set groundNormal-'Angle'-Vector for next FixedUpdate, 
         *      set currentNormal.x = 0 because sloping is an additional up/down movement
         * 5) if grounded, calculate portion of moveVector that alings with the slope we're standing on.
         *      slow down when moving up.
         * 6) airborn and hit something. stop all but downward movement.
         * 7) shorten move distance to found object, repeat for all found objects, lock in the closest object.
         * 8) actually move body.
         */
        void PerformMovement(Vector2 move, bool yMovement)
        {
            // 1
            var distance = move.magnitude;

            if (distance > minMoveDistance)
            {
                // 2
                var count = body.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
                for (var i = 0; i < count; i++)
                {
                    // 3
                    var currentNormal = hitBuffer[i].normal;
                    // 4
                    if (currentNormal.y > minGroundnormalY)
                    {
                        IsGrounded = true;

                        if (yMovement)
                        {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }
                    // 5
                    if (IsGrounded)
                    {
                        var projection = Vector2.Dot(velocity, currentNormal);
                        if (projection < 0)
                        {
                            velocity = velocity - projection * currentNormal;
                        }
                    }
                    // 6
                    else
                    {
                        velocity.x *= 0;
                        velocity.y = Mathf.Min(velocity.y, 0);
                    }

                    // 7
                    var modifiedDistance = hitBuffer[i].distance - shellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
            }
            // 8
            body.position = body.position + move.normalized * distance;
        }



    }

}