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
        AudioSource.PlayClipAtPoint(bulletData.bulletSound, transform.position);
        
        Transform explosioneffect = BulletPooling.Instance.GetVfxHit();
        explosioneffect.position = transform.position;
        explosioneffect.gameObject.SetActive(true);

        BulletPooling.Instance.ReturnBullet(transform);

        BulletPooling.Instance.StartCoroutine(ReturnVfxToPool(explosioneffect, 1f));
    }

    public static IEnumerator ReturnVfxToPool(Transform vfx, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (vfx != null)
        {
            BulletPooling.Instance.ReturnVfxHit(vfx);
        }
    }
}
