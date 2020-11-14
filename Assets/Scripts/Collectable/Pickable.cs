using UnityEngine;

public class Pickable : MonoBehaviour
{

    public Atom atomType;

    private void Awake()
    {
        gameObject.transform.localScale = new Vector3(.5f,.5f,.5f);
        gameObject.transform.Rotate(50f,0,0);
    }

    public void OnTriggerEnter(Collider other){
        other.GetComponent<Inventory>().Add(atomType);
        Destroy(gameObject);
    }
}
