using Player;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class BasicEnemy : MonoBehaviour
{
    private Health health;
    
    [SerializeField] private ParticleSystem deathParticles;
    private float hitCooldown = 1f;
    private float timer = 0f;
    private bool canHit = false;
    
    private MeshRenderer meshRenderer;
    
    private void OnEnable()
    {
        health = GetComponent<Health>();
        meshRenderer = GetComponent<MeshRenderer>();
        if (health)
            health.OnDeath += Death;
    }

    private void OnDisable()
    {
        if (health)
            health.OnDeath -= Death;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
            
        if (Physics.BoxCast(transform.position,
                new Vector3(meshRenderer.bounds.extents.x * 0.8f, meshRenderer.bounds.extents.y, meshRenderer.bounds.extents.z),
                Vector3.up, Quaternion.identity,meshRenderer.bounds.extents.y * 0.2f,
                LayerMask.GetMask("Player")))
            health.TakeDamage(1);
        else if (canHit && collision.gameObject.TryGetComponent(out Health playerHealth))
            playerHealth.TakeDamage(1);
        
        timer = 0f;
        canHit = false;
    }

    private void Update()
    {
        if (timer < hitCooldown)
            timer += Time.deltaTime;
        else if (!canHit)
        {
            canHit = true;
            
        }
    }
    
    private void Death()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        GameManager.Instance.player.GetComponent<PlayerScore>().AddScore(5);
        Destroy(gameObject);
    }
}
