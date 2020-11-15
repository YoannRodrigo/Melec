using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DoorController : MonoBehaviour
{
    public bool isLeft;
    public DoorManager doorManager;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.transform.position.x > transform.position.x)
        {
            GetComponent<BoxCollider>().isTrigger = false;
            doorManager.CloseDoor();
        }
    }

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = isLeft;
    }

    private void Update()
    {
        if (GetComponent<BoxCollider>().isTrigger)
        {
            doorManager.OpenDoor();
        }
    }
}
