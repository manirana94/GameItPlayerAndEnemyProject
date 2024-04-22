using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Subclass of Bullet to override base call methods to provide specific behavior
/// </summary>
public class ExplosiveBullet : Bullet
{
    public GameObject explosionPrefab;

    public override void OnHit()
    {
        // Play sound when bullet hits
        AudioSource.PlayClipAtPoint(bulletData.bulletSound, transform.position);
        
        // Get explosion effect from pool
        Transform explosioneffect = BulletPooling.Instance.GetVfxHit();
        explosioneffect.position = transform.position;
        explosioneffect.gameObject.SetActive(true);

        // Return bullet to pool
        BulletPooling.Instance.ReturnBullet(transform);

        // Start a coroutine to return the explosion effect to the pool after a delay
        BulletPooling.Instance.StartCoroutine(ReturnVfxToPool(explosioneffect, 1f));
    }

    public static IEnumerator ReturnVfxToPool(Transform vfx, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (vfx != null)
        {
            // Return the vfx to the pool
            BulletPooling.Instance.ReturnVfxHit(vfx);
        }
    }
}
