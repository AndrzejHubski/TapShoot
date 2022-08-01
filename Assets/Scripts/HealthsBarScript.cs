using UnityEngine;

public class HealthsBarScript : MonoBehaviour
{
    private Transform mainCamera;

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(mainCamera.position);
    }
}
