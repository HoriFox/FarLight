﻿using UnityEngine;

namespace fl
{
    public class ShipUserControl4Axis : MonoBehaviour
    {
        private float m_Throttle; // Вперёд и назад.
        private float m_Yaw;
        private float m_Pitch;
        private float m_Roll;   // Вращение.
        private float m_Right;  // Смещение влево и вправо.
        private float m_Up;     // Вверх и вниз.

        private bool m_AttackRight;
        private bool m_AttackLeft;

        private ShipController m_Ship;

        private void Awake()
        {
            m_Ship = GetComponent<ShipController>();
        }

        private void FixedUpdate()
        {
            m_Right = CrossPlatformInputManager.GetAxis("Horizontal");
            m_Pitch = CrossPlatformInputManager.GetAxis("Vertical");
            m_AttackRight = Input.GetMouseButton(0);
            m_AttackLeft = Input.GetMouseButton(1);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                DoubleTap("Warp");
            }

            // Вращение.
            m_Roll = (Input.GetKey(KeyCode.Q) ? 1f : 0f) - (Input.GetKey(KeyCode.E) ? 1f : 0f);

            // Вверх и вниз. Вынести в настройки. TO DO
            m_Up = (Input.GetKey(KeyCode.Space) ? 1f : 0f) - (Input.GetKey(KeyCode.LeftAlt) ? 1f : 0f);

            // Вперёд и назад. Вынести в настройки. TO DO
            m_Throttle = (Input.GetKey(KeyCode.LeftShift) ? 1f : 0f) - (Input.GetKey(KeyCode.Z) ? 1f : 0f);

            m_Ship.Move(m_Roll, m_Pitch, m_Yaw, m_Throttle, m_Right, m_Up);
            m_Ship.Attack(m_AttackRight, m_AttackLeft);
        }

        private void Warp()
        {
            StartCoroutine(m_Ship.Warp());
        }

        public float doubleTapDelay = 0.4f;

        private float tapCount = 0;
        private void DoubleTap(string methodLuck)
        {
            tapCount++;
            if (tapCount == 2)
            {
                CancelInvoke("FailDoubleTap");
                Invoke(methodLuck, 0f);
                tapCount = 0;
                return;
            }

            Invoke("FailDoubleTap", doubleTapDelay);
        }
        private void FailDoubleTap()
        {
            tapCount = 0;
        }
    }
}
