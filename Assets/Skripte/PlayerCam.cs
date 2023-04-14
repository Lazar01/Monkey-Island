using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform banaOrientation;
    public Transform gunOrinetation;

    float xRotation;
    float yRotation;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState == GameManager.GameState.running)
        {
            //if (Cursor.visible == true)
            //{
            //    Cursor.lockState = CursorLockMode.Locked;
            //    Cursor.visible = false;
            //}

            float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
            banaOrientation.rotation = Quaternion.Euler(0, yRotation - 90, 0);
        }
    }
}
