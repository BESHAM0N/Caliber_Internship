using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private Text _currentCounter;
    [SerializeField] private Text _averageCounter;
    [SerializeField] private Text _percentile5;
    [SerializeField] private Text _percentile1;
    public GameObject currentFPS;
    public GameObject averageFPS;
    public GameObject percentile5;
    public GameObject percentile1;
    

    private FPSCounter _fpsCounter;

    private void Awake()
    {
        _fpsCounter = GetComponent<FPSCounter>();
    }

    private void Update()
    {
        _currentCounter.text = "current FPS " + _fpsCounter.CurrentFps;
        _averageCounter.text = "average FPS " + _fpsCounter.AverageFps;
        _percentile5.text = "5 percent FPS " + _fpsCounter.FivePercentile;
        _percentile1.text = "1 percent FPS " + _fpsCounter.OnePercentile;

    }
    public void StartDisplay()
    {
        currentFPS.SetActive(true);
        averageFPS.SetActive(true);
        percentile5.SetActive(true);
        percentile1.SetActive(true);
    }

    
}