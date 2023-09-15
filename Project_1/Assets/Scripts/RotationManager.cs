using UnityEngine;
public class RotationManager : MonoBehaviour
{
    private const float _angle = 5f;
    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.AngleAxis(_angle, Vector3.up);
        transform.rotation *= rotation;
    }
}