using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class RubixCube : MonoBehaviour
{
    public GameObject corners;
    public GameObject edges;
    public GameObject centers;

    public string whatIsCorners;
    public string whatIsEdges;
    public string whatIsCenters;

    public Vector3 boxSize = new Vector3(4,0.5f,4);

    private Transform rotator;
    

    void Start()
    {
        rotator = new GameObject("rotator").transform;
        rotator.position = transform.position;
    }

    void Update()
    {
        bool reverse = Input.GetKey(KeyCode.Space);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            U(reverse);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            D();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            L();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            R();
        }
    }

    void U(bool reverse=false)
    {
        var rotation = reverse ? new Vector3(0, -90, 0) : new Vector3(0, 90 ,0);
        RotateCube(new Vector3(0, 1.5f, 0), Vector3.zero, rotation);
    }
    void D(bool reverse=false)
    {
        var rotation = reverse ? new Vector3(0, -90, 0) : new Vector3(0, 90 ,0);
        RotateCube(new Vector3(0, -1.5f, 0), Vector3.zero, rotation);
    }
    void L()
    {
        RotateCube(new Vector3(0, 0, 2), new Vector3(90, 0,0), new Vector3(90, 90, 0));
    }
    void R()
    {
        RotateCube(new Vector3(0, 0,-2), new Vector3(90,0,0), new Vector3(90, 90, 0));
    }

    
    void RotateCube(Vector3 center, Vector3 CubeRotation, Vector3 rotation)
    {
        var pieces = Physics.OverlapBox(center, boxSize, Quaternion.Euler(CubeRotation));

        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].transform.parent = rotator.transform;
        }
        rotator.transform.Rotate(rotation);

        SortPiecesToParents(pieces);
    }
    void SortPiecesToParents(Collider[] pieces)
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].gameObject.tag == whatIsCorners)
            {
                //is corner
                pieces[i].transform.parent = corners.transform;
            }
            else if (pieces[i].gameObject.tag == whatIsEdges)
            {
                //is edge
                pieces[i].transform.parent = edges.transform;
            }
            else if (pieces[i].gameObject.tag == whatIsCenters)
            {
                // somthing else, prob centers
                pieces[i].transform.parent = centers.transform;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        drawBox(Color.yellow, new Vector3(0, 1.5f, 0) / 2, Vector3.zero);

        drawBox(Color.red, new Vector3(0, 0, 2) / 2, new Vector3(90, 0,0));        
        
        drawBox(Color.blue, new Vector3(0, 0,-2) / 2, new Vector3(90,0,0));
    }

    void drawBox(Color color, Vector3 center, Vector3 cubeRotation)
    {
        Gizmos.color = color;
        Gizmos.matrix = Matrix4x4.TRS(center, Quaternion.Euler(cubeRotation), transform.lossyScale);
        Gizmos.DrawCube(center, boxSize);
    }
}
