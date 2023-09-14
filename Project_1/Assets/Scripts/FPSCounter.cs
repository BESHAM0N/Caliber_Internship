using UnityEngine;
using System;
using System.Linq;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private int _frameRange = 60;
    private int[] _fpsBuffer;
    private int _fpsBufferIndex;
    public int CurrentFps { get; private set; }
    public int AverageFps { get; private set; }

    public double FivePercentile { get; private set; }

    public double OnePercentile { get; private set; }

    private static int CalculateFps()
    {
        return (int)(1f / Time.unscaledDeltaTime);
    }

    public void Update()
    {
        if (_fpsBuffer == null || _frameRange != _fpsBuffer.Length)
            InitializeBuffer();

        UpdateBuffer();
        CurrentFps = CalculateFps();
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