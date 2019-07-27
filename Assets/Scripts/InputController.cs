using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float Vertical;
    public float Horizontal;
    public bool Jumped;
    public Vector2 MouseInput;

    public bool Fire1;
    public bool AimingOn;
    public bool TestButton;

    public bool ActivatableItem;

    private void Update()
    {

        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        TestButton = Input.GetKeyDown(KeyCode.P);
        
        Fire1 = Input.GetButton("Fire1");
        AimingOn = Input.GetButton("Fire2");
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        Jumped = Input.GetKeyDown(KeyCode.Space);
        ActivatableItem = Input.GetKeyDown(KeyCode.R);



    }


}
