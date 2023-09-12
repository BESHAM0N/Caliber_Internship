using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private Text _currentCounter;
    [SerializeField] private Text _averageCounter;
    

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
}