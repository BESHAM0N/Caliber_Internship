using UnityEngine;

public class RotationManager : MonoBehaviour
{
    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.AngleAxis(5, Vector3.up);
        transform.rotation *= rotation;
    }
}