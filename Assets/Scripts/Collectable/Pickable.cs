using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{

    public Atom atomType;

    public void OnTriggerEnter(Collider other){
        other.GetComponent<Inventory>().Add(atomType);
        Destroy(gameObject);
    }
}
