using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;
[Serializable]
public class WaveInfo 
{

    public List<EnemyQuantity> Enemies;
    public float WaveDuration=30.0f;
    public bool IsWaveDone;
    public bool IsWaveLaunched;
}
