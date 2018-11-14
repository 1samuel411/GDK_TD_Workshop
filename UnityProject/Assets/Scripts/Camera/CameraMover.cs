using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    // Public Variables
    public float borderSize;
    public float movementSpeed;

    public float minXPos, maxXPos;
    public float minYPos, maxYPos;

    void LateUpdate()
    {
        CheckBorder();

        ClampPosition();
    }

    void CheckBorder()
    {
        Vector2 movementVector = Vector2.zero;

        // Check movement
        if(TouchingBot())
            movementVector.y = -1;
        if(TouchingTop())
            movementVector.y = 1;
        if(TouchingLeft())
            movementVector.x = -1;
        if(TouchingRight())
            movementVector.x = 1;

        // Normalize
        movementVector = movementVector.normalized;

        // Move Transform
        transform.position += new Vector3(movementVector.x, 0, movementVector.y) * movementSpeed * Time.deltaTime;
    }

    void ClampPosition()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, minXPos, maxXPos);
        position.z = Mathf.Clamp(position.z, minYPos, maxYPos);
        transform.position = position;
    }

    bool TouchingBot()
    {
        if (Input.mousePosition.y < borderSize)
            return true;

        return false;
    }

    bool TouchingLeft()
    {
        if (Input.mousePosition.x < borderSize)
            return true;

        return false;
    }


    bool TouchingTop()
    {
        if (Input.mousePosition.y > Screen.height - borderSize)
            return true;

        return false;
    }

    bool TouchingRight()
    {
        if (Input.mousePosition.x > Screen.width - borderSize)
            return true;

        return false;
    }
}
