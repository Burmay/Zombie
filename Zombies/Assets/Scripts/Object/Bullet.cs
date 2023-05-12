using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    private DamageInteractor damageInteractor;
    private int damage, speed, energy;
    Vector3 direction;

    public void Initialize(int damage, int speed, int energy, Vector3 direction)
    {
        this.damage = damage;
        this.speed = speed;
        this.energy = energy;
        this.direction = direction;
    }

    private void Update()
    {
        Flight();
    }

    private void Flight()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        damageInteractor = Game.GetInteractor<DamageInteractor>();
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
        damageInteractor.Hit(collision, damage, energy, transform.rotation);
    }
}