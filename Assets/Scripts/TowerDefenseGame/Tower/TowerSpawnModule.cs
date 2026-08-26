using UnityEngine;

namespace Assets.Scripts.TowerDefenseGame.Tower
{
    public class TowerSpawnModule : MonoBehaviour
    {
        public TowerActor SpawnTower(GameObject towerPrefab, Transform tileTransform)
        {
            Vector3 position = tileTransform.position + Vector3.back;

            var clone = Instantiate(towerPrefab, position, Quaternion.identity);

            var towerActor = clone.GetComponent<TowerActor>();

            towerActor.Init();
            towerActor.Setup();

            return towerActor;
        }

        public void DespawnTower(TowerActor towerActor)
        {
            towerActor.Release();
            Destroy(towerActor.gameObject);

        }

    }
}