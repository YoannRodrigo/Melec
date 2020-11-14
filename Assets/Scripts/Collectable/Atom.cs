using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Atom", menuName = "ScriptableObjects/Atom", order = 1)]
public class Atom : ScriptableObject
{
    public string name;
    public AtomsManager.AtomAbb abbreviation;
    public Sprite sprite;

}
