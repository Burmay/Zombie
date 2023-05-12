using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInteractor interactor;
    ShootingInteractor shooting;
    Ray ray; RaycastHit hit; Vector3 lookPos; Vector3 lookDir;
    Rigidbody rigidbody;
    [SerializeField] private float speed, turnSpeed;
    private Vector3 input;
    [SerializeField] private Transform firePoint;
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
            if (interactor.Ammo > 0)
            {
                shooting.Shoot();
            }
        }
    }

    private void GatherInput()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).ToIso();
    }

    private void SetDirectionMove()
    {
        if(input != Vector3.zero)
        {
            var relative = (transform.position + input.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, Time.deltaTime * turnSpeed);
        }
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
