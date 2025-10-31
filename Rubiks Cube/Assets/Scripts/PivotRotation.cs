using System.Collections.Generic;
using Unity.VisualScripting;
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

    float sensitivity = 0.4f;
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
            }
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

        Vector3 mouseOffset = (Input.mousePosition - mouseRef);

        if (side == cubeState.front)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sensitivity * -1;
        }

        transform.Rotate(rotation, Space.Self);
        mouseRef = Input.mousePosition;
    }
}
