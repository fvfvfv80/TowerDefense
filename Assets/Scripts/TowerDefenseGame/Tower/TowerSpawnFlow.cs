using Assets.Scripts.TowerDefenseGame.Tower;
using Unity.Mathematics;
using UnityEngine;

public class TowerSpawnFlow : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;

    [SerializeField]
    private TowerDefenseTowerRole towerRole;

    public TowerActor SpawnTower(Transform tileTransform)
    {
        //선택한 위치에 타워 생성
        var clone = Instantiate(towerPrefab, tileTransform.position, quaternion.identity);

        var towerActor = clone.GetComponent<TowerActor>();

        towerActor.Init(towerRole);
        towerActor.Setup();

        return towerActor;
    }

}
