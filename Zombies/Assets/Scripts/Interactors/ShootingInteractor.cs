using UnityEngine;

public class ShootingInteractor : Interactor
{
    // gun parameters and shooting

    [SerializeField] private Transform firePoint;
    private WeaponsData weaponData;
    private GameObject bulletPrefab;
    private const string BULLET_PATH = "Prefabs/Bullet";
    private const string DATA_PATH = "Data/WeaponsData";
    private PlayerInteractor interactor;
    private SoundInteractor soundInteractor;
    public float shootTimer;

    private int damage, speed, energy;
    private float rateOfShooting, improvementRate;
    

    public override void OnCreate()
    {
        interactor = Game.GetInteractor<PlayerInteractor>();
        soundInteractor = Game.GetInteractor<SoundInteractor>();
        bulletPrefab = Resources.Load<GameObject>(BULLET_PATH);
        weaponData = Resources.Load<WeaponsData>(DATA_PATH);
        weaponData.GetData(this);
    }

    public void SetData(int damage, int speed, int energy, float rate, float improvementRate)
    {
        this.damage = damage;
        this.speed = speed;
        this.energy = energy;
        this.rateOfShooting = rate;
        this.improvementRate = improvementRate;
    }
    

    public void SetFirePoint(Transform firePoint)
    {
        this.firePoint = firePoint;
    }

    public bool Shoot()
    {
        if (shootTimer == 0)
        {
            GameObject instance = Instantiate(bulletPrefab, firePoint.position, interactor.GetTransformInstance().rotation);
            Vector3 dirShooting = interactor.GetTransformInstance().rotation * Vector3.forward;
            var bullet = instance.GetComponent<Bullet>();
            bullet.Initialize(damage, speed, energy, dirShooting);
            soundInteractor.PlayShot();
            shootTimer = rateOfShooting;
            return true;
        }
        else return false;
    }

    

    public void UpgradePerWave() // not used
    {
        SpeedRateUpgrade(improvementRate);
    }

    public void UpgradeWithItem()
    {
        SpeedRateUpgrade(improvementRate);
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