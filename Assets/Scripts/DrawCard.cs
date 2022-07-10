using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCard : MonoBehaviour
{
    public GameObject Card;
    public GameObject EnemyCard;
    public GameObject PlayerHandArea;
    public GameObject EnemyHandArea;
    public Button button;

    private readonly int MAX_HAND_SIZE = 7;

    List<GameObject> playerHand = new List<GameObject>();
    List<GameObject> enemyHand = new List<GameObject>();

    List<GameObject> playerDeck = new List<GameObject>();
    List<GameObject> enemyDeck = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerDeck.Add(Card);
        playerDeck.Add(EnemyCard);
        enemyDeck.Add(Card);
        enemyDeck.Add(EnemyCard);

        button = GetComponent<Button>();

        //Draw 3 cards at the start of the game
        for(int i = 0; i < 3; i++)
        {
            OnClick();
        }  
    }

    public int getPlayerHandSize()
    {
        return playerHand.Count;
    }

    public void OnClick()
    {
        if(PlayerHandArea.transform.childCount < MAX_HAND_SIZE & EnemyHandArea.transform.childCount < MAX_HAND_SIZE)
        {
            GameObject playerCard = Instantiate(playerDeck[Random.Range(0, playerDeck.Count)], new Vector2(0, 0), Quaternion.identity);
            playerCard.transform.SetParent(PlayerHandArea.transform, false);
            playerHand.Add(playerCard);
            

            GameObject enemyCard = Instantiate(enemyDeck[Random.Range(0, enemyDeck.Count)], new Vector2(0, 0), Quaternion.identity);
            enemyCard.transform.SetParent(EnemyHandArea.transform, false);
            enemyHand.Add(enemyCard); 
        }else
        {
            button.interactable = false;
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
