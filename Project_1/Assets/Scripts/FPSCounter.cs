using UnityEngine;
using System;
using System.Linq;


public class FPSCounter : MonoBehaviour
{
    private const float _fivePercentile = 0.05f;
    private const float _onePercentile = 0.01f;
    private const int _bufferStepCapacity = 5000;
    
    private int[] _fpsBuffer = new int[_bufferStepCapacity];
    private int _fpsBufferIndex;
    private int _bufferCount;
    private int _currentFpsBuffer;
    public int CurrentFps { get; private set; }
    public int AverageFps { get; private set; }
    public double FivePercentile { get; private set; }
    public double OnePercentile { get; private set; }

    public void Start()
    {
        InvokeRepeating("SetCurrentFps", 0, 0.2f);
        InvokeRepeating("CalculateAverageFps", 0f, 0.5f);
        InvokeRepeating("CalculatePercentile", 0f, 1f);
    }
    
    public void Update()
    {
        _currentFpsBuffer = GetCurrentFps();
        InsertInFpsBuffer(_currentFpsBuffer);
    }
    
    private int GetCurrentFps()
    {
        return (int)(1f / Time.unscaledDeltaTime);
    }

    private void SetCurrentFps()
    {
        CurrentFps = _currentFpsBuffer;
    }

    private void InsertInFpsBuffer(int currentFps)
    {
        _fpsBuffer[_fpsBufferIndex++] = currentFps;
        _bufferCount = _fpsBufferIndex++;

        if (_fpsBufferIndex >= _bufferCount)
            Array.Resize(ref _fpsBuffer, _fpsBuffer.Length + _bufferStepCapacity);
    }

    private int CalculateAverageFps()
    {
        int sum = _fpsBuffer.Sum();
        if (sum > 0)
            AverageFps = sum / _bufferCount;
        return AverageFps;
    }

    private void CalculatePercentile()
    {
        var orderedFps = _fpsBuffer.Where(i => i > 0).OrderBy(n => n).ToArray();
        if (orderedFps.Any())
        {
            var fivePercentileIndex = (int)Math.Round(_fivePercentile * _bufferCount);
            var onePercentileIndex = (int)Math.Round(_onePercentile * _bufferCount);
            FivePercentile = orderedFps[fivePercentileIndex];
            OnePercentile = orderedFps[onePercentileIndex];
        }
    }

    public void RestartCount()
    {
        _fpsBuffer = new int[1000];
        AverageFps = 0;
        FivePercentile = 0;
        OnePercentile = 0;
        _bufferCount = 0;
        _fpsBufferIndex = 0;
    }
}