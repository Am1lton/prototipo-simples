using Player;
using UnityEngine;

namespace Powers
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerMovement))]
    public class Dash : MonoBehaviour
    {
        private Rigidbody rb;
        private PlayerMovement movement;

        [SerializeField] private float dashMaxSpeed = 20f;
        [SerializeField] private float defaultMaxSpeed;
        [SerializeField] private float dashCooldown = 2f;

        [SerializeField] private float dashTimer = 0;

        private bool canDash = false;
        private bool dashing = false;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            movement = GetComponent<PlayerMovement>();
            defaultMaxSpeed = movement.maxHorizontalSpeed;
        }


        private void FixedUpdate()
        {
            rb.useGravity = !dashing;
            
            if (movement.maxHorizontalSpeed <= defaultMaxSpeed)
            {
                movement.maxHorizontalSpeed = defaultMaxSpeed;
                dashing = false;
                return;
            }
            
            movement.maxHorizontalSpeed = movement.maxHorizontalSpeed - 2 * dashMaxSpeed * Time.deltaTime < defaultMaxSpeed ?
                defaultMaxSpeed : movement.maxHorizontalSpeed - 2 * dashMaxSpeed * Time.deltaTime;
        }
        
        private void Update()
        {
            if (!canDash) dashTimer += Time.deltaTime;
            if (dashTimer >= dashCooldown)
            {
                canDash = true;
                dashTimer = 0;
            }
            
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                movement.maxHorizontalSpeed = dashMaxSpeed;
                switch (rb.velocity.x)
                {
                    case < 0:
                        rb.AddForce(Vector3.left * movement.maxHorizontalSpeed, ForceMode.VelocityChange);
                        break;
                    case >= 0:
                        rb.AddForce(Vector3.right * movement.maxHorizontalSpeed, ForceMode.VelocityChange);
                        break;
                }
                
                canDash = false;
                dashing = true;
                rb.velocity = new Vector3(rb.velocity.x, 0, 0);
            }
            
            
            
        }
    }
}
