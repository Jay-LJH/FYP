using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public Transform target;  // 玩家对象的Transform组件
    public float rotationSpeed = 5f;
    public float followSpeed = 5f;


    public float distance = 5f; //摄像机到目标的距离
    public float minDistance = 2f; // 最小距离
    public float maxDistance = 10f; // 最大距离
    public float zoomSpeed = 2f; // 缩放速度

    private void Start()
    {

    }
    void Update()
    {
        // 通过按住右键拖动来旋转摄像机
        if (Input.GetMouseButton(1))
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.RotateAround(target.position, Vector3.up, horizontalRotation);
            float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
            transform.RotateAround(target.position, transform.right, -verticalRotation);
        }
        // 使摄像机朝向目标
        transform.LookAt(target.position);


        // 使用滚轮缩放摄像机距离
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);


        // 调整摄像机位置
        Vector3 offset = -transform.forward * distance;
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * followSpeed);
    }
}