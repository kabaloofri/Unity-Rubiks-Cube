using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    //rotation
    List<GameObject> activeSide;
    Vector3 loacalForward;
    Vector3 mouseRef;
    bool dragging = false;

    //auto rotation
    bool autoRotating = false;
    Quaternion targetQuaternion;
    float speed = 300f;

    float sensitivity = 0.2f;
    Vector3 rotation;

    ReadCube readCube;
    CubeState cubeState;

    void Start()
    {
        readCube = FindFirstObjectByType<ReadCube>();
        cubeState = FindFirstObjectByType<CubeState>();
    }

    void Update()
    {
        if (dragging)
        {
            SpinSide(activeSide);
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                RotateToRightAngle();
            }
        }
        if (autoRotating)
        {
            AutoRotate();
        }
    }
    public void Rotate(List<GameObject> side)
    {
        activeSide = side;
        mouseRef = Input.mousePosition;
        dragging = true;

        loacalForward = Vector3.zero - side[4].transform.parent.localPosition;

        readCube.ReadState();
    }

    void SpinSide(List<GameObject> side)
    {
        rotation = Vector3.zero;

        Vector3 mouseOffset = Input.mousePosition - mouseRef;

        //expr
        Vector2 PivotScreenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, activeSide[4].transform.position);
        Vector2 pivotOffset = new Vector2(Input.mousePosition.x - PivotScreenPoint.x, Input.mousePosition.y - PivotScreenPoint.y);
        pivotOffset.Normalize();

        float Xdir = Vector2.Dot(Vector2.right, pivotOffset); 
        float Ydir = -Vector2.Dot(Vector2.up, pivotOffset);


        //strait up comarison no work
        if (cubeState.front == side)
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;

        if (cubeState.back.Contains(side[4]))
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;

        if (cubeState.up.Contains(side[4]))
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;

        if (cubeState.down.Contains(side[4]))
            rotation.y = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;

        if (cubeState.left.Contains(side[4]))
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * 1;

        if (cubeState.right.Contains(side[4]))
            rotation.z = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;


        transform.Rotate(rotation, Space.Self);
        mouseRef = Input.mousePosition;
    }


    void AutoRotate()
    {
        dragging = false;

        var step = speed * Time.deltaTime;

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1f)
        {
            transform.localRotation = targetQuaternion;

            //unparent the pieces
            cubeState.PutDown(activeSide, transform.parent);
            readCube.ReadState();

            autoRotating = false;
        }
    }

    public void RotateToRightAngle()
    {
        var vec = transform.localEulerAngles;
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        targetQuaternion.eulerAngles = vec;
        autoRotating = true;
    } 
}
