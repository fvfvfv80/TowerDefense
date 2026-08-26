using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;


namespace Assets.Scripts.TowerDefenseGame.Scenario
{
    public class EnemyWaveScenario
    {
        private readonly IScenarioContext _scenarioContext;



        public EnemyWaveScenario(IScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

        }


        public void BindEnemy(EnemyActor enemyActor)
        {
            var enemyScenario = _scenarioContext.CreateScenario<EnemyScenario>();

            enemyActor.SetupScenario(enemyScenario);
        }
    }
}
