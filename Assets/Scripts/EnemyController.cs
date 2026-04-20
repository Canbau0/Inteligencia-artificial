using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform player;
    [SerializeField] private LineOfSight los;
    [SerializeField] private EnemyDecisionTree decisionTree;
    private EnemyContext context;


    [Header("Variables")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotationSpeed = 5f;    

    [SerializeField] private float patrolRadius = 5f;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float patrolWaitTime = 1f;

    [Header("Disparo")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float shootOffset = 1.5f;

    private float fireTimer;
    private Vector3 patrolCenter;
    private Vector3 patrolTarget;
    private float waitTimer;

    private void Start()
    {
        patrolCenter = transform.position;
        SetNewPatrolPoint();
    }
    private void Awake()
    {
        los = GetComponent<LineOfSight>();
        decisionTree = GetComponent<EnemyDecisionTree>();
        context = new EnemyContext { self = transform, player = player, los = los };
    }

    public void Update()
    {
        context.player = player;
        context.distanceToPlayer = Vector3.Distance(transform.position, player.position);

        decisionTree.Evaluate(this, context);
    }

    void SetNewPatrolPoint()
    {
        Vector2 randomCircle = Random.insideUnitCircle * patrolRadius;
        patrolTarget = patrolCenter + new Vector3(randomCircle.x, 0, randomCircle.y);
    }

    public void Patrol()
    {
        Vector3 dir = patrolTarget - transform.position;
        dir.y = 0;

        if (dir.magnitude < 0.2f)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= patrolWaitTime)
            {
                SetNewPatrolPoint();
                waitTimer = 0;
            }

            return;
        }

        Vector3 moveDir = dir.normalized;

        //mover
        transform.position += moveDir * patrolSpeed * Time.deltaTime;

        //rotar
        transform.forward = Vector3.Lerp(
            transform.forward,
            moveDir,
            rotationSpeed * Time.deltaTime
        );
    }

    public void PursuePlayer()
    {
        Vector3 dir = player.position - transform.position;
        dir.y = 0;
        Vector3 moveDir = dir.normalized;

        transform.position += moveDir * speed * Time.deltaTime;

        transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    public void ShootPlayer()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            fireTimer = 0;
            StartCoroutine(BurstFire());
        }
    }

    IEnumerator BurstFire()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawnPos = shootPoint.position + shootPoint.forward * shootOffset;

            GameObject bullet = Instantiate(bulletPrefab, spawnPos, shootPoint.rotation);

            Vector3 dir = (player.position - shootPoint.position).normalized;
            bullet.transform.forward = dir;

            yield return new WaitForSeconds(0.2f);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }

}

//public class EnemyController : MonoBehaviour
//{
//    [Header("Referencias")]
//    [SerializeField] private Transform player;
//    [SerializeField] private LineOfSight los;
//    [SerializeField] private FSMClasses fsm;


//    [Header("Variables")]
//    [SerializeField] private float speed = 3f;
//    [SerializeField] private float rotationSpeed = 5f;
//    [SerializeField] private float patrolRotationSpeed = 10f;


//    private void Awake()
//    {
//        los = GetComponent<LineOfSight>();
//        fsm = GetComponent<FSMClasses>();
//    }


//    void Update()
//    {
//        bool canSeePlayer = los.IsInRange(transform, player) &&
//                            los.IsInAngle(transform, player) &&
//                            los.HasLineOfSight(transform, player);

//        fsm.UpdateFSM(canSeePlayer);
//    }

//    //private void ExecuteState()
//    //{
//    //    switch (fsm.currentState)
//    //    {
//    //        case FSM.EnemyState.Patrol:
//    //            Patrol();
//    //            break;

//    //            case FSM.EnemyState.Pursuit:
//    //            PursuePlayer();
//    //            break;
//    //    }
//    //}

//    private void ExecuteState()
//    {
//        if (fsm.currentState is PatrolState)
//        {
//            Patrol();
//        }

//        else if (fsm.currentState is PursuitState)
//        {
//            PursuePlayer();
//        }
//    }

//    public void Patrol()
//    {
//        transform.Rotate(Vector3.up * patrolRotationSpeed * Time.deltaTime);
//    }

//    public void PursuePlayer()
//    {
//        Vector3 dir = player.position - transform.position;
//        dir.y = 0;
//        Vector3 moveDir = dir.normalized;

//        transform.position += moveDir * speed * Time.deltaTime;

//        transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
//    }
//}

