using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1;
    [HideInInspector] public ShotControl shotControl;

    public void SetDirection(Vector3 aimPoint)
    {
        transform.LookAt(aimPoint);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().curHp--;
        }

        shotControl.ReturnBulletToPool(this);
    }
}
