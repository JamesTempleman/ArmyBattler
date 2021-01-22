using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    public Toggle ZoomToggle;
    public float Zoom = 10;
    public float ZoomedOut = 15;
    public float Speed = 1;
    public float FastSpeed = 9;
    public KeyCode EnableFastSpeedWithKey = KeyCode.LeftShift;

    void Update(){

        var currentSpeed = Speed;
        var currentZoom = Zoom;

        

        if (Input.GetKey(EnableFastSpeedWithKey))
        {
            currentSpeed = FastSpeed;
        }

        if (ZoomToggle.isOn)
        {
            currentZoom = ZoomedOut;
        }

        Camera.main.orthographicSize = currentZoom;

        if (Input.GetKey("d"))
        {

                transform.Translate(Vector3.right * currentSpeed * Time.deltaTime, Space.World);


        }
        if (Input.GetKey("a"))
        {

                transform.Translate(Vector3.left * currentSpeed * Time.deltaTime, Space.World);

        }

        if (Input.GetKey("w"))
        {

                transform.Translate(Vector3.up * currentSpeed * Time.deltaTime, Space.World);

        }
        if (Input.GetKey("s"))
        {

                transform.Translate(Vector3.down * currentSpeed * Time.deltaTime, Space.World);
        }


    }
}

