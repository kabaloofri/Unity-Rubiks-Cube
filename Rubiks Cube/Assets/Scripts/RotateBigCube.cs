using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class RotateBigCube : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 200f;
    private Vector2 firstSwipePos;
    private Vector2 secondSwipePos;
    private Vector2 currentSwipe;

    private Vector3 prevMousePos;
    private Vector3 mouseDelta;

    // Update is called once per frame
    void Update()
    {
        Swipe();
        
        Drag();
    }

    void Drag()
    {
        //hold cube and stuff
        if (Input.GetMouseButton(1))
        {
            mouseDelta = Input.mousePosition - prevMousePos;
            mouseDelta *= 0.1f;

            transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0) * transform.rotation;

        }
        //rotate smooth to target
        else
        {
            if (transform.rotation != target.rotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, speed * Time.deltaTime);
            }
        }
        prevMousePos = Input.mousePosition;
    }

    void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //get screan pos of click
            firstSwipePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            secondSwipePos = Input.mousePosition;

            //create vector from first and second pos

            currentSwipe = firstSwipePos - secondSwipePos;
            currentSwipe.Normalize();

            if (LeftSwipe())
            {
                target.Rotate(0, -90, 0, Space.World);
            }

            else if (RightSwipe())
            {
                target.Rotate(0, 90, 0, Space.World);
            }
            else if (UpLeftSwipe())
            {
                target.Rotate(-90, 0, 0, Space.World);
            }
            else if (UpRightSwipe())
            {
                target.Rotate(0, 0, 90, Space.World);
            }
            else if (DownLeftSwipe())
            {
                target.Rotate(0, 0, -90, Space.World);
            }
            else if (DownRightSwipe())
            {
                target.Rotate(90, 0, 0, Space.World);
            }
        }
    }

    bool LeftSwipe()
    {
        return currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    bool RightSwipe()
    {
        return currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    bool UpLeftSwipe()
    {
        return currentSwipe.y > 0 && currentSwipe.x < 0;
    }

    bool UpRightSwipe()
    {
        return currentSwipe.y > 0 && currentSwipe.x > 0;
    }

    bool DownLeftSwipe()
    {
        return currentSwipe.y < 0 && currentSwipe.x < 0;
    }

    bool DownRightSwipe()
    {
        return currentSwipe.y < 0 && currentSwipe.x > 0;
    }
}
