using System.Collections;
using UnityEngine;
namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemyAnimationModule : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer enemyRenderer;

        private void Update()
        {

            enemyRenderer.transform.Rotate(Vector3.forward, 360 * Time.deltaTime);
        }

        public void PlayHitAnimation()
        {
            StopCoroutine(nameof(HitAlphaAnimation));
            StartCoroutine(nameof(HitAlphaAnimation));
        }

        private IEnumerator HitAlphaAnimation()
        {
            Color color = enemyRenderer.color;

            color.a = 0.4f;
            enemyRenderer.color = color;

            yield return new WaitForSeconds(0.05f);

            color.a = 1.0f;
            enemyRenderer.color = color;

        }
    }
}

