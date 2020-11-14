using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;
    
    private void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if(playerTransform)
        {
            if (playerTransform.position.x > transform.position.x + 8f)
            {
                transform.position += new Vector3(16, 0, 0);
            }
            else if (playerTransform.position.x < transform.position.x - 8f)
            {
                transform.position += new Vector3(-16, 0, 0);
            }
        }
    }
}
