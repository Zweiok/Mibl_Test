using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [ReadOnly] [SerializeField] private float maxDifficultyValue = 1;
    [ReadOnly] [SerializeField] private float difficultyUpdateTime = 1;
    [SerializeField] private GameSettings settings;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private MinesFactory minesFactory;
    [SerializeField] private EnemiesFactory enemiesFactory;
    private List<IDifficultyInfluenced> difficultyInfluenced;
    private PlayerController _currentPlayer;
    private float startGameTime;
    private float currentDifficultyValue = 0;
    private Coroutine difficultyScenario;
    private void Awake()
    {
        SubscribeEvents();
        uiManager.SetScreen(UIScreenType.StartScreen);
        difficultyInfluenced = new List<IDifficultyInfluenced>();
        AddDifficultyInfluencedSystems();
    }

    private void AddDifficultyInfluencedSystems()
    {
        difficultyInfluenced.Add(minesFactory);
        difficultyInfluenced.Add(enemiesFactory);
    }

    private void SubscribeEvents()
    {
        uiManager.SubscribeStartAction(RunGame);
        uiManager.SubscribeRestartAction(RestartGame);
    }

    #region game management
    private void RunGame()
    {
        startGameTime = Time.timeSinceLevelLoad; // change if game have pause
        InitializePlayer();
        cameraController.SetupFollowData(_currentPlayer.transform, _currentPlayer.transform);
        cameraController.SetCamera(CameraType.FollowPlayerCamera);
        uiManager.SetScreen(UIScreenType.GamePlayScreen);
        InitFactories();
        difficultyScenario = StartCoroutine(DifficultyGrowScenario());
    }

    private void StopGame()
    {
        StopCoroutine(difficultyScenario);
        uiManager.SetScreen(UIScreenType.GameOverScreen);
        cameraController.SetCamera(CameraType.UICamera);
        uiManager.UpdateGameTimeValue(Time.timeSinceLevelLoad - startGameTime);
        ResetFactories();
    }

    private void RestartGame()
    {
        RunGame();
    }
    #endregion

    private void InitFactories()
    {
        minesFactory.RunFactory(settings.minesSettings, currentDifficultyValue);
        enemiesFactory.RunFactory(settings.enemiesSettings, currentDifficultyValue, _currentPlayer.transform);
    }

    private void ResetFactories()
    {
        minesFactory.StopFactory();
        enemiesFactory.StopFactory();
    }

    private void InitializePlayer()
    {
        _currentPlayer = Instantiate(settings.playerSettings.prefab, playerSpawnPoint.position, Quaternion.identity).GetComponent<PlayerController>();
        _currentPlayer.SetupSettings(settings.playerSettings);
        _currentPlayer.SetJoysticks(uiManager.CurrentMovementJoystick, uiManager.CurrentRotationJoystick);
        _currentPlayer.onDeath += StopGame;
    }

    private IEnumerator DifficultyGrowScenario()
    {
        float timeCounter = 0;
        while (currentDifficultyValue < maxDifficultyValue)
        {
            yield return new WaitForSeconds(difficultyUpdateTime);
            timeCounter += difficultyUpdateTime;
            currentDifficultyValue = settings.gameProcessSettings.difficultyCurve.Evaluate(timeCounter / settings.gameProcessSettings.timeToMaxDifficulty);
            UpdateDifficultyForSystems();
        }
    }

    private void UpdateDifficultyForSystems()
    {
        foreach(var system in difficultyInfluenced)
        {
            system.UpdateDifficulty(currentDifficultyValue);
        }
    }
}
