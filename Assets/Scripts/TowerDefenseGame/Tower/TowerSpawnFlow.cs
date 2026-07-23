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
        Vector3 position = tileTransform.position + Vector3.back;
        
        var clone = Instantiate(towerPrefab, position, quaternion.identity);

        var towerActor = clone.GetComponent<TowerActor>();

        towerActor.Init(towerRole);
        towerActor.Setup();

        return towerActor;
    }

}
