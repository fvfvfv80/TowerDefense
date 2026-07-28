using Assets.Scripts.TowerDefenseGame.Tower;
using Unity.Mathematics;
using UnityEngine;

public class TowerSpawnFlow : MonoBehaviour
{
    [SerializeField]
    private TowerTemplateSO towerTemplate;

    [SerializeField]
    private TowerDefenseTowerRole towerRole;

    public int BuildCost => towerTemplate.weapon[0].cost;

    public TowerActor SpawnTower(Transform tileTransform)
    {
        Vector3 position = tileTransform.position + Vector3.back;
        
        var clone = Instantiate(towerTemplate.towerPrefab, position, quaternion.identity);

        var towerActor = clone.GetComponent<TowerActor>();

        towerActor.Init(towerRole);
        towerActor.Setup();

        return towerActor;
    }

    public GameObject SpawnFollowTower()
    {
        var clone = Instantiate(towerTemplate.followTowerPrefab);
        return clone;

    }

    //디스폰 시 처리해야되는 작업들을 여기서 처리
    public void DespawnTower(TowerActor towerActor)
    {
        Destroy(towerActor.gameObject);
    }

}
