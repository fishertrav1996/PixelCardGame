using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragAndDrop : NetworkBehaviour
{

    public GameObject Canvas;
    public Player Player;

    private bool dragging = false;
    private GameObject startParent;
    private Vector2 startPos;
    private GameObject playerBattlefield;
    private bool isOverPlayerBattlefield;
    private bool canDrag = true;

    private readonly int MAX_BATTLEFIELD_SIZE = 5;


    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        if(!hasAuthority)
        {
            canDrag = false;
        }
    }

    public void BeginDrag()
    {
        if(!canDrag) return;

        dragging = true;
        startParent = transform.parent.gameObject;
        startPos = transform.position;
    }

    public void StopDrag()
    {
        if(!canDrag) return;

        dragging = false;

        if(isOverPlayerBattlefield & playerBattlefield != null)
        {
            if(playerBattlefield.transform.childCount < MAX_BATTLEFIELD_SIZE){
                transform.SetParent(playerBattlefield.transform, false);
                canDrag = false;
                NetworkIdentity networkIdentity = NetworkClient.connection.identity;
                Player = networkIdentity.GetComponent<Player>();
                Player.PlayCard(gameObject);
            }else{
                transform.position = startPos;
                transform.SetParent(startParent.transform, false);
            }         
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
