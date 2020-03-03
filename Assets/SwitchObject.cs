using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGDDPlatformer
{
    public class SwitchObject : MonoBehaviour
    {
        public SpriteRenderer sr;
        public Color pressedColor;
        public Color timerColor;
        public MovingPlatform targetPlatform;
        public bool timedSwitch = false;
        public float maximumActiveTime = 0.0f;
        private bool pressed = false;
        private float timer;
        private Color startingColor;

        
        // Start is called before the first frame update
        void Start()
        {
            startingColor = sr.color;
            timer = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if(!pressed || !timedSwitch) return;

            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                targetPlatform.MoveToStartingPosition();
                pressed = false;
                sr.color = startingColor;
            }
        }

        /// <summary>
        /// Sent when another object enters a trigger collider attached to this
        /// object (2D physics only).
        /// </summary>
        /// <param name="other">The other Collider2D involved in this collision.</param>
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player1"))
            {
                pressed = true;
                // Debug.Log("Movng platform");
                targetPlatform.StartMoving();
                timer = maximumActiveTime;
                if(timedSwitch)
                    sr.color = timerColor;
                else
                    sr.color = pressedColor;
            }
        }
    }
}