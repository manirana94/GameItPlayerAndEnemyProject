using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Scriptable Object to store bullet data
/// </summary>
[CreateAssetMenu(fileName = "BulletData", menuName = "Bullet Data", order = 1)]
public class BulletData : ScriptableObject
{
    public float speed;
    public float damage;
    public float lifeTime;
    public AudioClip bulletSound;
}
