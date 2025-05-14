using UnityEngine;

namespace Powers
{ 
    public class Projectile : MonoBehaviour
    {
        private GameObject projectile;
        private float cooldown = 1;
        private float timer = 0;
        private bool canFire = true;
        
        // Start is called before the first frame update
        void Start()
        {
            projectile = Resources.Load("Powers/Projectile") as GameObject;
        }

        // Update is called once per frame
        void Update()
        {
            if (!canFire)
            {
                timer += Time.deltaTime;
                if (timer >= cooldown)
                    canFire = true;
            }

            if (Input.GetMouseButtonDown(0) && canFire)
            {
                GameObject spawnedProjectile = Instantiate(projectile, transform.position, transform.rotation);
                spawnedProjectile.GetComponent<ProjectileBehavior>().parent = transform;
                canFire = false;
            }
        }
    }
}
