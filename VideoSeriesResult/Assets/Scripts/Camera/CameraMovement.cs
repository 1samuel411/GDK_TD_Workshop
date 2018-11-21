using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public int borderSize;
    public float movementSpeed;

    public float minXPos, maxXPos;
    public float minZPos, maxZPos;

    void Start()
    {

    }

    void Update()
    {
        CheckBorder();

        ClampPosition();
    }

    void ClampPosition()
    {
        Vector3 curPos = transform.position;
        curPos.x = Mathf.Clamp(curPos.x, minXPos, maxXPos);
        curPos.z = Mathf.Clamp(curPos.z, minZPos, maxZPos);
        transform.position = curPos;
    }

    void CheckBorder()
    {
        Vector2 movementVector = Vector2.zero;
        if (Input.mousePosition.x < borderSize)
        {
            // move left
            movementVector.x = -1;
        }
        if (Input.mousePosition.x > (Screen.width - borderSize))
        {
            // move right
            movementVector.x = 1;
        }

        if (Input.mousePosition.y < borderSize)
        {
            // move down
            movementVector.y = -1;
        }
        if (Input.mousePosition.y > (Screen.height - borderSize))
        {
            // move up
            movementVector.y = 1;
        }

        movementVector = movementVector.normalized;

        transform.position += new Vector3(movementVector.x, 0, movementVector.y) * movementSpeed * Time.deltaTime;
    }
}
