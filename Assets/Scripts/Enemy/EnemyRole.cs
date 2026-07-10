using System.Collections;
using UnityEngine;

public class EnemyRole : MonoBehaviour
{
    [SerializeField]
    private float timeOffset = 1f;      


    private Transform[] _wayPoints;     
    private int _wayPointCount;
    private int _currentIndex = 0;
    

    public void Init(BaseActor actor)
    {

    }

    public void Setup(Transform[] wayPoints)
    {
        //적 이동 경로 정보 설정
        _wayPointCount = wayPoints.Length;
        _wayPoints = new Transform[_wayPointCount];
        _wayPoints = wayPoints;

        //적 위치를 첫 번째 wayPoint 위치로 설정
        transform.position = _wayPoints[_currentIndex].position;

        _currentIndex++;

        // 적 이동 제어
        StartCoroutine(nameof(Process));

    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, 360 * Time.deltaTime);
    }

    private IEnumerator Process()
    {
        while(true)
        {
            //현재 위치에서 목표 위치까지 이동
            yield return StartCoroutine(MoveAToB(transform.position, _wayPoints[_currentIndex].position));

            //다음 이동 위치 설정
            if (_currentIndex < _wayPointCount - 1) _currentIndex++;
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator MoveAToB(Vector3 start, Vector3 end)
    {
        float percent = 0f;
        float moveTime = Vector3.Distance(start, end) * timeOffset;

        while(percent < 1f)
        {
            percent += Time.deltaTime / moveTime;
            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }
    }
}
