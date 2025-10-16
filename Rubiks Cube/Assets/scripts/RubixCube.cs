using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class RubixCube : MonoBehaviour
{
    public GameObject corners;
    public GameObject edges;
    public GameObject centers;

    public LayerMask whatIsCorners;
    public LayerMask whatIsEdges;
    public LayerMask whatIsCenters;
    void Update()
    {
        bool reverse = Input.GetKey(KeyCode.Space);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            U(reverse);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            D(reverse);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            L(reverse);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            R(reverse);
        }
    }

    void U(bool reverse=false)
    {
        var top = GameObject.Find("top").GetComponent<RotateSide>();

        SortPiecesToParents(top.rotate(reverse));
    }
    void D(bool reverse=false)
    {
        var bottom = GameObject.Find("bottom").GetComponent<RotateSide>();

        SortPiecesToParents(bottom.rotate(reverse));
    }
    void L(bool reverse=false)
    {
        var right = GameObject.Find("right").GetComponent<RotateSide>();

        SortPiecesToParents(right.rotate(reverse));
    }
    void R(bool reverse=false)
    {
        var left = GameObject.Find("left").GetComponent<RotateSide>();

        SortPiecesToParents(left.rotate(reverse));
    }


    void SortPiecesToParents(Collider[] pieces)
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].gameObject.layer == whatIsCorners)
            {
                //is corner
                pieces[i].transform.parent = corners.transform;
            }
            else if (pieces[i].gameObject.layer == whatIsEdges)
            {
                //is edge
                pieces[i].transform.parent = edges.transform;
            }
            else if (pieces[i].gameObject.layer == whatIsCenters)
            {
                //prob centers
                pieces[i].transform.parent = centers.transform;
            }
        }
    }
}
