using Assets.Scripts.TowerDefenseGame.Player;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerBuildInputModule : MonoBehaviour
{
    private IPlayerHandler _playerHandler;

    private Camera _mainCamera;
    private Vector2 _screenPoint;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void Init(IPlayerHandler playerHandler)
    {
        _playerHandler = playerHandler;
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        _screenPoint = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        
        if (!context.performed)
            return;

        if (!context.ReadValueAsButton())
            return;

        Ray ray = _mainCamera.ScreenPointToRay(_screenPoint);

        if (Physics.Raycast(ray,out var hit,Mathf.Infinity))
        {
            if(hit.transform.CompareTag("Tile"))
            {
                _playerHandler.BuildTower(hit.transform);
            }
        }
    }


}
