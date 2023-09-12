using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private int _frameRange = 60;
    private int[] _fpsBuffer;
    private int _fpsBufferIndex;
    public int CurrentFps { get; private set; } 
    public int AverageFps { get; private set; } 

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
        
        return  sum / _frameRange;
    }
}