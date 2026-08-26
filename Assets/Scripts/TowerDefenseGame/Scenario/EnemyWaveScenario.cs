using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Enemy;
using Assets.Scripts.TowerDefenseGame.EnemyWave;


namespace Assets.Scripts.TowerDefenseGame.Scenario
{
    public class EnemyWaveScenario: IEnemyWaveScenario,IScenario
    {
        private readonly IScenarioContext _scenarioContext;



        public EnemyWaveScenario(IScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

        }

        public void NotifyEnemySpawned(EnemyActor enemyActor)
        {
            var enemyScenario = _scenarioContext.CreateScenario<EnemyScenario>();

            enemyActor.SetupScenario(enemyScenario);
        }
    }
}
