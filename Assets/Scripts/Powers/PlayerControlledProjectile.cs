using UnityEngine;

namespace Powers
{
    public class PlayerControlledProjectile : Projectile
    {
        [SerializeField] private float projectileSpeed = 9;
        protected override void Update()
        {
            base.Update();
            
            if (Input.GetMouseButtonDown(0))
            {
                Shoot(transform.position, transform.rotation, projectileSpeed, transform);
            }
        }
    }
}
