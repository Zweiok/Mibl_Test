using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemiesFactory : MonoBehaviour, IDifficultyInfluenced 
{
    [SerializeField] private BoxCollider enemiesSpawnArea;

    private float currentSpawnDealy;

    private EnemiesSettings _settings;

    private float _difficulty;

    private Transform _targetToFollow;

    private float _enemiesSpeed;

    private List<Enemy> spawnedEnemies;

    public void UpdateDifficulty(float difficulty)
    {
        _difficulty = difficulty;
        UpdateSpawnDelay();
        UpdateEnemiesSpeed();
    }

    public void RunFactory(EnemiesSettings settings, float initialDifficulty, Transform targetToFollow)
    {
        // Object pool should be connected here (if spend more time)
        _settings = settings;
        _targetToFollow = targetToFollow;
        UpdateDifficulty(initialDifficulty);
        StartCoroutine(RunSpawnProcess());
    }

    public void StopFactory()
    {
        StopAllCoroutines();
        RemoveEnemies();
    }

    private void UpdateSpawnDelay()
    {
        currentSpawnDealy = _settings.minSpawnDelay + 
            ((_settings.maxSpawnDelay - _settings.minSpawnDelay) * (1 - _settings.spawnDelayCurve.Evaluate(_difficulty)));
    }

    private void UpdateEnemiesSpeed()
    {
        _enemiesSpeed = _settings.minSpeed + 
            ((_settings.maxSpeed - _settings.minSpeed) * _settings.speedCurve.Evaluate(_difficulty));
    }

    private void RemoveEnemies()
    {
        foreach(var enemy in spawnedEnemies)
        {
            Destroy(enemy.gameObject);
        }
    }

    private IEnumerator RunSpawnProcess()
    {
        spawnedEnemies = new List<Enemy>();
        while (true)
        {
            yield return new WaitForSeconds(currentSpawnDealy);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = enemiesSpawnArea.RandomPointInBounds();
        spawnPosition.y = .5f; // here i should use half of height of object (depends on pivot)

        Enemy enemy = Instantiate(_settings.prefab, spawnPosition, Quaternion.identity)
            .GetComponent<Enemy>(); // component should be got from object pool (is this case it take more resources)

        enemy.SetupData(_settings.damage, _enemiesSpeed, _targetToFollow);

        spawnedEnemies.Add(enemy);
    }
}
