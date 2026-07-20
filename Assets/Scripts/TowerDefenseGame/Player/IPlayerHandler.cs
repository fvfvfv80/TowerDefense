using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Player
{
    public interface IPlayerHandler
    {
        void BuildTower(Transform tileTransform);
    }
}
