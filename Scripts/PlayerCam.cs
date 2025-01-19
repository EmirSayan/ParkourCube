using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] float sensX;
    [SerializeField] float sensY;

    public Transform oriantation;
    private PlayerMovment playerMovment;
    

    float xRotation;
    float yRotation;
    
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        xRotation = transform.rotation.eulerAngles.x;
        yRotation = transform.rotation.eulerAngles.y;
        playerMovment = GetComponent<PlayerMovment>();

    }

    
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        CameraMove();
    }

    public void CameraMove()
    {
        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        oriantation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
