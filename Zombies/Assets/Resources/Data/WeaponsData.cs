using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponsData", menuName = "WeaponsData")]
public class WeaponsData : ScriptableObject
{
    public float rateOfShooting;
    public int damage;
    public int energy;
    public int speedBullet;
    public float improvementRate;
    public float explosionRadius;

    public void GetData(Object interactor)
    {
        if(interactor is ShootingInteractor)
        {
            ShootingInteractor shootingInteractor = (ShootingInteractor)interactor;
            shootingInteractor.SetData(damage, speedBullet, energy, rateOfShooting, improvementRate);
        }
        if(interactor is DamageInteractor)
        {
            DamageInteractor damageInteractor = (DamageInteractor)interactor;
            damageInteractor.SetData(explosionRadius);
        }
    }
}
