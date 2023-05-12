using UnityEngine;

public class DamageInteractor : Interactor
{

    // Calculation of damage and associated effects

    GameObject recipient;
    PlayerInteractor playerInt;

    public override void OnStart()
    {
        playerInt = Game.GetInteractor<PlayerInteractor>();
    }

    // 
    public void Hit(Collision collision, int damage, int energy, Quaternion rotation)
    {
        if(collision.gameObject != null)
        {
            recipient = collision.gameObject;
            if (recipient.tag == "Enemy")
            {
                HitEnemy(collision, damage, energy, rotation);
            }
            else if (recipient.tag == "Player")
            {
                // Ricochet, not rallied
                HitPlayer(collision, damage);
            }
            else
            {
                return;
            }
        }
    }

    private void HitEnemy(Collision collision, int damage, int energy, Quaternion rotation)
    {

        Enemy enemy = recipient.GetComponent<Enemy>();
        enemy.IncomingDamage(damage);

        //ContactPoint contactPoint = collision.GetContact(0);
        //Vector3 backVector = contactPoint.normal;

        var backVector = rotation * Vector3.forward;

        collision.rigidbody.AddForce(backVector * energy);
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
