using UnityEngine;
using System;
using System.Linq;
public class FPSCounter : MonoBehaviour
{
    private const float _fivePercentile = 0.05f;
    private const float _onePercentile = 0.01f;
    private const int _bufferStepCapacity = 1000;
    private const float _currentRepeatRate = 0.12f;
    private const float _averageRepeatRate = 0.5f;
    private const float _percentileRepeatRate = 1f;
    private const float _invocationTime = 0f;

    private int[] _fpsBuffer = new int[_bufferStepCapacity];
    private int _fpsBufferIndex;
    private int _lastFps;
    public int CurrentFps { get; private set; }
    public int AverageFps { get; private set; }
    public double FivePercentile { get; private set; }
    public double OnePercentile { get; private set; }

    public void Start()
    {
        InvokeRepeating("SetCurrentFps", _invocationTime, _currentRepeatRate);
        InvokeRepeating("CalculateAverageFps", _invocationTime, _averageRepeatRate);
        InvokeRepeating("CalculatePercentile", _invocationTime, _percentileRepeatRate);
    }

    public void Update()
    {
        CalculateCurrentFps();
    }

    private void SetCurrentFps()
    {
        CurrentFps = _lastFps;
    }

    private void CalculateCurrentFps()
    {
        var fps = (int)Math.Round(1f / Time.unscaledDeltaTime);
        _lastFps = fps;
        _fpsBuffer[_fpsBufferIndex] = fps;
        _fpsBufferIndex++;

        if (_fpsBufferIndex >= _fpsBuffer.Length)
            Array.Resize(ref _fpsBuffer, _fpsBuffer.Length + _bufferStepCapacity);
    }

    private int CalculateAverageFps()
    {
        int sum = _fpsBuffer.Sum();
        if (sum > 0)
            AverageFps = sum / _fpsBufferIndex + 1;
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
    
    //I think that this method has a place to be,
    //since it can show a clearer frequency of instantaneous fps drops.
    // private int SumPercentile(int index, int[] array)
    // {
    //     int sumPercentile = 0;
    //     for (int i = 0; i <= index; i++)
    //     {
    //         sumPercentile += array[i];
    //     }
    //
    //     return sumPercentile / (index + 1);
    // }

    public void RestartCount()
    {
        _fpsBuffer = new int[_bufferStepCapacity];
        _fpsBufferIndex = 0;
        AverageFps = 0;
        OnePercentile = 0;
        FivePercentile = 0;
    }
}