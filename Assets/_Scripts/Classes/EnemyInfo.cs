using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class EnemyInfo 
{
    public E_EnemyType Type;
    public GameObject EnemyPrefab;
    public float Health;
    public float Damage;
    public float MoveSpeed;
    public int Reward;
    public float MaxHealth;

}
