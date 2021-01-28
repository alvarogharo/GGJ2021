using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorClicked;
    public Texture2D cursorUp;
    public Texture2D cursorDown;
    public Texture2D cursorLeft;
    public Texture2D cursorRight;
    public float horizontalOffset;
    public float verticalOffset;
    public int stateChangeDelay;

    public CursorState currentState;

    private Texture2D[] cursorTypes;
    public Vector3 mousePos = Vector3.zero;
    public Vector3 prevMousePos = Vector3.zero;
    private bool firstFrame = true;
    private int stateCount = 0;

    public enum CursorState
    {
        None = 0,
        Clicked = 1,
        Up = 2,
        Down = 3,
        Left = 4,
        Right = 5
    }

    // Start is called before the first frame update
    void Awake()
    {
        Texture2D[] cursorTypesInit = { cursor, cursorClicked, cursorUp, cursorDown, cursorLeft, cursorRight };
        cursorTypes = cursorTypesInit;
        currentState = CursorState.None;
        ChangeCursor(cursorTypes[(int)currentState]);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        Vector3 mouseMotion = prevMousePos - mousePos;
        if (stateCount < stateChangeDelay && (currentState == CursorState.Up || currentState == CursorState.Down || currentState == CursorState.Left || currentState == CursorState.Right))
        {
            stateCount++;
        }
        else
        {
            stateCount = 0;
            if (!Input.GetMouseButton(0))
            {
                if ((mouseMotion.x <= horizontalOffset && mouseMotion.x >= -horizontalOffset) || (mouseMotion.y <= verticalOffset && mouseMotion.y >= -verticalOffset))
                {
                    ChangeCursor(cursor);
                }

                if (Math.Abs(mouseMotion.x) >= Math.Abs(mouseMotion.y))
                {
                    if (mouseMotion.x > horizontalOffset)
                    {
                        ChangeCursor(cursorLeft);
                    }
                    else if (mouseMotion.x < -horizontalOffset)
                    {
                        ChangeCursor(cursorRight);
                    }
                }
                else
                {

                    if (mouseMotion.y > verticalOffset)
                    {
                        ChangeCursor(cursorDown);
                    }
                    else if (mouseMotion.y < -verticalOffset)
                    {
                        ChangeCursor(cursorUp);
                    }
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            ChangeCursor(cursorClicked);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ChangeCursor(cursor);
        }

        if (firstFrame)
        {
            ChangeCursor(cursor);
            firstFrame = false;
        }

        prevMousePos = mousePos;
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        currentState = (CursorState)Array.IndexOf(cursorTypes, cursorType);
        Cursor.SetCursor(cursorType, Vector2.zero, CursorMode.Auto);
    }

}
