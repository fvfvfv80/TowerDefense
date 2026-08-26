using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Enemy
{
    public class EnemyMovementModule : MonoBehaviour
    {
        [SerializeField]
        private float timeOffset = 1f;

        private Transform[] _wayPoints;

        private float _baseTimeOffset;

        public float TimeOffset
        {
            get => timeOffset;
            set => timeOffset = Mathf.Min(value, 5.0f);
        }

        private int _wayPointCount;
        private int _currentIndex = 0;

        private IEnemyModuleHost _enemyHandler;

        public void Init(IEnemyModuleHost enemyHandler)
        {
            _enemyHandler = enemyHandler;
        }

        public void Setup(Transform[] wayPoints)
        {

            _baseTimeOffset = timeOffset;

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
        public void ResetTimeOffset()
        {
            timeOffset = _baseTimeOffset;
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
                    _enemyHandler.RequestDespawn(EnemyDestroyType.Arrive);
                    break;
                }
            }
        }

        private IEnumerator MoveAToB(Vector3 start, Vector3 end)
        {
            float percent = 0f;
            float moveTime = 0f;
            float distance = Vector3.Distance(start, end);

            while (percent < 1f)
            {
                moveTime = Mathf.Max(distance * timeOffset, 0.0001f);

                percent += Time.deltaTime / moveTime;
                percent = Mathf.Min(percent, 1f);
                transform.position = Vector3.Lerp(start, end, percent);

                yield return null;
            }

            transform.position = end;
        }

    }
}
