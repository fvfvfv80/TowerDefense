using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{
    [CreateAssetMenu]
    public class TowerTemplateSO : ScriptableObject
    {
        public GameObject towerPrefab;
        public GameObject followTowerPrefab;
        public WeaponSpec[] weapon;

        [System.Serializable]
        public struct WeaponSpec
        {
            public Sprite sprite;   // 보여지는 타워 이미지
            public float damage;    // 공격력
            public float slow;      // 감속 퍼센트
            public float buff;      // 공격력 증가율
            public float rate;      // 공격 속도
            public float range;     // 공격 범위
            public int cost;        // 필요 골드
            public int sell;        // 타워 판매 시 획득 골드
        }

    }
}