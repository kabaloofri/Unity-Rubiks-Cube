using UnityEditor;
using UnityEngine;

public class RotateSide : MonoBehaviour
{
    private BoxCollider col;
    void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    public Collider[] rotate(bool reverse)
    {
        var objects = getObjectsInCollider();

        foreach (var obj in objects)
        {
            obj.transform.parent = transform; //move objects in col under the side 
        }

        Vector3 angle = new Vector3(0, 90, 0);

        if (gameObject.name == "right" || gameObject.name == "left")
        {
            angle = new Vector3(0, 0, 90);
        }

        angle *= reverse ? -1 : 1; 
        // if up or down - 90 on y, left and right - 90 on z
        // if reverse: -90

        transform.Rotate(angle);
        
        return objects;
    }
    public Collider[] getObjectsInCollider()
    {
        return Physics.OverlapBox(col.transform.position + col.center, col.size * 0.5f, col.transform.rotation);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(col.transform.position, col.size);
    }
}
