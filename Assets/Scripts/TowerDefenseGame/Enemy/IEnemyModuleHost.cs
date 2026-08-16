using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public enum EnemyDestroyType { Kill = 0, Arrive }
    public interface IEnemyModuleHost
    {
        public void RequestHit();

        public void RequestReachGoal(EnemyDestroyType type);
    }
}
