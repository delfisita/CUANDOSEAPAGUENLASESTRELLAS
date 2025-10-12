using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("A quién seguir")]
    public Transform target;

    [Header("Ajustes")]
    public float smoothSpeed = 0.125f;   // qué tan suave sigue
    public Vector3 offset;               // separación (x, y, z)

    void LateUpdate()
    {
        if (target == null) return;

        // Posición deseada: la del target + offset
        Vector3 desiredPosition = target.position + offset;

        // Interpolación suave entre la posición actual y la deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
