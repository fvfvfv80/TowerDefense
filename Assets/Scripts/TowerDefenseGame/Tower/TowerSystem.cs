using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class TowerSystem : MonoBehaviour
    {

        private TowerSpawner _towerSpawner;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _towerSpawner = GetComponent<TowerSpawner>();
        }


    }
}