using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject g = AtomsManager._instance.SpawnAtom(AtomsManager.AtomAbb.H, new Vector3(3f,1f,0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
