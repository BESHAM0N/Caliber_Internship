using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private Text _currentCounter;
    [SerializeField] private Text _averageCounter;
    public GameObject currentFPS;
    public GameObject averageFPS;
    

    private FPSCounter _fpsCounter;

    private void Awake()
    {
        _fpsCounter = GetComponent<FPSCounter>();
    }

    private void Update()
    {
        _currentCounter.text = _fpsCounter.CurrentFps.ToString();
        _averageCounter.text = _fpsCounter.AverageFps.ToString();
    }
    public void StartDisplay()
    {
        currentFPS.SetActive(true);
        averageFPS.SetActive(true);
    }

    
}