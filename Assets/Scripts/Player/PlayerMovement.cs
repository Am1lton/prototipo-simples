using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody rb;
        private Collider col;
        private LayerMask groundLayer;
        private float height;
        private float width;

        public float maxHorizontalSpeed = 10f;
        
        public bool Grounded { private set; get; }
        public bool OnWallRight { private set; get; }
        public bool OnWallLeft { private set; get; }

        [SerializeField] private float speed = 10f;
        [SerializeField] private float jumpForce = 30f;
        [SerializeField] private float extraGravity = 85f;
        
        public float GetJumpForce() => jumpForce;
        
        
        private void Start()
        {
            maxHorizontalSpeed = speed;
            rb = GetComponent<Rigidbody>();
            
            rb.drag = 0f;
            rb.angularDrag = 0f;
            
            col = GetComponent<Collider>();
            height = col.bounds.size.y;
            width = col.bounds.size.x;
            groundLayer = LayerMask.GetMask("Ground");
        }

        private void FixedUpdate()
        {
           float horizontalInput = Input.GetAxisRaw("Horizontal");
           
           Vector3 movement = new Vector3(horizontalInput * speed, 0, 0);
           
           rb.AddForce(movement, ForceMode.VelocityChange);
           
           if (Mathf.Abs(rb.velocity.x) > maxHorizontalSpeed)
               rb.velocity = rb.velocity.x > 0 ? new Vector3(maxHorizontalSpeed, rb.velocity.y, rb.velocity.z) :
                   new Vector3(-maxHorizontalSpeed, rb.velocity.y, rb.velocity.z);

           if (!Grounded && rb.useGravity)
           {
               rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
           }
           else
           {
               if (Mathf.Abs(rb.velocity.x) < maxHorizontalSpeed - 0.1f)
                   rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
           }

           transform.rotation = rb.velocity.x switch //I didn't know you could do that with switch
           {
               > 0 => Quaternion.Euler(0f, 0f, 0f),
               < 0 => Quaternion.Euler(0f, 180f, 0f),
               _ => transform.rotation
           };
        }

        private void Update()
        {
            Grounded = Physics.Raycast(transform.position, Vector3.down, height * 0.6f, groundLayer);
            
            if (!Grounded)
            {
                OnWallRight = Physics.Raycast(transform.position, Vector3.right, width * 0.6f, groundLayer);
                OnWallLeft = Physics.Raycast(transform.position, Vector3.left, width * 0.6f, groundLayer);
            }
            else
            {
                OnWallRight = false;
                OnWallLeft = false;
            }
            
            
            if (Input.GetKeyDown(KeyCode.Space) && Grounded)
            {
                Jump();
            }
        }

        public void Jump() => rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }
}
