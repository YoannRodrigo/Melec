using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Atom", menuName = "ScriptableObjects/Atom", order = 1)]
public class NewBehaviourScript : ScriptableObject
{
    public string name;
    public AtomsManager.AtomAbb abbreviation;
    public Material material;
    public Sprite sprite;
    public GameObject overworldGO;
}
