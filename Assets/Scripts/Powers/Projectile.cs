using System;
using UnityEngine;

namespace Powers
{ 
    public class Projectile : MonoBehaviour
    {
        private GameObject projectile;
        private float cooldown = 0.7f;
        private float timer = 0;
        private bool canFire = true;
        
        
        void Start()
        {
            projectile = Resources.Load("Powers/Projectile") as GameObject;
        }

        protected virtual void Update()
        {
            if (!canFire)
            {
                timer += Time.deltaTime;
                if (timer >= cooldown)
                {
                    canFire = true;
                    timer = 0;
                }
            }
        }

        public void Shoot(Vector3 origin, Quaternion rotation, float projectileSpeed, Transform parent)
        {
            if (!canFire) return;
            GameObject spawnedProjectile = Instantiate(projectile, origin, rotation, parent);
            ProjectileBehavior projBhvr = spawnedProjectile.GetComponent<ProjectileBehavior>();
            projBhvr.projectileSpeed = projectileSpeed;
            canFire = false;
        }
        
        public void Shoot(Vector3 origin, Quaternion rotation, Transform parent)
        {
            if (!canFire)
            {
                Debug.Log("Projectile on Cooldown");
                return;
            }
            GameObject spawnedProjectile = Instantiate(projectile, origin, rotation, parent);
            canFire = false;
        }
    }
}
