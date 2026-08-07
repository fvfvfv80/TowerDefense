using Assets.Scripts.TowerDefenseGame.Enemy;
using Assets.Scripts.TowerDefenseGame.EnemyWave;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.UI
{
    public class UIGamePlay : MonoBehaviour
    {

        //이렇게 해도 되지만 gameplayflow로 묶어도 가능?
        [SerializeField]
        private EnemyWaveDirector enemyWaveDirector; //IFlowHandler?  가능 


        public void StartWave()
        {
            enemyWaveDirector.OrderWaveStart();


        }
        
    }
}