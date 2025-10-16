using UnityEngine;

public class rotateCube : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(new Vector3(0,0,-90));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Rotate(new Vector3(0,0,90));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, -90,0));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, 90, 0));
        }
    }
}
