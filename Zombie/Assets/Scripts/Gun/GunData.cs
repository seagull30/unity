using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Gundata", fileName = "Gun Data")]

public class GunData : ScriptableObject
{
    public AudioClip ShotClip;
    public AudioClip ReloadClip;

    public float Damage = 25f;

    public int InitialAmmoCount = 100;
    public int MagazineCapacity = 25;

    public float FireCooltime = 0.12f;
    public float ReloadTime = 1.8f;
}
