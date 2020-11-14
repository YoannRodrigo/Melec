using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    private int launcherInstanceId;

    public void SetLauncherInstanceId(int launcherInstanceId)
    {
        this.launcherInstanceId = launcherInstanceId;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(launcherInstanceId != other.gameObject.GetInstanceID())
        {
            Destroy(gameObject);
        }
    }
}
