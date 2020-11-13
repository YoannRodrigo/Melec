using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private float timeSinceLastShot;
    public GameObject projectilePrefab;
    public GameObject firePoint;
    private const float TIME_BEFORE_SHOT = 2;

    private void Start()
    {
        playerRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.LookAt(Vector3.Scale(playerRigidbody.velocity + playerRigidbody.position, new Vector3(1, 0, 1)));
        if (timeSinceLastShot > TIME_BEFORE_SHOT)
        {
            timeSinceLastShot = 0;
            ShootProjectile();
        }
        timeSinceLastShot += Time.deltaTime;
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);
        projectile.GetComponent<ProjectileCollision>().SetEnemyInstanceId(gameObject.GetInstanceID());
        projectile.GetComponent<Rigidbody>().AddForce(2*transform.forward,ForceMode.Impulse);
    }
}
