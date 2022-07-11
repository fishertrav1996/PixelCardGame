using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class DrawCard : NetworkBehaviour
{
    public Player Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnClick()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        Player = networkIdentity.GetComponent<Player>();
        Player.CmdDealCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
