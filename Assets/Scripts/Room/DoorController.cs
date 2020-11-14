using UnityEngine;

public class DoorController : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.position.x > transform.position.x)
        {
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
}
