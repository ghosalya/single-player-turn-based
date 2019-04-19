using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="EnemySquadSet")]
public class EnemySquadSet : ScriptableObject
{
    public List<GameObject> enemiesToDeploy;
    public List<Vector2> enemyPosition;
}
