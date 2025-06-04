using Player;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class BasicEnemy : MonoBehaviour
{
    private Health health;
    
    [SerializeField] private ParticleSystem deathParticles;

    private void OnEnable()
    {
        health = GetComponent<Health>();
        if (health)
            health.OnDeath += Death;
    }

    private void OnDisable()
    {
        if (health)
            health.OnDeath -= Death;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            health.TakeDamage(1);
    }

    private void Death()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        GameManager.Instance.player.GetComponent<PlayerScore>().AddScore(5);
        Destroy(gameObject);
    }
}
