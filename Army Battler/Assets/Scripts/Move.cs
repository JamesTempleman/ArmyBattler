using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float Speed = 1;
    public float FastSpeed = 9;
    public KeyCode EnableFastSpeedWithKey = KeyCode.LeftShift;
    public double MaxX = 3.74;
    public double MinX = -3.69;
    public double MaxY = 4.98;
    public double MinY = -5.18;

    //max x 3.74 max y 4.98 
    //min x -3.69 min y -5.18

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){

        var currentSpeed = Speed;

        if (Input.GetKey(EnableFastSpeedWithKey))
        {
            currentSpeed = FastSpeed;
        }

 
        if (Input.GetKey("d"))
        {
            //if our current X is > than the minimum range AND less than maximum range
            if (transform.position.x < MaxX)
            {
                transform.Translate(Vector3.right * currentSpeed * Time.deltaTime, Space.World);
            }
 
            else
            {
                Debug.Log("Transform.position.x is greater than " + MinX + " or is less than " + MaxX);
            }
        }
        if (Input.GetKey("a"))
        {
            //if our current X is > than the minimum range AND less than maximum range
            if (transform.position.x > MinX)
            {
                transform.Translate(Vector3.left * currentSpeed * Time.deltaTime, Space.World);
            }

            else
            {
                Debug.Log("Transform.position.x is Lesser than " + MinX + " or is greater than " + MaxX);
            }
        }

        if (Input.GetKey("w"))
        {
            //if our current X is > than the minimum range AND less than maximum range
            if (transform.position.y < MaxY)
            {
                transform.Translate(Vector3.up * currentSpeed * Time.deltaTime, Space.World);
            }

            else
            {
                Debug.Log("Transform.position.y is greater than " + MinY + " or is less than " + 12.6);
            }
        }
        if (Input.GetKey("s"))
        {
            //if our current X is > than the minimum range AND less than maximum range
            if (transform.position.y > MinY)
            {
                transform.Translate(Vector3.down * currentSpeed * Time.deltaTime, Space.World);
            }

            else
            {
                Debug.Log("Transform.position.y is Lesser than " + MinY + " or is greater than " + MaxY);
            }
        }


    }
}

/*
var movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
transform.Translate(movement* currentSpeed * Time.deltaTime);*/