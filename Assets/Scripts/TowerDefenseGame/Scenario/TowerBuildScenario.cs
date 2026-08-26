using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Flow;
using Assets.Scripts.TowerDefenseGame.Tower;

namespace Assets.Scripts.TowerDefenseGame.Scenario
{
    public class TowerBuildScenario
    {
        private readonly IScenarioContext _scenarioContext;

        private TowerBindFlow _towerBindFlow;


        public TowerBuildScenario(IScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;

            _towerBindFlow = _scenarioContext.CreateFlow<TowerBindFlow>();

        }


        public void BindTower(TowerActor towerActor)
        {
            _towerBindFlow.BindTower(towerActor);

            //타워 스폰 플로우로 생성받기
            var towerScenario = _scenarioContext.CreateScenario<TowerScenario>();
            towerActor.SetupScenario(towerScenario);
        }

        public void UnbindTower(TowerActor towerActor)
        {
            _towerBindFlow.UnbindTower(towerActor);
        }
    }
}
