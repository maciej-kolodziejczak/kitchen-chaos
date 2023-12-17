using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Mode mode = Mode.Normal;
    
    private enum Mode
    {
        Normal,
        Inverted
    }
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        var forward = _camera.transform.forward;
        transform.forward = mode switch
        {
            Mode.Normal =>
                forward,
            Mode.Inverted =>
                -forward,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}