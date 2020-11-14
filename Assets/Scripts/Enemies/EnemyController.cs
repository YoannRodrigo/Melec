using RhythmTool;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private float timeSinceLastShot;
    public GameObject projectilePrefab;
    public GameObject firePoint;
    private const float TIME_BEFORE_SHOT = 2;
    public float life;
    private SoundManager soundManager;
    private CollectablesManager collectablesManager;
    private int dropRate = 60;

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
        if(Time.timeScale != 0)
        {
            ShootProjectile();
        }
    }

    private void Start()
    {
        collectablesManager = FindObjectOfType<CollectablesManager>();
        playerRigidbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        if(playerRigidbody)
        {
            transform.LookAt(Vector3.Scale(0.2f * playerRigidbody.velocity + playerRigidbody.position, new Vector3(1, 0, 1)));
        }
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
            int dropChance = Random.Range(0, 101);
            if(dropChance < dropRate)
            {
                int randomDropId = Random.Range(0, collectablesManager.atomsArray.Length);
                collectablesManager.SpawnAtom((CollectablesManager.AtomAbb) randomDropId, transform.position + Vector3.up);
                Destroy(gameObject);
            }
        }
    }
}
