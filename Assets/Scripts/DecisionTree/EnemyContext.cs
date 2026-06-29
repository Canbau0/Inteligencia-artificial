using UnityEngine;

public class EnemyContext : MonoBehaviour
{
    public Transform self;
    public Transform player;
    public LineOfSight los;
    public float distanceToPlayer;
    public int currentHealth;
    public EnemyController.EnemyType enemyType;
}
