using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public interface ITowerWeaponHandler
    {
        public IEnumerable<BaseActor> GetTargetList();
    }
}
