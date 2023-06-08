using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DamageInteractor : Interactor
{

    // Calculation of damage and associated effects

    GameObject recipient;
    PlayerInteractor playerInt;

    private float explosionRadius;
    private WeaponsData weaponData;
    private const string DATA_PATH = "Data/WeaponsData";

    public override void OnStart()
    {
        playerInt = Game.GetInteractor<PlayerInteractor>();
        weaponData = Resources.Load<WeaponsData>(DATA_PATH);
        weaponData.GetData(this);
    }

    public void SetData(float expR)
    {
        explosionRadius = expR;
    }

    // 
    public void Hit(Collision collision, int damage, int energy, Quaternion rotation)
    {
        if(collision.gameObject != null)
        {
            if(explosionRadius != 0) { Explosion(collision.transform, collision, damage, energy, rotation); }

            recipient = collision.gameObject;
            if (recipient.tag == "Enemy")
            {
                HitEnemy(collision, damage, energy, rotation);
            }
            else if (recipient.tag == "Player")
            {
                // Ricochet, not rallised
                HitPlayer(collision, damage);
            }
            else
            {
                return;
            }
        }
    }

    // Explosion Effect not realised! 
    private void Explosion(Transform bulletTransform, Collision collision, int damage, int energy, Quaternion rotation)
    {
        Collider[] colliders = Physics.OverlapSphere(bulletTransform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                 ExpHitEnemy(collider, damage, energy, rotation);
            }
        }
    }

    private void HitEnemy(Collision collision, int damage, int energy, Quaternion rotation)
    {

        Enemy enemy = recipient.GetComponent<Enemy>();
        enemy.IncomingDamage(damage);

        var backVector = rotation * Vector3.forward;

        collision.rigidbody.AddForce(backVector * energy);
    }

    private void ExpHitEnemy(Collider collider, int damage, int energy, Quaternion rotation)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        enemy.IncomingDamage(damage);

        var backVector = rotation * Vector3.forward;
    }


    private void HitPlayer(Collision collision, int damage)
    {
        Debug.Log("Not realised");
        PlayerInteractor playerInteractor = Game.GetInteractor<PlayerInteractor>();
    }

    public void Bite(Collision collision)
    {
        if(playerInt.GetPlayerInstance().die == false)
        {
            playerInt.GetPlayerInstance().Die();
        }
    }
}
