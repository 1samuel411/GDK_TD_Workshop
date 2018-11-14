using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    // Static Ref
    public static Spawner instance;

    // Structs
    [System.Serializable]
    public struct SpawnableUnit
    {
        public int minimumWave;
        public Unit unit;
    }

    // Public Variables
    public SpawnableUnit[] spawnableUnits;
    public int wave;
    public bool spawningEnemies;
    [HideInInspector]
    public List<Unit> spawnedUnits;
    public float nextWaveTime = 5;
    public float spawnRate;

    // Private Variables
    private float nextWaveTimer;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        nextWaveTimer = Time.time + nextWaveTime;
    }

    void Update()
    {
        if(Time.time >= nextWaveTimer && spawnedUnits.Count <= 0 && !spawningEnemies)
        {
            // Spawn next wave
            StartCoroutine(NextWave());
        }
    }

    IEnumerator NextWave()
    {
        spawningEnemies = true;
        wave++;
        int numToSpawn = CalculateSpawnCount();
        for(int i = 0; i < numToSpawn; i++)
        {
            yield return new WaitForSeconds(spawnRate);
            SpawnUnit();
        }
        nextWaveTimer = Time.time + nextWaveTime;
        spawningEnemies = false;
    }

    void SpawnUnit()
    {
        // Get random unit
        SpawnableUnit unitToSpawn = spawnableUnits[Random.Range(0, spawnableUnits.Length)];

        // If wave is greater than min wave
        while(wave < unitToSpawn.minimumWave)
        {
            // Get a new one
            unitToSpawn = spawnableUnits[Random.Range(0, spawnableUnits.Length)];
        }

        // Spawn
        GameObject newUnit = GameObject.Instantiate(unitToSpawn.unit.gameObject, transform.position, transform.rotation);
        Unit spawnedUnit = newUnit.GetComponent<Unit>();
        spawnedUnit.health.SetHealth(CalculateUnitHealth(spawnedUnit.health.health));
        spawnedUnit.attackDamage = CalculateUnitDamage(spawnedUnit.attackDamage);
        spawnedUnit.attackRate = CalculateUnitRate(spawnedUnit.attackRate);

        spawnedUnits.Add(spawnedUnit);
    }

    int CalculateUnitHealth(int initHealth)
    {
        int newHealth = (int)(Mathf.Pow(1.1f, wave) - 1) + initHealth;
        return newHealth;
    }

    int CalculateUnitDamage(int initDamage)
    {
        int newDmg = (int)(Mathf.Pow(1.05f, wave) - 1) + initDamage;
        return newDmg;
    }

    float CalculateUnitRate(float initRate)
    {
        float newRate = -(Mathf.Pow(1.075f, wave) - 1) + initRate;
        return newRate;
    }

    int CalculateSpawnCount()
    {
        int count = (int)(Mathf.Pow(1.2f, wave)) + 1;
        return count;
    }
}
