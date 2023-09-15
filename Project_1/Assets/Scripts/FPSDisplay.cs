using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour
{
    private const int _maxValueDictionary = 1000;
    private Dictionary<int, string> _cachedStringValues = new();

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
        SeedValueCache();
        _fpsCounter = GetComponent<FPSCounter>();
    }

    private void Update()
    {
        _currentCounter.text = CheckingValue(_fpsCounter.CurrentFps);
        _averageCounter.text = CheckingValue(_fpsCounter.AverageFps);
        _fivePercentile.text = CheckingValue(_fpsCounter.FivePercentile);
        _onePercentile.text =  CheckingValue(_fpsCounter.OnePercentile);
    }

    public void StartDisplay()
    {
        currentFPS.SetActive(true);
        averageFPS.SetActive(true);
        fivePercentile.SetActive(true);
        onePercentile.SetActive(true);
    }

    private void SeedValueCache()
    {
        for (int i = 0; i < _maxValueDictionary; i++)
            _cachedStringValues[i] = i.ToString();
    }

    private string CheckingValue(int value)
    {
        var text = value switch
        {
            var x when x >= 0 && x < _maxValueDictionary => _cachedStringValues[x],
            var x when x >= _maxValueDictionary => $"> {_cachedStringValues}",
            //var x when x < 0 => "< 0",
            _ => "?"
        };
        return text;
    }
}