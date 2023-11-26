using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemyWave", menuName = "Enemy/Wave")]
public class EnemyWave : ScriptableObject
{
    public List<GameObject> enemies;
}
