using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public static WaveManager instance;

    [System.Serializable]
    public struct SpawnableUnits
    {
        public int minWave;
        public Unit unit;
    }
    public SpawnableUnits[] spawnableUnits;

    public int wave;
    public float spawnTime = 5;
    public List<Unit> spawnedUnits = new List<Unit>();

    private float spawnTimer;
    private bool startedTimer;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        }

        instance = this;
    }

    private void Update()
    {
        if(spawnedUnits.Count <= 0)
        {
            // no enemies
            if(!startedTimer)
            {
                spawnTimer = Time.time + spawnTime;
                startedTimer = true;
            }
            if(Time.time >= spawnTimer)
            {
                // ready to spawn
                SpawnEnemies();
            }
        }
    }

    void SpawnEnemies()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        wave++;
        for(int i = 0; i < wave * 3; i++)
        {
            SpawnableUnits unitToSpawn = spawnableUnits[UnityEngine.Random.Range(0, spawnableUnits.Length)];
            while(wave < unitToSpawn.minWave)
            {
                unitToSpawn = spawnableUnits[UnityEngine.Random.Range(0, spawnableUnits.Length)];
            }
            GameObject newEnemy = Instantiate(unitToSpawn.unit.gameObject, transform.position, transform.rotation);
            Unit newEnemyUnit = newEnemy.GetComponent<Unit>();

            newEnemyUnit.health.curHealth += wave * 20;

            spawnedUnits.Add(newEnemyUnit);

            yield return new WaitForSeconds(1.5f);
        }
        startedTimer = false;
    }

}
