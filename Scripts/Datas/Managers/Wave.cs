using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Datas/WaveDatas", fileName = "Wave_")]
public class Wave : ScriptableObject
{
    public EnemyType enemy;
    public int count;
    public float rate;
}
