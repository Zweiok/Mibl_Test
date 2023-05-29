using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinesFactory : MonoBehaviour, IDifficultyInfluenced
{
    [SerializeField] private BoxCollider minesSpawnArea;

    private float currentSpawnDealy;

    private MinesSettings _settings;

    private float _difficulty;

    private List<Mine> spawnedMines;
    public void UpdateDifficulty(float difficulty)
    {
        _difficulty = difficulty;
        UpdateSpawnDelay();
    }

    public void RunFactory(MinesSettings settings, float initialDifficulty)
    {
        // Object pool should be connected here (if spend more time)
        _settings = settings;
        UpdateDifficulty(initialDifficulty);
        StartCoroutine(RunSpawnProcess());
    }

    public void StopFactory()
    {
        StopAllCoroutines();
        RemoveAllMines();
    }

    private void UpdateSpawnDelay()
    {
        currentSpawnDealy = _settings.minSpawnDelay + 
            ((_settings.maxSpawnDelay - _settings.minSpawnDelay) * (1 - _settings.spawnDelayCurve.Evaluate(_difficulty)));
    }

    private IEnumerator RunSpawnProcess()
    {
        spawnedMines = new List<Mine>();

        while (true) // here should be some stop conditions
        {
            yield return new WaitForSeconds(currentSpawnDealy);
            SpawnMine();
        }
    }

    private void SpawnMine()
    {
        Vector3 spawnPosition = minesSpawnArea.RandomPointInBounds();
        spawnPosition.y = .5f; // here i should use half of height of object (depends on pivot)

        Mine mine = Instantiate(_settings.prefab, spawnPosition, Quaternion.identity)
            .GetComponent<Mine>(); // component should be got from object pool (is this case it take more resources)


        mine.SetupData(_settings.timeToDetonate, _settings.damage, _settings.detonateRange);

        spawnedMines.Add(mine);

        mine.onMineDetonated += RemoveMine;
    }

    private void RemoveMine(Mine mine)
    {
        spawnedMines.Remove(mine);
    }

    private void RemoveAllMines()
    {
        foreach(var mine in spawnedMines)
        {
            Destroy(mine.gameObject);
        }
    }
}
