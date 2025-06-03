using Powers;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Projectile), typeof(SimpleEnemyMovement))]
    public class RangedEnemy : MonoBehaviour
    {
        private Projectile projectileScript;
        private SimpleEnemyMovement simpleMovement;
        private bool canAttack = false;
        private float timer = 0;
        private Health health;

        [SerializeField] private float attackRange = 7;
        [SerializeField] private float maxShootAngle = 45;
        [SerializeField] private float attackCooldown = 2f;
        [SerializeField] private float projectileSpeed = 9f;

    
        private void Start()
        {
            projectileScript = GetComponent<Projectile>();
            simpleMovement = GetComponent<SimpleEnemyMovement>();
        }

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
    
        private void Update()
        {
            if (timer < attackCooldown)
                timer += Time.deltaTime;
            
            canAttack = (GameManager.Instance.player.position - transform.position).magnitude <= attackRange;
            simpleMovement.canMove = !canAttack;

            //Look at player when able to attack
            if (canAttack)
                transform.right = GameManager.Instance.player.position.x >= transform.position.x ? Vector3.right : Vector3.left;
            
            if (canAttack && timer >= attackCooldown)
            {
                Attack();
                timer = 0;
            }
        }

        private void Attack()
        {
            //Checks if player direction is inside max angle
            if (Vector3.SignedAngle(GameManager.Instance.player.position - transform.position, transform.right,
                    Vector3.up) < maxShootAngle)
            {
                Quaternion rot = Quaternion.LookRotation(Vector3.forward, GameManager.Instance.player.position - transform.position);
                Vector3 euler = rot.eulerAngles;
                euler.z += 90;
                rot = Quaternion.Euler(euler);
                projectileScript.Shoot(transform.position, rot, projectileSpeed, transform);
            }
        }
        
        
        private void Death()
        {
            GameManager.Instance.player.GetComponent<Player.PlayerScore>().AddScore(10);
            Destroy(gameObject);
        }
    }
}
