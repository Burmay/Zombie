using System;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private Transform targetTransform;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float camSpeed;

    private void Start()
    {
        Game.OnGameInitializedEvent += FindTarget;
    }

    private void FindTarget(object sender, EventArgs e)
    {
        PlayerInteractor interactor = Game.GetInteractor<PlayerInteractor>();
        targetTransform = interactor.GetTransformInstance();
    }
    void LateUpdate()
    {
        Move();
        //Rotation();
    }

    private void Move()
    {
        if (targetTransform == null) return;
        Vector3 nextPos = Vector3.Lerp(transform.position, targetTransform.position + offset, Time.deltaTime * camSpeed);
        transform.position = nextPos;
    }

    private void Rotation()
    {
        //Vector3 v = new Vector3(transform.localEulerAngles.x + 30, targetTransform.localEulerAngles.y + 45, transform.localEulerAngles.z);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(45, v.y, v.z), offsetRotation * Time.deltaTime);
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + 30, targetTransform.localEulerAngles.y + 45, transform.localEulerAngles.z);

        //if (Input.GetKey(KeyCode.Q))
        //{
        //
        //}
        //if (Input.GetKey(KeyCode.E))
        //{
        //
        //}
    }


}
