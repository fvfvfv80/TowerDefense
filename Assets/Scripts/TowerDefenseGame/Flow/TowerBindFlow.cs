using Assets.Scripts.Core;
using Assets.Scripts.TowerDefenseGame.Tower;

namespace Assets.Scripts.TowerDefenseGame.Flow
{
    public class TowerBindFlow : IFlow
    {

        private TowerBuffDirector _towerBuffDirector;

        public TowerBindFlow(TowerBuffDirector towerBuffDirector)
        {
            _towerBuffDirector = towerBuffDirector;
        }

        public void BindTower(TowerActor towerActor)
        {
            _towerBuffDirector.RegisterTower(towerActor);
        }

        public void UnbindTower(TowerActor towerActor)
        {
            _towerBuffDirector.RemoveTower(towerActor);
        }
    }
}