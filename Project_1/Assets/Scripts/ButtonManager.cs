using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button startButton;
    public Button restartButton;
    public Button increaseButton;
    public Button reduceButton;

    private FPSDisplay _fpsDisplay;
    private FPSCounter _fpsCounter;
    private SpawnManager _spawnManager;

    private void Start()
    {
        _fpsDisplay = gameObject.GetComponentInChildren<FPSDisplay>();
        _fpsCounter = gameObject.GetComponentInChildren<FPSCounter>();
        _spawnManager = gameObject.GetComponentInChildren<SpawnManager>();
        
        startButton.onClick.AddListener(StartDisplay);
        restartButton.onClick.AddListener(RestartCount);
        increaseButton.onClick.AddListener(StartSpawn);
        reduceButton.onClick.AddListener(DeleteAllCookies);
    }

    private void StartDisplay()
    {
        _fpsDisplay.StartDisplay();
    }

    private void RestartCount()
    {
        _fpsCounter.RestartCount();
    }

    private void StartSpawn()
    {
        _spawnManager.StartSpawn();
    }

    private void DeleteAllCookies()
    {
        _spawnManager.DeleteHundredCookies();
    }
}