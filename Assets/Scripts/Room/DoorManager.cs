using UnityEngine;


[RequireComponent(typeof(Animator))]
public class DoorManager : MonoBehaviour
{
    public DoorController doorController;
    private Animator animator;

    private static readonly int OPEN = Animator.StringToHash("Open");

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        animator.SetBool(OPEN, true);
    }

    public void CloseDoor()
    {
        animator.SetBool(OPEN,false);
    }
}
