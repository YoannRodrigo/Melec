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
        if (playerTransform.position.z > transform.position.z + 4.5f)
        {
            transform.position += new Vector3(0,0,9);
        }
        else if  (playerTransform.position.z < transform.position.z - 4.5f)
        {
            transform.position += new Vector3(0,0,-9);
        }
    }
}
