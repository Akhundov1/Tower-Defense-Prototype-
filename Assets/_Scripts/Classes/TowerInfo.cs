using System;
using UnityEngine;
[Serializable]
public class TowerInfo 
{
    public E_AttackType TowerType;
    public GameObject TowerPrefab;
    public int Price;
    public float Damage;
    [Header ("In Seconds")]
    public float AttackSpeed;
    public float AttackRadius;
}
