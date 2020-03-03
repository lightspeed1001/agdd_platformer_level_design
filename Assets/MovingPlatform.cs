using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGDDPlatformer
{
    public class MovingPlatform : MonoBehaviour
    {
        public Collider2D triggerCollider;
        public Rigidbody2D rb;
        public GameObject target;
        public Vector3 deltaPosition;
        public bool continuous = false;
        public bool startActive = false;
        public float speed = 0.5f;
        public float edgeWait = 0.5f;

        private bool isActive;
        private Vector3 startingLocation;
        private Vector3 targetLocation;
        private float edgeTimer;
        private bool movingToStart = false;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            isActive = startActive;
            startingLocation = transform.position;
            targetLocation = startingLocation + deltaPosition;
            edgeTimer = edgeWait;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if(!isActive) return;

            if(movingToStart)
            {
                // Debug.Log("going to start");
                transform.position = Vector3.MoveTowards(transform.position, startingLocation, speed);
                if(transform.position == startingLocation)
                {
                    movingToStart = false;
                    isActive = false;
                }
                return;
            }
            else
            {
                // Debug.Log("going somewhere");
                Vector3 newPos = Vector3.MoveTowards(transform.position, targetLocation, speed);
                // Debug.Log(newPos);
                Vector3 diff = newPos - transform.position;
                rb.MovePosition(newPos);
                if(target != null)
                    target.transform.position += diff;
                // transform.position = Vector3.MoveTowards(transform.position, targetLocation, speed);
            }
            
            if(transform.position == targetLocation && !continuous)
            {
                isActive = false;
                return;
            }
            
            if(continuous && targetLocation == transform.position && (edgeTimer = edgeTimer - Time.deltaTime) <= 0.0f)
            {
                Vector3 tmp = startingLocation;
                startingLocation = targetLocation;
                targetLocation = tmp;
                edgeTimer = edgeWait;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Player1"))
            {
                target = other.gameObject;
                // other.transform.parent = transform;
                // other.transform.position = triggerCollider.transform.position + new Vector3(0, 0.25f, 0);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(other.gameObject.CompareTag("Player1")) //&& other.gameObject.transform.parent == this.transform)
            {
                target = null;
                // other.transform.parent = null;
            }
        }

        public void StartMoving()
        {
            isActive = true;
        }

        public void MoveToStartingPosition()
        {
            movingToStart = true;
            StartMoving();
        }
    }
}