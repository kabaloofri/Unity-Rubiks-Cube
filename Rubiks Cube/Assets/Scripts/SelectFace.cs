using System.Collections.Generic;
using UnityEngine;

public class SelectFace : MonoBehaviour
{
    [SerializeField] LayerMask whatIsFaces;
    CubeState cubeState;
    ReadCube readCube;

    void Start()
    {
        cubeState = FindFirstObjectByType<CubeState>();
        readCube = FindFirstObjectByType<ReadCube>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            readCube.ReadState();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, whatIsFaces))
            {
                GameObject face = hit.collider.gameObject;

                var cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left,
                    cubeState.right,
                    cubeState.front,
                    cubeState.back,
                };

                //if the face exsist
                foreach (var cubeSide in cubeSides)
                {
                    if (cubeSide.Contains(face))
                    {
                        //pick it up

                        cubeState.PickUp(cubeSide);
                    }
                }
            }
        }
    }
}
