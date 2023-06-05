using UnityEngine;

public class MegaEnemy : Enemy
{
    private SoundInteractor soundInteractor;

    protected override void Start()
    {
        base.Start();
        soundInteractor = Game.GetInteractor<SoundInteractor>();
    }

    public override void Init(int minXP, int maxXP, int minSpeed, int maxSpeed)
    {
        base.Init(minXP, maxXP, minSpeed, maxSpeed);
    }

    public override void IncomingDamage(int damage)
    {
        base.IncomingDamage(damage);
    }

    protected override void Die()
    {
        soundInteractor.PlayZombieDied();
        base.Die();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
