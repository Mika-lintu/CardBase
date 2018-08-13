using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeck: MonoBehaviour
{

    public Deck theDeck;
    
    public CardSlot[] cardSlots;
    int[] myDeck;
    int activeCards;

    int lastCardChecked;

    private void Start()
    {
        
    }

    public void SetNewCard(int slotNum)
    {
        switch (slotNum)
        {
            case 0:
                GetCardInfo(myDeck[0], slotNum, 0);
                break;
            case 1:
                GetCardInfo(myDeck[1], slotNum, 1);
                break;
            case 2:
                GetCardInfo(myDeck[2], slotNum, 2);
                break;
        default:
                break;
        }
        /*
        for (int i = 0; i < myDeck.Length; i++)
        {
            if(lastCardChecked == i)
            {
           
                if(myDeck[i] != 0)
                {
                    GetCardInfo(myDeck[i],slotNum, i);
                    
                    SetActiveCard(myDeck[i]);
                    lastCardChecked = i;
                    return;
                }
            }
        }
        */
    }

    void SetActiveCard(int cardID)
    {
        //SEND TO SERVER
        NetworkClientUI.SendCardInfo(cardID);
    }

    public void DestroyCard(int cardID)
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if(cardSlots[i].myCardsID == cardID)
            {
                int temp;
                temp = cardSlots[i].myCardsPos;
                myDeck[temp] = 0;
                cardSlots[i].SetAsEmpty();
            }
        }
    }

    public void SetDeck(List<int> deckFromServer)
    {
        myDeck = deckFromServer.ToArray();
        SetNewCard(0);
        SetNewCard(1);
        SetNewCard(2);
    }

    void GetCardInfo(int cardID, int slotID, int deckIndex)
    {
        for (int i = 0; i < theDeck.listOfCards.Count; i++)
        {
            
            if (cardID == theDeck.listOfCards[i].cardID)
            {

                Sprite sprite;
                string cardName;
                sprite = theDeck.listOfCards[i].cardImage;
                cardName = theDeck.listOfCards[i].cardName;
                cardSlots[slotID].SetCardInfo(cardID, cardName, sprite, deckIndex);
               
            }
        }
    }


    //NetworkClientUI networkClient;

    //public GameObject[] cards;
    //public int[] mycards;

    //private int lastCheckedCard = -1;
    ////List<int> handCards;
    //string printString = "";

    //private void OnGUI()
    //{

    //    GUI.Box(new Rect(((Screen.width / 2)-50),  30, 300, 50), "");
    //    GUI.Label(new Rect(((Screen.width/2)-50), 40, 300, 20), printString);
    //}
    //private void Start()
    //{
    //    networkClient = GetComponent<NetworkClientUI>();
    //    SetStartCards();
    //}

    //void SetStartCards()
    //{
    //    for (int i = 0; i < cards.Length; i++)
    //    {
    //        SetNewCard(i);
    //    }
    //}

    //void SetNewCard(int slotNum)
    //{
    //    for (int i = 0; i < mycards.Length; i++)
    //    {
    //        if (lastCheckedCard < i)
    //        {
    //            if (mycards[i] != 0)
    //            {
    //                lastCheckedCard = i;
    //                //PrintToGUI(mycards[i]);
    //                cards[slotNum].GetComponent<CardSlot>().SetNewCardInfo(mycards[i], i);
    //                return;
    //            }
    //        }
    //    }
    //}

    //public void DestroyCard(int slot)
    //{
    //    int cardToDestroy = 0;
    //    cardToDestroy = cards[slot].GetComponent<CardSlot>().cardNumMydeck;
    //    mycards[cardToDestroy] = 0;
    //    printString = "" + cardToDestroy;
    //    SetNewCard(slot);
    //}

    //public void GetHandCards(List<int> list)
    //{
    //    mycards = list.ToArray();
    //    for (int i = 0; i < mycards.Length; i++)
    //    {
    //        PrintToGUI(mycards[i]);
    //    }
    //    for (int i = 0; i < 3; i++)
    //    {
    //        SetNewCard(i);
    //    }
    //}
    //void PrintToGUI(int i)
    //{
    //    printString = printString + " " +i;
    //}
}
