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
        public bool hasWallJump = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            playerMovement = GetComponent<PlayerMovement>();
            if (GetComponent<WallJump>())
                hasWallJump = true;
        }

        private void FixedUpdate()
        {
            if (playerMovement.Grounded)
                jumps = MAX_JUMPS;
        }

        private void Update()
        {
            if (playerMovement.Grounded) return;
            if (hasWallJump && playerMovement.OnWallLeft || playerMovement.OnWallRight) return;
            
            if (Input.GetKeyDown(KeyCode.Space) && jumps > 0)
            {
                playerMovement.Jump();
                jumps--;
            }
        }
    }
}
