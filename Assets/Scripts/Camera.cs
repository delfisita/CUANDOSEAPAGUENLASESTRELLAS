using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("A qui�n seguir")]
    public Transform target;

    [Header("Ajustes")]
    public float smoothSpeed = 0.125f;   // qu� tan suave sigue
    public Vector3 offset;               // separaci�n (x, y, z)

    void LateUpdate()
    {
        if (target == null) return;

        // Posici�n deseada: la del target + offset
        Vector3 desiredPosition = target.position + offset;

        // Interpolaci�n suave entre la posici�n actual y la deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
