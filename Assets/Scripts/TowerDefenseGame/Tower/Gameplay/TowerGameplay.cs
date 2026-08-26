using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower.GamePlay
{
    public enum TowerGameplayCommandType
    {
        ApplyBuff,
    }
    public struct TowerGameplayCommand
    {
        public TowerGameplayCommandType type;
        public TowerActor buffTargetTower; 


    }
    public abstract class TowerGameplay : MonoBehaviour
    {

        public abstract void Init(TowerActor towerActor);

        public abstract void Setup();

        public abstract void StartGameplay();

        public virtual void ApplyLevel(int level) { }
   
        public virtual void HandleCommand(TowerGameplayCommand command) { }

        public abstract void Release();
    }
}