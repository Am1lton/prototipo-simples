using UnityEngine;

namespace Powers
{
    public class PlayerControlledProjectile : Projectile
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot(transform.position, transform.rotation, transform);
            }
        }
    }
}
