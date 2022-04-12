using UnityEngine;
public class HealthCanvasFacing : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(45f, 45f, 0);
    }
}
