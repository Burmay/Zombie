using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private EnemyInteractor interactor;
    private DamageInteractor damageInteractor;
    protected int hp { get; set; }
    NavMeshAgent navMeshAgent;

    protected virtual void Start()
    {
        interactor = Game.GetInteractor<EnemyInteractor>();
        damageInteractor = Game.GetInteractor<DamageInteractor>();
    }

    public virtual void Init(int minHP, int maxHP)
    {
        System.Random random = new System.Random();
        hp = random.Next(minHP, maxHP);
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = random.Next(7,12);
    }

    public virtual void IncomingDamage(int damage)
    {
        if (hp - damage > 0)
        {
            hp -= damage;
        }
        else
        {
            interactor.CheckSurvivor();
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject, 0.05f);
        interactor.UpdateScore();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            damageInteractor.Bite(collision);
        }
    }

}
