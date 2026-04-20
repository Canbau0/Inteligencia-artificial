using UnityEngine;

public class EnemyDecisionTree : MonoBehaviour
{
    private DecisionNode rootNode;

    private void Awake()
    {
        ActionNode patrolNode = new ActionNode(enemy => enemy.Patrol());
        ActionNode pursuitNode = new ActionNode(enemy => enemy.PursuePlayer());
        ActionNode shootNode = new ActionNode(enemy => enemy.ShootPlayer());

        //Esta cerca?
        QuestionNode distanceCheck = new QuestionNode(context => context.distanceToPlayer < 5, shootNode, pursuitNode);

        //Lo veo?
        rootNode = new QuestionNode(
        context =>
            context.los.IsInRange(context.self, context.player) &&
            context.los.IsInAngle(context.self, context.player) &&
            context.los.HasLineOfSight(context.self, context.player), distanceCheck, patrolNode);

    }

    public void Evaluate(EnemyController enemy, EnemyContext context)
    {
        rootNode.Evaluate(enemy, context);
    }

}
