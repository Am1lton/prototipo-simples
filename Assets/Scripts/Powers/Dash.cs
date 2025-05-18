using Player;
using UnityEngine;

namespace Powers
{
    [RequireComponent(typeof(Rigidbody), typeof(PlayerMovement))]
    public class Dash : MonoBehaviour
    {
        private Rigidbody rb;
        private PlayerMovement movement;

        [SerializeField] private float dashMaxSpeed = 40f;
        [SerializeField] private float dashCooldown = 2f;

        [SerializeField] private float dashTimer = 0;

        private bool canDash = false;
        private bool dashing = false;
        private float defaultSpeedFallof;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            movement = GetComponent<PlayerMovement>();
            defaultSpeedFallof = movement.maxSpeedFalloff;
        }


        private void FixedUpdate()
        {
            rb.useGravity = !dashing;

            if (movement.temporaryMaxHorizontalSpeed <= movement.MaxHorizontalSpeed)
            {
                dashing = false;
                movement.maxSpeedFalloff = defaultSpeedFallof;
            }
        }
        
        private void Update()
        {
            if (!canDash) dashTimer += Time.deltaTime;
            if (dashTimer >= dashCooldown)
            {
                canDash = true;
                dashTimer = 0;
            }

            if (dashing)
                movement.maxSpeedFalloff += 10 * dashMaxSpeed * Time.deltaTime;
            
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                movement.temporaryMaxHorizontalSpeed = dashMaxSpeed;
                switch (rb.velocity.x)
                {
                    case < 0:
                        rb.velocity = new Vector3(-movement.temporaryMaxHorizontalSpeed, 0, 0);
                        break;
                    case >= 0:
                        rb.velocity = new Vector3(movement.temporaryMaxHorizontalSpeed, 0, 0);
                        break;
                }
                
                canDash = false;
                dashing = true;
            }
        }
    }
}
