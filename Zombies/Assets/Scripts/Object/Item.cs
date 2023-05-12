using UnityEngine;

public class Item : MonoBehaviour
{
    ShootingInteractor shooting;

    private void Start()
    {
        shooting = Game.GetInteractor<ShootingInteractor>();
    }

    void FixedUpdate()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 2.5f, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shooting.UpgradeWithItem();
            GameObject.Destroy(gameObject);
        }
    }

}
