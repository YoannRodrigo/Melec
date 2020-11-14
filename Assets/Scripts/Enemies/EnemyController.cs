using System;
using RhythmTool;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private float timeSinceLastShot;
    public GameObject projectilePrefab;
    public GameObject firePoint;
    private const float TIME_BEFORE_SHOT = 2;
    public float life;
    private SoundManager soundManager;

    private void OnEnable()
    {
        if (!soundManager)
        {
            soundManager = FindObjectOfType<SoundManager>();
        }
        else
        {
            soundManager.GetEventProvider().Register<Onset>(OnsetEvent);
        }
    }

    private void OnDestroy()
    {
        ClearData();
    }

    private void OnDisable()
    {
        ClearData();
    }

    private void ClearData()
    {
        if(soundManager.GetEventProvider())
        {
            soundManager.GetEventProvider().Unregister<Onset>(OnsetEvent);
        }
    }

    private void OnsetEvent(Onset onset)
    {
        ShootProjectile();
    }

    private void Start()
    {
        playerRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        transform.LookAt(Vector3.Scale(playerRigidbody.velocity + playerRigidbody.position, new Vector3(1, 0, 1)));
        /*if (timeSinceLastShot > TIME_BEFORE_SHOT)
        {
            timeSinceLastShot = 0;
            ShootProjectile();
        }
        timeSinceLastShot += Time.deltaTime;*/
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);
        projectile.GetComponent<ProjectileCollision>().SetLauncherInstanceId(gameObject.GetInstanceID());
        projectile.GetComponent<Rigidbody>().AddForce(2*transform.forward,ForceMode.Impulse);
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name.Contains("GoodProjectile"))
        {
            life--;
            LifeCheck();
        }
    }

    private void LifeCheck()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
