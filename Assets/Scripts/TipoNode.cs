public class TipoNode : DecisionNode
{
    private DecisionNode Normal;
    private DecisionNode Francotirador;
    private DecisionNode Huye;


    public TipoNode(
        DecisionNode Normal,
        DecisionNode Francotirador,
        DecisionNode Huye)
    {
        this.Normal = Normal;
        this.Francotirador = Francotirador;
        this.Huye = Huye;
    }


    public override void Evaluate(EnemyController enemy, EnemyContext context)
    {
        switch (context.enemyType)
        {
            case EnemyController.EnemyType.Normal:
                Normal.Evaluate(enemy, context);
                break;


            case EnemyController.EnemyType.Francotirador:
                Francotirador.Evaluate(enemy, context);
                break;


            case EnemyController.EnemyType.Huye:
                Huye.Evaluate(enemy, context);
                break;
        }
    }
}
