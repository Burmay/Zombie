using UnityEngine;

public class ShootingInteractor : Interactor
{
    // gun parameters and shooting

    [SerializeField] private Transform firePoint;
    private GameObject bulletPrefab;
    private const string BULLET_PATH = "Prefabs/Bullet";
    private PlayerInteractor interactor;
    private SoundInteractor soundInteractor;
    public float shootTimer;

    private int damage, speed, energy;
    private float rateOfShooting;

    public override void OnCreate()
    {
        interactor = Game.GetInteractor<PlayerInteractor>();
        soundInteractor = Game.GetInteractor<SoundInteractor>();
        bulletPrefab = Resources.Load<GameObject>(BULLET_PATH);
        rateOfShooting = 0.47f;
        damage = 50;
        speed = 50;
        energy = 10000;
        shootTimer = 0;
    }

    public void SetFirePoint(Transform firePoint)
    {
        this.firePoint = firePoint;
    }

    public void Shoot()
    {
        if (shootTimer == 0)
        {
            interactor.SpendAmmo();
            GameObject instance = Instantiate(bulletPrefab, firePoint.position, interactor.GetTransformInstance().rotation);
            Vector3 dirShooting = interactor.GetTransformInstance().rotation * Vector3.forward;
            var bullet = instance.GetComponent<Bullet>();
            bullet.Initialize(damage, speed, energy, dirShooting);
            soundInteractor.PlayShot();
            shootTimer = rateOfShooting;
        }
    }

    public void UpgradePerWave()
    {
        SpeedRateUpgrade(0.0666f);
    }

    public void UpgradeWithItem()
    {
        SpeedRateUpgrade(0.0666f);
        soundInteractor.PlayPickUp();
    }

    private void SpeedRateUpgrade(float value)
    {
        rateOfShooting -= value;
    }

    private void DamageUpgrade(float value)
    {

    }


}