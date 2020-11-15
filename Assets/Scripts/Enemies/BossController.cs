using System.Collections.Generic;
using DG.Tweening;
using RhythmTool;
using UnityEngine;

public class BossController : EnemyController
{
    public class Attack
    {
        private int nbProjectile;
        private GameObject projectile;
        
        public Attack(int nbProjectile = 0, GameObject projectile = null)
        {
            this.projectile = projectile;
            this.nbProjectile = nbProjectile;
        }
    }
    
    private Attack wavesAttack;
    private Attack explosionAttack;
    private Attack circleAttack;
    public GameObject explosiveProjectile;
    public List<Transform> alternativeFirePoints = new List<Transform>();
    
    private enum AttackStyle
    {
        WAVES,
        EXPLOSION,
        HALF_CIRCLE,
    }

    protected override void Start()
    {
        base.Start();
        DOTween.Init();
        InitWavesAttack();
        InitExplosionAttack();
        InitCircleAttack();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (soundManager)
        {
            
        }
    }

    protected override void OnBeat(Beat beat)
    {
    }

    private void InitWavesAttack()
    {
        wavesAttack = new Attack(4,projectilePrefab);
    }
    
    private void InitExplosionAttack()
    {
        explosionAttack = new Attack(1,explosiveProjectile);
    }
    
    private void InitCircleAttack()
    {
        circleAttack = new Attack(8,projectilePrefab);
    }

    protected override void OnSetEvent(Onset onset)
    {
        WavesAttack();
    }


    private void WavesAttack()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.LookRotation(firePoint.transform.position - (transform.position + new Vector3(0,firePoint.transform.position.y,0)), Vector3.up));
        newProjectile.GetComponent<Rigidbody>().AddForce(5*newProjectile.transform.forward,ForceMode.Impulse);
        foreach (Transform alternativeFirePoint in alternativeFirePoints)
        {
            newProjectile = Instantiate(projectilePrefab, alternativeFirePoint.position, Quaternion.LookRotation(alternativeFirePoint.position- (transform.position + new Vector3(0,alternativeFirePoint.position.y,0)), Vector3.up));
            newProjectile.GetComponent<Rigidbody>().AddForce(5*newProjectile.transform.forward,ForceMode.Impulse);
        }
    }
}
