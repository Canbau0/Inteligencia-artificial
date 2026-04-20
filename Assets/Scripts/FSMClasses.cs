//using JetBrains.Annotations;
//using Unity.VisualScripting;
//using UnityEngine;

//public class FSMClasses : MonoBehaviour
//{
//    public State currentState { get; private set; }

//    private PatrolState patrolState;
//    private PursuitState pursuitState;

//    [SerializeField] private Transform player;


//    private void Awake()
//    {
//        patrolState = new PatrolState(this);
//        pursuitState = new PursuitState(this);

//        ChangeToPatrol();
//    }

//    public void UpdateFSM(bool canSeePlayer)
//    {
//        currentState?.Update(canSeePlayer);
//    }
//    public Transform GetPlayer()
//    {
//        return player;
//    }

//    public void ChangeState(State newState)
//    {
//        if (currentState == newState)
//        {
//            return;
//        }

//        currentState?.Exit();
//        currentState = newState;
//        currentState.Enter();
//    }

//    public void ChangeToPatrol()
//    {
//        ChangeState(patrolState);
//    }

//    public void ChangeToPursuit() 
//    {
//        ChangeState(pursuitState);
//    }
//}

//public abstract class State
//{
//    protected FSMClasses fsm;

//    public State(FSMClasses fsm)
//    {
//        this.fsm = fsm;
//    }

//    public virtual void Enter() { }
//    public virtual void Exit() { }

//    public abstract void Update(bool canSeePlayer);
//}


//public class PatrolState : State
//{
//    public PatrolState(FSMClasses fsm) : base(fsm) { }
    

//    public override void Enter()
//    {
//        Debug.Log("Entró a Patrol");
//    }


//    public override void Exit()
//    {
//        Debug.Log("Salió de Patrol");
//    }

//    public override void Update (bool canSeePlayer)
//    {
//        fsm.transform.Rotate(Vector3.up * 50f * Time.deltaTime);

//        if (canSeePlayer)
//        {
//            fsm.ChangeToPursuit();
//        }
//    }

//}


//public class PursuitState : State
//{
//    public PursuitState(FSMClasses fsm) : base(fsm) { }    

//    public override void Update(bool canSeePlayer)
//    {
//        Transform player = fsm.GetPlayer();

//        Vector3 dir = player.position - fsm.transform.position;
//        dir.y = 0;

//        Vector3 moveDir = dir.normalized;

//        // mover enemigo
//        fsm.transform.position += moveDir * 3f * Time.deltaTime;

//        // rotar
//        fsm.transform.forward = Vector3.Lerp(fsm.transform.forward,moveDir,Time.deltaTime * 5f);

//        if (!canSeePlayer)
//        {
//            fsm.ChangeToPatrol();
//        }
//    }
//}