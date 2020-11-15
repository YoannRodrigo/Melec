﻿using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    private int launcherInstanceId;

    public void SetLauncherInstanceId(int launcherInstanceId)
    {
        this.launcherInstanceId = launcherInstanceId;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<ProjectileCollision>())
        {
            Physics.IgnoreCollision(transform.GetComponent<Collider>(), other.collider);
        }
        else if(launcherInstanceId != other.gameObject.GetInstanceID())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
