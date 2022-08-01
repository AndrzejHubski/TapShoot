using System.Collections.Generic;
using UnityEngine;

public class ShotControl : MonoBehaviour
{
    private Camera mainCamera;
    private LayerMask layerMask = 1 << 8;
    public Transform spawnPoint;
    public Bullet bulletScript;
    public List<Bullet> bulletPool;
    private bool isStarted;

    private void Start()
    {
        mainCamera = Camera.main;

        //init pool of bullets
        bulletPool = new List<Bullet>();
        for(int i = 0; i < 10; i++)
        {
            var bulletTemp = Instantiate(bulletScript, spawnPoint);
            bulletTemp.shotControl = gameObject.GetComponent<ShotControl>();
            bulletPool.Add(bulletTemp);
            bulletTemp.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        Vector3 shotPoint;

        if (Input.GetMouseButtonDown(0))
        {
            if(isStarted == true)
            {
                //get point of shot
                RaycastHit hit;
                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, layerMask))
                {
                    shotPoint = hit.point;
                    var shot = GetBulletFromPool();
                    shot.SetDirection(shotPoint);
                }
            }
            else
            {
                isStarted = true;
            }
        }
    }

    private Bullet GetBulletFromPool()
    {
        if(bulletPool.Count > 0)
        {
            var bulletTemp = bulletPool[0];
            bulletPool.Remove(bulletTemp);
            bulletTemp.gameObject.SetActive(true);
            bulletTemp.transform.parent = null;
            bulletTemp.transform.position = spawnPoint.transform.position;
            return bulletTemp;
        }
        else
        {
            return Instantiate(bulletScript, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void ReturnBulletToPool(Bullet bulletTemp)
    {
        if (!bulletPool.Contains(bulletTemp))
        {
            bulletPool.Add(bulletTemp);
        }

        bulletTemp.gameObject.SetActive(false);
        bulletTemp.transform.parent = spawnPoint;
        bulletTemp.transform.position = spawnPoint.transform.position;
    }
}
