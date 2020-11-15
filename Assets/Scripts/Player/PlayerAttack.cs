using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    public GameObject projectile;
    public int damage;

    public abstract void Shoot();
}
