using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float life = 100;
    public  float maxHp = 100;

    private void Start()
    {
        GameManager.instance.Init();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BadProjectile"))
        {
            life -= other.gameObject.GetComponent<ProjectileCollision>().DealsDamage();
            Destroy(other.gameObject);
            GetComponent<HPManager>().UpdateHpUI();
            CheckPlayerLife();
        }
    }

    private void CheckPlayerLife()
    {
        if (life <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
