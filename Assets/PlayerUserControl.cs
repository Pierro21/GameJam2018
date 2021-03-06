using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlayerCharacter))]
    public class PlayerUserControl : MonoBehaviour
    {
        private PlayerCharacter m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlayerCharacter>();
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
            bool attack = Input.GetKey(KeyCode.LeftControl);
            bool attack2 = Input.GetKey(KeyCode.LeftAlt);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            Debug.Log("attack: " + attack);
            m_Character.Move(h, attack, attack2, m_Jump);
            m_Jump = false;
        }
    }
}
