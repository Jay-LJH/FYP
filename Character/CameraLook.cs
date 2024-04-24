using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CameraLook : MonoBehaviour
{
    private float mouseSensitivity = 100.0f;
    private float rotationY = 0.0f;
    private bool gyroEnabled;
    private Gyroscope gyro;
    [SerializeField]
    public bool UseMouse = false;
    private  Vector3 rot,preEuler;
    float filterFactor = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        gyroEnabled = EnableGyro();
        Debug.Log("use mouse: " + UseMouse);
    }
    
    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            rot = gyro.attitude.eulerAngles;
            preEuler = rot;
            return true;
        }
        return false;
    }
    void Update()
    {
       if (!UseMouse)
        {
           Vector3 gyroEuler = gyro.attitude.eulerAngles;
           Vector3 filteredEulerAngles = Vector3.Lerp(preEuler, gyroEuler, filterFactor);
           transform.rotation = Quaternion.Euler(gyroEuler.y-rot.y, rot.x-gyroEuler.x, 0);
           Debug.Log(gyroEuler);
           transform.parent.rotation = Quaternion.Euler(0, rot.x-gyroEuler.x, 0);
           preEuler = gyroEuler;
        }
        else
        {
            MouseLook();
        }
    }
    void MouseLook(){
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotationY, 0, 0);
        transform.parent.Rotate(Vector3.up * mouseX);
    }
     private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}