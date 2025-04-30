using Player;
using UnityEngine;

namespace Powers
{
    [RequireComponent(typeof(PlayerMovement), typeof(Rigidbody))]
    public class DoubleJump : MonoBehaviour
    {
        private Rigidbody rb;
        private PlayerMovement playerMovement;
        
        private bool canDoubleJump;
        private bool hasDoubleJumped;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
            {
                rb.AddForce(Vector3.up * playerMovement.GetJumpForce(), ForceMode.VelocityChange);
                hasDoubleJumped = true;
            }

            switch (playerMovement.grounded)
            {
                case false when !hasDoubleJumped:
                    canDoubleJump = true;
                    break;
                case true:
                    hasDoubleJumped = false;
                    break;
            }
        }
    }
}
