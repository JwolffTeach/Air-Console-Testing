using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private float h;
        [SerializeField] float dampen = 0.2f;
        bool dampening = false;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            dampenHorizontal();
            m_Jump = false;
        }

        public void SetHorizontal(float horizontalInput) {
            dampening = false;
            h = horizontalInput;
        }

        public void SlowDown() {
            dampening = true;
        }

        private void dampenHorizontal() {
            if (dampening) {
                if (h > 0) {
                    h = Mathf.Clamp(h - dampen, 0f, 1f);
                }
                else if (h < 0) {
                    h = Mathf.Clamp(h + dampen, -1f, 0f);
                }
                else {
                    dampening = false;
                    h = 0;
                }
            }
        }
    }
}
