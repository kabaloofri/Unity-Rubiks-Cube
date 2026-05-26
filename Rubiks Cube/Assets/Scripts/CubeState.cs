using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeState : MonoBehaviour {
    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> up = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();

    public bool autoRotating = false;
    public bool initialized = false;
    public void PickUp(List<GameObject> cubeSide) {
        // attach all pices onto the center piece
        foreach (var face in cubeSide) {
            if (face != cubeSide[4]) {
                face.transform.parent.parent = cubeSide[4].transform.parent;
            }
        }
    }

    public void PutDown(List<GameObject> cubeSide, Transform pivot) {
        foreach (var face in cubeSide) {
            if (face != cubeSide[4]) {
                face.transform.parent.parent = pivot; // detach the side
            }
        }
    }
}
