using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.TowerDefenseGame
{
    public class ObjectFollowMousePositionModule : MonoBehaviour
    {


        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }



        public Vector3 GetPointerWorldPosition()
        {
            Vector2 screenPosition = Mouse.current.position.ReadValue();


            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);

            worldPosition.z = 0f;


            return worldPosition;
        }



        private void Update()
        {

            transform.position = GetPointerWorldPosition();

        }
    }
}