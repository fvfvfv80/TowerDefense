using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Flow;
using Assets.Scripts.TowerDefenseGame.Tower;

namespace Assets.Scripts.TowerDefenseGame.Scenario
{
    public class TowerBuildScenario : ITowerBuildScenario
    {
        private readonly IScenarioContext _scenarioContext;

        private readonly TowerBindFlow _towerBindFlow;


        public TowerBuildScenario(IScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            _towerBindFlow = _scenarioContext.CreateFlow<TowerBindFlow>();

        }


        public void NotifyTowerBuilt(TowerActor towerActor)
        {
            _towerBindFlow.BindTower(towerActor);

            var towerScenario = _scenarioContext.CreateScenario<TowerScenario>();
            towerActor.SetupScenario(towerScenario);
        }

        public void NotifyTowerDemolished(TowerActor towerActor)
        {
            _towerBindFlow.UnbindTower(towerActor);
        }
    }
}
