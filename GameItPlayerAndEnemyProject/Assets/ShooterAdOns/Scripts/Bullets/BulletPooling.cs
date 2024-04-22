using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    public static BulletPooling Instance { get; private set; }

    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform Hitvfx;

    private Queue<Transform> bulletPool = new Queue<Transform>();
    private Queue<Transform> HitvfxPool = new Queue<Transform>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < 50; i++)
        {
           //instantiate all bullets in the childern of the bullet pool object
           Transform bullet = Instantiate(pfBulletProjectile, Vector3.zero, Quaternion.identity, this.transform);
           bullet.gameObject.SetActive(false);
           bulletPool.Enqueue(bullet);

            Transform vfxGreen = Instantiate(Hitvfx, Vector3.zero, Quaternion.identity,this.transform);
            vfxGreen.gameObject.SetActive(false);
            HitvfxPool.Enqueue(vfxGreen);
            
        }
    }

    public Transform GetBullet(Vector3 spawnPosition)
    {
        if (bulletPool.Count == 0)
        {
            Transform bullet = Instantiate(pfBulletProjectile, spawnPosition, Quaternion.identity, this.transform);
            bullet.gameObject.SetActive(false);
            bulletPool.Enqueue(bullet);
        }

        Transform bulletFromPool = bulletPool.Dequeue();
        bulletFromPool.position = spawnPosition;
        bulletFromPool.gameObject.SetActive(true);
        return bulletFromPool;
    }

    public void ReturnBullet(Transform bullet)
    {
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    public Transform GetVfxHit()
    {
        if (HitvfxPool.Count == 0)
        {
            Transform vfxGreen = Instantiate(Hitvfx, Vector3.zero, Quaternion.identity,this.transform);
            vfxGreen.gameObject.SetActive(false);
            HitvfxPool.Enqueue(vfxGreen);
        }

        Transform vfxGreenFromPool = HitvfxPool.Dequeue();
        vfxGreenFromPool.gameObject.SetActive(true);
        return vfxGreenFromPool;
    }

    public void ReturnVfxHit(Transform vfx)
    {
        vfx.gameObject.SetActive(false);
        HitvfxPool.Enqueue(vfx);
    }
    
}
