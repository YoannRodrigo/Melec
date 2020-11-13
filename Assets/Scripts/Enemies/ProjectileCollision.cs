using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    private int enemyInstanceId;

    public void SetEnemyInstanceId(int enemyInstanceId)
    {
        this.enemyInstanceId = enemyInstanceId;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if(enemyInstanceId != other.gameObject.GetInstanceID())
        {
            Destroy(gameObject);
        }
    }
}
