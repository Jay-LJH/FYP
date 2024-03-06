using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public Transform target;  // ��Ҷ����Transform���
    public float rotationSpeed = 5f;
    public float followSpeed = 5f;


    public float distance = 5f; //�������Ŀ��ľ���
    public float minDistance = 2f; // ��С����
    public float maxDistance = 10f; // ������
    public float zoomSpeed = 2f; // �����ٶ�

    private void Start()
    {

    }
    void Update()
    {
        // ͨ����ס�Ҽ��϶�����ת�����
        if (Input.GetMouseButton(1))
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.RotateAround(target.position, Vector3.up, horizontalRotation);
            float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
            transform.RotateAround(target.position, transform.right, -verticalRotation);
        }
        // ʹ���������Ŀ��
        transform.LookAt(target.position);


        // ʹ�ù����������������
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);


        // ���������λ��
        Vector3 offset = -transform.forward * distance;
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * followSpeed);
    }
}