using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    private Camera myCam;

    private void Awake()
    {
        myCam = this.GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            var newpos = this.transform.position;
            newpos.x -= speed * Time.deltaTime;
            this.transform.position = newpos;
        }
        if (Input.GetKey(KeyCode.S))
        {
            var newpos = this.transform.position;
            newpos.x += speed * Time.deltaTime;
            this.transform.position = newpos;
        }

        if (Input.GetKey(KeyCode.A))
        {
            var newpos = this.transform.position;
            newpos.z -= speed * Time.deltaTime;
            this.transform.position = newpos;
        }
        if (Input.GetKey(KeyCode.D))
        {
            var newpos = this.transform.position;
            newpos.z += speed * Time.deltaTime;
            this.transform.position = newpos;
        }

        myCam.orthographicSize += Input.mouseScrollDelta.y;

        myCam.orthographicSize = Mathf.Clamp(myCam.orthographicSize, 1, 16);
    }
}
