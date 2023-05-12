using UnityEngine;

public class Aim : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 mousePos;

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        transform .position = mousePos;
    }
}
