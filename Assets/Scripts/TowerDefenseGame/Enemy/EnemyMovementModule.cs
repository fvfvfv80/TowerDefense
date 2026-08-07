using System.Collections;
using UnityEngine;


namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemyMovementModule : MonoBehaviour
    {
        [SerializeField]
        private float timeOffset = 1f;

        private Transform[] _wayPoints;
        private int _wayPointCount;
        private int _currentIndex = 0;


        private IEnemyModuleHost _enemyHandler;

        public void Init(IEnemyModuleHost enemyHandler)
        {
            _enemyHandler = enemyHandler;
        }

        public void Setup(Transform[] wayPoints)
        {

            _wayPointCount = wayPoints.Length;
            _wayPoints = new Transform[_wayPointCount];
            _wayPoints = wayPoints;

            transform.position = _wayPoints[_currentIndex].position;

            _currentIndex++;

        }

        public void StartMove()
        {
            // 적 이동 제어
            StartCoroutine(nameof(Process));
        }

        private IEnumerator Process()
        {
            while (true)
            {
                //현재 위치에서 목표 위치까지 이동
                yield return StartCoroutine(MoveAToB(transform.position, _wayPoints[_currentIndex].position));

                //다음 이동 위치 설정
                if (_currentIndex < _wayPointCount - 1) _currentIndex++;
                else
                {
                    _enemyHandler.HandleDespawn(EnemyDestroyType.Arrive);
                }
            }
        }

        private IEnumerator MoveAToB(Vector3 start, Vector3 end)
        {
            float percent = 0f;
            float moveTime = Vector3.Distance(start, end) * timeOffset;

            while (percent < 1f)
            {
                percent += Time.deltaTime / moveTime;
                transform.position = Vector3.Lerp(start, end, percent);

                yield return null;
            }
        }
    }
}
