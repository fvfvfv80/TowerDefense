using Assets.Scripts.TowerDefenseGame.Player;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputModule : MonoBehaviour
{
    private IPlayerInputHost _playerInputHost;

    private Camera _mainCamera;
    private Vector2 _screenPoint;

    private bool _isTowerBuild = false;
    private bool _isTowerMaintenance = false;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    public void Init(IPlayerInputHost playerHandler)
    {
        _playerInputHost = playerHandler;
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

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("Tile"))
            {
                _playerInputHost.RequesteBuildTower(hit.transform);
            }
            else if (hit.transform.CompareTag("Tower"))
            {
                _playerInputHost.RequestSelectTower(hit.transform);
                _isTowerMaintenance = true;
            }
        }
    }

    public void OnTowerBuildButtonClick()
    {
        _isTowerBuild = true;
        _playerInputHost.RequestEnterTowerBuild();
    }

    private void OnEscape()
    {
        if (_isTowerBuild)
        {
            _isTowerBuild = false;
            _playerInputHost.RequestCancelTowerBuild();
        }

        if (_isTowerMaintenance)
        {
            _isTowerMaintenance = false;
            _playerInputHost.RequestCancelTowerMaintenance();
        }


    }

    public void OnTowerUpgradeButtonClick()
    {
        _playerInputHost.RequestUpgradeTower();
    }

    public void OnTowerSellButtonClick()
    {
        _playerInputHost.RequestSellTower();
    }




    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame ||
            Mouse.current.rightButton.wasPressedThisFrame)

        {
            OnEscape();
        }
    }




}
