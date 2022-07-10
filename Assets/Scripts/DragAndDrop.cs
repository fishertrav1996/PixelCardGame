using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    private bool dragging = false;
    private GameObject startParent;
    private Vector2 startPos;
    private GameObject playerBattlefield;
    private bool isOverPlayerBattlefield;
    private bool canDrag = true;

    public GameObject Canvas;


    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
    }

    public void BeginDrag()
    {
        if(canDrag)
        {
            dragging = true;
            startParent = transform.parent.gameObject;
            startPos = transform.position;
        }
        
    }

    public void StopDrag()
    {
        dragging = false;

        if(isOverPlayerBattlefield)
        {
            transform.SetParent(playerBattlefield.transform, false);
            canDrag = false;
        }
        else
        {
            transform.position = startPos;
            transform.SetParent(startParent.transform, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverPlayerBattlefield = true;
        playerBattlefield = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverPlayerBattlefield = false;
        playerBattlefield = null;
    }

    // Update is called once per frame
    void Update()
    {
        // Follow the position of the mouse
        if(dragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }
}
