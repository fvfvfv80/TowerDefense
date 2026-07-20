using Assets.Scripts.TowerDefenseGame.Enemy;
using Assets.Scripts.TowerDefenseGame.Player;
using Unity.Mathematics;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;


    public GameObject SpawnTower(Transform tileTransform)
    {

        //선택한 위치에 타워 생성
        var clone = Instantiate(towerPrefab, tileTransform.position, quaternion.identity);

       
        return clone;
    }

}
