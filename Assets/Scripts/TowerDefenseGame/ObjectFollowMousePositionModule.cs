using System.Collections;
using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame
{
    public class ObjectFollowMousePositionModule : MonoBehaviour
    {

        private PlayerInputModule _playerInput;

        public void Setup(PlayerInputModule playerInput)
        {
            _playerInput = playerInput;
        }

        private void Update()
        {
            if (_playerInput == null)
                return;

            transform.position = _playerInput.GetPointerWorldPosition();
        }
    }
}