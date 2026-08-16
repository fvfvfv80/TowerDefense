
using System.Collections.Generic;

namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public interface ITowerWeaponHost
    {
        public IEnumerable<BaseActor> GetTargetList();
    }
}
