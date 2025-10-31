using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeState : MonoBehaviour
{
    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> up = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();


    public void PickUp(List<GameObject> cubeSide)
    {
        foreach (var face in cubeSide)
        {
            if (face != cubeSide[4])
            {
                //place all the faces onto the center face
                face.transform.parent.parent = cubeSide[4].transform.parent;
            }

            //rotate the center piece
            cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
        }
    }
}
