using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{

    public Collectable atomType;
    private bool isRare;
    public List<Pickable> otherRare = new List<Pickable>();

    public void SetIsRare()
    {
        isRare = true;
    }
    
    private void Awake()
    {
        gameObject.transform.localScale = new Vector3(.5f,.5f,.5f);
        gameObject.transform.Rotate(50f,0,0);
    }
    
    public void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player") && other.GetComponent<Inventory>().Add(atomType)) {
            if (isRare)
            {
                foreach (Pickable pickable in otherRare)
                {
                    Destroy(pickable.gameObject);
                }
            }
            Destroy(gameObject);
            
        }
        
    }
}
