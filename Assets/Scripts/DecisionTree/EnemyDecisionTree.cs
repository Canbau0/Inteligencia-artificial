using UnityEngine;

public class EnemyDecisionTree : MonoBehaviour
{
    private DecisionNode rootNode;

    private void Awake()
    {
        ActionNode patrolNode = new ActionNode(enemy => enemy.Patrol());
        ActionNode pathNode = new ActionNode(enemy => enemy.FollowPath());
        ActionNode shootNode = new ActionNode(enemy => enemy.ShootPlayer());
        ActionNode fleeNode = new ActionNode(enemy => enemy.Flee());


        //Enemigo que huye
        QuestionNode HuyeCheck = new QuestionNode(
            context => context.currentHealth <= 5,
            fleeNode,
            pathNode
        );


        //Enemigo francotirador
        QuestionNode FrancotiradorCheck = new QuestionNode(
            context => context.distanceToPlayer < 8,
            shootNode,
            pathNode
        );


        //Enemigo normal
        QuestionNode NormalCheck = new QuestionNode(
            context => context.distanceToPlayer < 2,
            shootNode,
            pathNode
        );


        rootNode = new QuestionNode(
            context =>
            context.los.IsInRange(context.self, context.player) &&
            context.los.IsInAngle(context.self, context.player) &&
            context.los.HasLineOfSight(context.self, context.player),

            new TipoNode(
                NormalCheck,
                FrancotiradorCheck,
                HuyeCheck
            ),

            patrolNode
        );


    }
    public void Evaluate(EnemyController enemy, EnemyContext context)
    {
        rootNode.Evaluate(enemy, context);
    }
}