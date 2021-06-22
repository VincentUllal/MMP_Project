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
    public class KinematicObject_InProgress : MonoBehaviour
    {
        public float hitForceMultiplier = 100f;
        public float hitTorqueMultiplier = 10f;

        /* Adjustable variables:
         * - weight: physical mass, used for inertia in collisions.
         * - gravityModifier: Multiplier on default Physics2D.gravity.
         * - fallSpeedModifier: Lets the Object fall faster.
         * - minGroundnormalY: sets the stepest floor angle for the object to sit on. More than that and it will slip.
         */
        public float gravityModifier = 2f;
        public float fallSpeedModifier = 1f;
        public float minGroundnormalY = 0.65f;

        /* Mechanical Limits.
         * Values smaller than that are ignored.
         */
        protected const float minMoveDistance = 0.001f;
        protected const float shellRadius = 0.01f;

        // Time keeping: Waiting, to stop pushing a DynamicObjec a dozen times instantly.
        private float pauseTillTime;

        [SerializeField] protected Vector2 velocity;
        protected Vector2 nextVelocity;
        protected Vector2 groundNormal;

        protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
        protected ContactFilter2D contactFilter;
        protected Rigidbody2D body;

        protected bool IsGrounded = false;

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

            pauseTillTime = Time.time;
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

        protected void FixedUpdate()
        {
            GameObject hitObject = null;


            if (velocity.y < 0)
                velocity += fallSpeedModifier * gravityModifier * Time.deltaTime * Physics2D.gravity;
            else
                velocity += gravityModifier * Time.deltaTime * Physics2D.gravity;


            velocity.x = nextVelocity.x;


            IsGrounded = false;

            var move = velocity * Time.deltaTime;
            var nextMoveModified = PerformMovement(move, ref hitObject);

            if (hitObject != null)
            {
                if (string.Compare(hitObject.tag, "DynamicObject") == 0 && Time.time > pauseTillTime)
                {
                    if (string.Compare(hitObject.name, "FallingBlock") == 0) print("FOUND");
                    AlterMovementOfOther(move, ref nextMoveModified, hitObject);
                    pauseTillTime += Time.time + 0.1f;
                }
            }

            body.position += nextMoveModified;
        }

        Vector2 PerformMovement(Vector2 move, ref GameObject hitObject)
        {
            float distance = move.magnitude;

            if (distance > minMoveDistance) // lower cutoff, jittering
            {
                int count = body.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

                for (int i = 0; i < count; i++)
                {
                    if (string.Compare(hitBuffer[i].transform.gameObject.tag, "DynamicObject") == 0 ||
                        string.Compare(hitBuffer[i].transform.gameObject.tag, "StaticObject") == 0)
                    {

                        Vector2 currentNormal = hitBuffer[i].normal;


                        //if (body.Cast(Vector2.down, contactFilter,))

                            if (IsGrounded)
                            {
                                float projection = Vector2.Dot(velocity, currentNormal);
                                if (projection < 0)
                                {
                                    velocity -= projection * currentNormal;
                                }
                            }
                            else
                            {
                                velocity.x *= 0;
                                velocity.y = Mathf.Min(velocity.y, 0);
                            }

                        float modifiedDistance = hitBuffer[i].distance - shellRadius;
                        if (modifiedDistance < distance)
                        {
                            distance = modifiedDistance;
                            hitObject = hitBuffer[i].transform.gameObject;
                        }
                    }
                }
            }
            return move.normalized * distance;
        }

        void AlterMovementOfOther(Vector2 move, ref Vector2 nextMoveModified, GameObject hitObject)
        {
            Rigidbody2D eRigid = hitObject.GetComponent<Rigidbody2D>();
            if (eRigid != null)
            {
                eRigid.AddForce(move * hitForceMultiplier, ForceMode2D.Force);
                eRigid.AddTorque(-hitTorqueMultiplier * Vector2.Dot(GetComponent<Transform>().position, eRigid.transform.position));

                print("hit " + hitObject.transform.name + " with " + nextMoveModified);
            }

        }

    }

}