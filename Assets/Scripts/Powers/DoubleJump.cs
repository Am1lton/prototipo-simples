using System;
using Player;
using UnityEngine;

namespace Powers
{
    [RequireComponent(typeof(PlayerMovement), typeof(Rigidbody))]
    public class DoubleJump : MonoBehaviour
    {
        private Rigidbody rb;
        private PlayerMovement playerMovement;

        private uint jumps = 1;
        private const uint MAX_JUMPS = 1;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void FixedUpdate()
        {
            if (playerMovement.grounded)
                jumps = MAX_JUMPS;
        }

        private void Update()
        {
            if (playerMovement.grounded) return;
            if (Input.GetKeyDown(KeyCode.Space) && jumps > 0)
            {
                playerMovement.Jump();
                jumps--;
            }
        }
    }
}
