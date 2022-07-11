using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Player : NetworkBehaviour
{
    public GameObject Card;
    public GameObject EnemyCard;
    public GameObject PlayerHandArea;
    public GameObject EnemyHandArea;
    public GameObject PlayerBattlefield;
    public GameObject EnemyBattlefield;

    List<GameObject> playerDeck = new List<GameObject>();

    List<GameObject> playerHand = new List<GameObject>();

    public int playerHitpoints = 20;
    private readonly int MAX_HAND_SIZE = 7;
    public int currentHandSize = 0;

    public override void OnStartClient()
    {
        base.OnStartClient();

        PlayerHandArea = GameObject.Find("PlayerHandArea");
        EnemyHandArea = GameObject.Find("EnemyHandArea");
        PlayerBattlefield = GameObject.Find("PlayerBattlefield");
        EnemyBattlefield = GameObject.Find("EnemyBattlefield");

        //CmdDealCard();
    }

    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();

        playerDeck.Add(Card);
        playerDeck.Add(EnemyCard);

        Debug.Log(playerDeck);
    }

    [Command]
    public void CmdDealCard()
    {
        if(currentHandSize < MAX_HAND_SIZE)
        {
            GameObject playerCard = Instantiate(playerDeck[Random.Range(0, playerDeck.Count)], new Vector2(0, 0), Quaternion.identity);
            NetworkServer.Spawn(playerCard, connectionToClient);
            RpcShowCard(playerCard, "Drawn");
            playerHand.Add(playerCard);
            currentHandSize++;
        }  
    }

    public void PlayCard(GameObject playerCard)
    {
        CmdPlayCard(playerCard);
    }

    [Command]
    void CmdPlayCard(GameObject playerCard)
    {
        RpcShowCard(playerCard, "Played");
    }

    [ClientRpc]
    void RpcShowCard(GameObject playerCard, string type)
    {
        if(type == "Drawn")
        {
            if(hasAuthority)
            {
                playerCard.transform.SetParent(PlayerHandArea.transform, false);
            }else
            {
                playerCard.transform.SetParent(EnemyHandArea.transform, false);
            }
        }
        if(type == "Played")
        {
            if(hasAuthority)
            {
                playerCard.transform.SetParent(PlayerBattlefield.transform, false);
            }else
            {
                playerCard.transform.SetParent(EnemyBattlefield.transform, false);
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
