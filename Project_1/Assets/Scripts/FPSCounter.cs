using UnityEngine;
using System;
using System.Linq;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private int _frameRange = 60;
    private int[] _fpsBuffer;
    private int _fpsBufferIndex;
    private float _updateTimer;
    private float _updateFrequency = 0.1f;
    private float _fps;
    public int CurrentFps { get; private set; }
    public int AverageFps { get; private set; }
    public double FivePercentile { get; private set; }
    public double OnePercentile { get; private set; }

    private int CalculateFps()
    {
        return (int)(1f / Time.unscaledDeltaTime);
    }
    
    private int CalculateCurrentFps()
    {
        _updateTimer -= Time.deltaTime;
        if (_updateTimer <= 0f)
        {
            _fps = 1f / Time.unscaledDeltaTime;
            _updateTimer = _updateFrequency;
        }
        return (int)Math.Round(_fps);
    }

    public void Update()
    {
        if (_fpsBuffer == null || _frameRange != _fpsBuffer.Length)
            InitializeBuffer();

        UpdateBuffer();
        CurrentFps = CalculateCurrentFps();
        AverageFps = CalculateAverageFps();
        FivePercentile = CalculatePercentile(_fpsBuffer, 0.05);
        OnePercentile = CalculatePercentile(_fpsBuffer, 0.01);
    }

    private void InitializeBuffer()
    {
        if (_frameRange <= 0)
            _frameRange = 1;

        _fpsBuffer = new int[_frameRange];
        _fpsBufferIndex = 0;
    }

    private void UpdateBuffer()
    {
        _fpsBuffer[_fpsBufferIndex++] = CalculateFps();
        if (_fpsBufferIndex >= _frameRange)
            _fpsBufferIndex = 0;
    }

    private int CalculateAverageFps()
    {
        int sum = 0;
        for (int i = 0; i < _frameRange; i++)
            sum += _fpsBuffer[i];

        return sum / _frameRange;
    }

    private double CalculatePercentile(int[] data, double percentile)
    {
        var orderedFps = data.OrderBy(n => n).ToArray();
        var percentileIndex = (int)Math.Round(percentile * orderedFps.Length);
        return orderedFps[percentileIndex];
    }

    public void RestartCount()
    {
        Array.Clear(_fpsBuffer, 0, _fpsBuffer.Length);
    }
}