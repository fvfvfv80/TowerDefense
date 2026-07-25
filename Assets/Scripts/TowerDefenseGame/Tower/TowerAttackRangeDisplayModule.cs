using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class TowerAttackRangeDisplayModule : MonoBehaviour
    {
        public void OnAttackRange(Vector3 position, float range)
        {
            gameObject.SetActive(true);

            float diameter = range * 2.0f;
            transform.localScale = Vector3.one * diameter;

            transform.position = position;

        }

        public void OffAttackRange()
        {
            gameObject.SetActive(false);
        }

    }
}