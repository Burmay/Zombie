using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInteractor interactor;
    ShootingInteractor shooting;
    Ray ray; RaycastHit hit; Vector3 lookPos; Vector3 lookDir;
    Rigidbody rigidbody;
    private float speed, ammo;
    private Vector3 input;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PlayerData playerData;
    public bool die;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        die = false;
        interactor = Game.GetInteractor<PlayerInteractor>();
        shooting = Game.GetInteractor<ShootingInteractor>();
        rigidbody = GetComponent<Rigidbody>();
        shooting.SetFirePoint(firePoint);
        playerData.GetData(this);
    }

    public void SetData(int speed, int ammo)
    {
        this.speed = speed;
        this.ammo = ammo;
    }

    private void Update()
    {
        if (!die)
        {
            GatherInput();
            Shoot();
            ShootTimer();

            Aiming();
            Move();
        }
    }

    private void ShootTimer()
    {
        if (shooting.shootTimer != 0)
        {
            if (shooting.shootTimer - Time.deltaTime > 0) { shooting.shootTimer -= Time.deltaTime; }
            else { shooting.shootTimer = 0; }
        }
    }

    private void Shoot() /// !!!
    {
        if (Input.GetMouseButton(0))
        {
            if (ammo > 0)
            {
                if (shooting.Shoot())
                {
                    SpendAmmo();
                }
            }
        }
    }

    public void SpendAmmo()
    {
        if (ammo > 0)
        {
            ammo--;
        }
    }


    private void GatherInput()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).ToIso();
    }

    private void Move()
    {
        characterController.Move(input * Time.deltaTime * speed);;
    }

    private void Aiming()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;
        transform.LookAt(transform.position + lookDir, Vector3.up);
    }

    public void Die()
    {
        interactor.GameOver();
        //GameObject.Destroy(gameObject);
    }
}
