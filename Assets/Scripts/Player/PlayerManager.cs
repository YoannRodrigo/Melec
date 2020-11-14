using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    private float life = 100;

    private void Start()
    {
        GameManager.instance.Init();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("BadProjectile"))
        {
            life -= 5;
            Destroy(other.gameObject);
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
