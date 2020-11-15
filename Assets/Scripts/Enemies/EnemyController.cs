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
    internal SoundManager soundManager;
    internal CollectablesManager collectablesManager;
    private int nbBeat;
    private const int DROP_RATE = 100;
    internal bool isSlow;
    private bool isDoT;
    private float timeSinceLastDoT;
    private bool isDead;
    private const float TIME_BEFORE_DOT = 1f;


    protected virtual void OnEnable()
    {
        if (!soundManager)
        {
            soundManager = FindObjectOfType<SoundManager>();
        }
        else
        {
            soundManager.GetEventProvider().Register<Beat>(OnBeat);
            soundManager.GetEventProvider().Register<Onset>(OnSetEvent);
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
            soundManager.GetEventProvider().Unregister<Beat>(OnBeat);
            soundManager.GetEventProvider().Unregister<Onset>(OnSetEvent);
        }
    }

    protected virtual void OnSetEvent(Onset onset)
    {
    }
    

    protected virtual void OnBeat(Beat beat)
    {
        if(Time.timeScale != 0 && GameManager.instance.gameState == GameManager.GameStates.GAME)
        {
            ShootProjectile();
        }
        else if(isSlow || GameManager.instance.gameState == GameManager.GameStates.INVENTORY && nbBeat % 2 == 0)
        {
            nbBeat++;
            ShootProjectile();
        }
    }

    protected virtual void Start()
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

        timeSinceLastDoT += Time.deltaTime;
        if (isDoT && timeSinceLastDoT > TIME_BEFORE_DOT)
        {
            timeSinceLastDoT = 0f;
            life -= 1;
            LifeCheck();
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
            Collectable collectable = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().GetCollectibleAttack();
            isSlow = collectable.atomAbb == CollectablesManager.AtomAbb.CL;
            isDoT = collectable.atomAbb == CollectablesManager.AtomAbb.S;
            life -= other.gameObject.GetComponent<ProjectileCollision>().DealsDamage();
            LifeCheck();
        }
    }

    private void LifeCheck()
    {
        if (life <= 0 && !isDead)
        {
            isDead = true;
            Drop();
            Destroy(gameObject);
        }
    }

    protected virtual void Drop()
    {
        int dropChance = Random.Range(0, 101);
        if(dropChance < DROP_RATE)
        {
            int randomDropId = Random.Range(0, collectablesManager.atomsArray.Length);
            collectablesManager.SpawnAtom((CollectablesManager.AtomAbb) randomDropId, transform.position + Vector3.up);
        }
    }
}
