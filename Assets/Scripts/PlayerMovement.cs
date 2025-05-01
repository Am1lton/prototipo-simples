using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rb;
        private Collider col;
        private LayerMask groundLayer;
        private float height;
        public bool grounded { private set; get; }

        [SerializeField] private float speed = 10f;
        [SerializeField] private float jumpForce = 30f;
        [SerializeField] private float extraGravity = 85f;
        
        public float GetJumpForce() => jumpForce;
        
        
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            
            rb.drag = 0f;
            rb.angularDrag = 0f;
            
            col = GetComponent<Collider>();
            height = col.bounds.size.y;
            groundLayer = LayerMask.GetMask("Ground");
        }

        private void FixedUpdate()
        {
           float horizontalVelocity = Input.GetAxisRaw("Horizontal");
           
           Vector3 moveInput = new Vector3(horizontalVelocity, 0, 0).normalized * speed;
           
           rb.velocity = new Vector3(moveInput.x, rb.velocity.y, moveInput.z);

           if (!grounded)
           {
               rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
           }

           if (rb.velocity.x > 0)
           {
               transform.rotation = Quaternion.Euler(0f, 0f, 0f);
           }
           else if (rb.velocity.x < 0)
           {
               transform.rotation = Quaternion.Euler(0f, 180f, 0f);
           }
        }

        private void Update()
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, height * 0.6f, groundLayer);

            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                Jump();
            }
        }

        public void Jump() => rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }
}
