using Player;
using UnityEngine;

namespace Powers
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerMovement))]
    public class WallJump : MonoBehaviour
    {
        private PlayerMovement playerMovement;
        private Rigidbody rb;

        private enum JumpSide
        {
            None = 0,
            Left = 1,
            Right = 2,
        }
        
        private JumpSide lastJumpSide = JumpSide.None;
        private bool canJump = false;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            playerMovement = GetComponent<PlayerMovement>();
         
            if (TryGetComponent(out DoubleJump doubleJump))
                doubleJump.hasWallJump = true;
        }

        private void Update()
        {
            if (playerMovement.Grounded)
            {
                lastJumpSide = JumpSide.None;
                return;
            }
            if (!playerMovement.OnWallLeft && !playerMovement.OnWallRight)
            {
                canJump = true;
                return;
            }
            if (!Input.GetKeyDown(KeyCode.Space)) return;

            playerMovement.temporaryMaxHorizontalSpeed = playerMovement.GetJumpForce();
            if (playerMovement.OnWallLeft && canJump && lastJumpSide != JumpSide.Left)
            {
                lastJumpSide = JumpSide.Left;
                rb.velocity = new Vector3(playerMovement.temporaryMaxHorizontalSpeed, playerMovement.GetJumpForce() * 0.8f, 0);
                transform.right = Vector3.right;
            }
            else if (lastJumpSide != JumpSide.Right)
            {
                lastJumpSide = JumpSide.Right;
                rb.velocity = new Vector3(-playerMovement.temporaryMaxHorizontalSpeed, playerMovement.GetJumpForce() * 0.8f, 0);
                transform.right = Vector3.left;
            }
            canJump = false;
        }

        private void OnDisable()
        {
            if (TryGetComponent(out DoubleJump doubleJump))
                doubleJump.hasWallJump = false;
        }
    }
}
