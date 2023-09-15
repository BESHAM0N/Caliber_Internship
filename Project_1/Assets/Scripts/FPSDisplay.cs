using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private Text _currentCounter;
    [SerializeField] private Text _averageCounter;
    [SerializeField] private Text _fivePercentile;
    [SerializeField] private Text _onePercentile;
    
    public GameObject currentFPS;
    public GameObject averageFPS;
    public GameObject fivePercentile;
    public GameObject onePercentile;
    
    private FPSCounter _fpsCounter;

    private void Awake()
    {
        _fpsCounter = GetComponent<FPSCounter>();
    }

    private void Update()
    {
        _currentCounter.text = "current FPS " + _fpsCounter.CurrentFps;
        _averageCounter.text = "average FPS " + _fpsCounter.AverageFps;
        _fivePercentile.text = "5 percent FPS " + _fpsCounter.FivePercentile;
        _onePercentile.text = "1 percent FPS " + _fpsCounter.OnePercentile;
    }

    public void StartDisplay()
    {
        currentFPS.SetActive(true);
        averageFPS.SetActive(true);
        fivePercentile.SetActive(true);
        onePercentile.SetActive(true);
    }
}