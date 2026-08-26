using Assets.Scripts.TowerDefenseGame.TowerBuff;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower.Feature
{

    public interface ITowerSupportHost
    {
        public IEnumerable<BaseActor> RequestBuffTargetList();

    }
    //타워 서포트 모듈// 타워 데미지 버프 모듈로 분리 가능
    public class TowerDamageSupportModule : MonoBehaviour
    {

        [SerializeField]
        private TowerStatModule towerStatModule;

        private ITowerSupportHost _supportHost;

        private int _level = 0;

        private float _buff;

        private float _range;

        public void Init(TowerActor towerActor)
        {
            _supportHost = towerActor;
        }

        public void Setup()
        {
            UpdateStat(0);
        }

        public void ApplyLevel(int level)
        {
            OffBuffAroundTower();
            UpdateStat(level);
            OnBuffAroundTower();
        }



        public void StartSupport()
        {
            OnBuffAroundTower();
        }

        public void EndSupport()
        {
            OffBuffAroundTower();
        }

        

        public void OnBuffAroundTower()
        {
            var buffTargetList = _supportHost.RequestBuffTargetList();

            foreach(var target in buffTargetList)
            {
                var towerActor = target as TowerActor;

                TryApplyBuff(towerActor);
            }
        }

        public void OffBuffAroundTower()
        {
            var buffTargetList = _supportHost.RequestBuffTargetList();

            foreach (var target in buffTargetList)
            {
                var towerActor = target as TowerActor;

                TryRemoveBuff(towerActor);
            }
        }

        public void TryApplyBuff(TowerActor towerActor)
        {
            if (Vector3.Distance(towerActor.transform.position, transform.position) <= _range)
            {
                if (towerActor.TowerType == TowerType.Cannon || towerActor.TowerType == TowerType.Laser)
                {
                    towerActor.StatModule.ApplyBuff(new TowerDamageBuff(_level, _buff));
                }
            }
        }

        public void TryRemoveBuff(TowerActor towerActor)
        {
            if (Vector3.Distance(towerActor.transform.position, transform.position) <= _range)
            {
                if (towerActor.TowerType == TowerType.Cannon || towerActor.TowerType == TowerType.Laser)
                {
                    towerActor.StatModule.RemoveBuff(new TowerDamageBuff(_level, _buff));
                }
            }
        }

        private void UpdateStat(int level)
        {
            _level = level;
            _buff = towerStatModule.BaseSpec.buff;
            _range = towerStatModule.BaseSpec.range;
        }

    }
}