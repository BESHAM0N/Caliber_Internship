using UnityEngine;
using System;
using System.Linq;
public class FPSCounter : MonoBehaviour
{
    private const float _fivePercentile = 0.05f;
    private const float _onePercentile = 0.01f;
    private const int _bufferStepCapacity = 1000;
    private const float _CurrentRepeatRate = 0.12f;
    private const float _AverageRepeatRate = 0.5f;
    private const float _PercentileRepeatRate = 0.5f;
    
    private int[] _fpsBuffer = new int[_bufferStepCapacity];
    private int _fpsBufferIndex;
    private int _currentFpsBuffer;
    public int CurrentFps { get; private set; }
    public int AverageFps { get; private set; }
    public double FivePercentile { get; private set; }
    public double OnePercentile { get; private set; }
    
    public void Start()
    {
        InvokeRepeating("SetCurrentFps", 0f, _CurrentRepeatRate);
        InvokeRepeating("CalculateAverageFps", 0f, _AverageRepeatRate);
        InvokeRepeating("CalculatePercentile", 0f, _PercentileRepeatRate);
    }
    
    public void Update()
    {
        CalculateCurrentFps();
    }
    
    private void SetCurrentFps()
    {
        CurrentFps = _currentFpsBuffer;
    }
    
    private void CalculateCurrentFps()
    {
        var fps = (int)Math.Round(1f / Time.unscaledDeltaTime);
        _currentFpsBuffer = fps;
        _fpsBuffer[_fpsBufferIndex] = fps;
        _fpsBufferIndex++;
    
        if (_fpsBufferIndex >= _fpsBuffer.Length)
            Array.Resize(ref _fpsBuffer, _fpsBuffer.Length + _bufferStepCapacity);
    }
    
    private int CalculateAverageFps()
    {
        int sum = _fpsBuffer.Sum();
        if (sum > 0)
            AverageFps = sum / _fpsBufferIndex+1;
        return AverageFps;
    }
    
    private void CalculatePercentile()
    {
        var orderedFps = _fpsBuffer.Where(n => n > 0).OrderBy(n => n).ToArray();
        if (orderedFps.Any())
        {
            var onePercentileIndex = (int)Math.Round(_onePercentile * _fpsBufferIndex);
            var fivePercentileIndex = (int)Math.Round(_fivePercentile * _fpsBufferIndex);
            OnePercentile = orderedFps[onePercentileIndex];
            FivePercentile = orderedFps[fivePercentileIndex];
        }
    }
    
    public void RestartCount()
    {
        _fpsBuffer = new int[_bufferStepCapacity];
        _fpsBufferIndex = 0;
        AverageFps = 0;
        OnePercentile = 0;
        FivePercentile = 0;
    }
}
