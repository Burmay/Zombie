using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    private EnemyInteractor interactor;
    private DamageInteractor damageInteractor;
    protected int hp { get; set; }
    protected NavMeshAgent navMeshAgent;

    protected virtual void Start()
    {
        interactor = Game.GetInteractor<EnemyInteractor>();
        damageInteractor = Game.GetInteractor<DamageInteractor>();
        enemyData.GetData(this);
    }

    public virtual void Init(int minHP, int maxHP, int minSpeed, int maxSpeed)
    {
        System.Random random = new System.Random();
        hp = random.Next(minHP, maxHP);
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = random.Next(minSpeed, maxSpeed);
    }

    public virtual void IncomingDamage(int damage)
    {
        if (hp - damage > 0)
        {
            hp -= damage;
        }
        else
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject, 0.05f);
        interactor.UpdateScore();
        interactor.ZoombieDied?.Invoke();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            damageInteractor.Bite(collision);
        }
    }

}
