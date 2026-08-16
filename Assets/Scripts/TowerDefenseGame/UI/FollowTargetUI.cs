using System.Collections;
using UnityEngine;

public class FollowTargetUI : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private RectTransform _rectTransform;
    private Camera _mainCamera;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _mainCamera = Camera.main;
    }

    public void SetTarget(Transform target) => this.target = target;

    private void LateUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        _rectTransform.position = _mainCamera.WorldToScreenPoint(target.position);
    }

}
