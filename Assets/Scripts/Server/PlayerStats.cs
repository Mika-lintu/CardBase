using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Deck theDeck;

    public int playerID;
    int[] thisPlayersDeck;
    int[] activeCards = {0 , 0 ,0 };

    List<GameObject> targets;
    NetworkServerUI server;

    private void Awake()
    {
        server = GameObject.Find("NetworkManager").GetComponent<NetworkServerUI>();
    }

    public void SetTargets(int cardID)
    {
        for (int i = 0; i < thisPlayersDeck.Length; i++)
        {
            if(thisPlayersDeck[i] == cardID)
            {
                for (int x = 0; x < activeCards.Length; x++)
                {
                    if(activeCards[x]== 0)
                    {
                        activeCards[x] = cardID;
                        SetTargetObject(cardID);
                    }
                }
            }
        }
    }

    void SetTargetObject(int cardID)
    {
        for (int i = 0; i < theDeck.listOfCards.Count; i++)
        {
            if(cardID == theDeck.listOfCards[i].cardID)
            {
                targets.Add(theDeck.listOfCards[i].targetObject);
            }
        }
    }

    public void SetDeck(List<int> deckRef)
    {
        thisPlayersDeck = deckRef.ToArray();
        server.SendPlayerDeck(1, 1, thisPlayersDeck);
        print(thisPlayersDeck.Length);
        for (int i = 0; i < thisPlayersDeck.Length; i++)
        {
            print(name + "  " + thisPlayersDeck[i]);
        }
    }

    public void TargetFound(GameObject cardTarget)
    {
        for (int i = 0; i < theDeck.listOfCards.Count; i++)
        {
            if(theDeck.listOfCards[i].targetObject == cardTarget)
            {
                server.SendMessage(playerID, 2, theDeck.listOfCards[i].cardID);
            }
        }
    }
    
}
