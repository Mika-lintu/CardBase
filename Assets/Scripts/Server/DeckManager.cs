using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Deck theDeck;

    int[] shuffledDeck;
    public GameObject[] players;

    int deckSize;
    public int playerCount;

    NetworkServerUI server;

    private void Start()
    {
        server = GetComponent<NetworkServerUI>();
        deckSize = theDeck.listOfCards.Count;
        print(deckSize);
        List<int> tempShuffle = new List<int>();
        for (int i = 0; i < deckSize; i++)
        {
            tempShuffle.Add(theDeck.listOfCards[i].cardID);
        }
        shuffledDeck = tempShuffle.ToArray();
        ShuffleDeck();
    }

    void ShuffleDeck()
    {
        for (int i = 0; i < deckSize; i++)
        {
            int rnd = Random.Range(i, (deckSize - 1));
            int tempTemp = shuffledDeck[rnd];
            shuffledDeck[rnd] = shuffledDeck[i];
            shuffledDeck[i] = tempTemp;
            print("NEW DECK: " + shuffledDeck[i]);
        }
        //DealDeck(playerCount);
    }

    public void DealDeck()
    {

        int cardsDealt = 0;

        for (int i = 0; i < players.Length; i++)
        {
            cardsDealt = 0;
            List<int> arr = new List<int>();

            for (int x = 0; x < shuffledDeck.Length; x++)
            {
                if (cardsDealt == 3)
                {
                    break;
                }
                if (shuffledDeck[x] != 0)
                {
                    arr.Add(shuffledDeck[x]);
                    shuffledDeck[x] = 0;
                    cardsDealt++;
                }
            }
            players[i].GetComponent<PlayerStats>().SetDeck(arr);
        }


        /*
        int cardsDealt  = 0;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == enabled)
            {
                cardsDealt = 0;
                List<int> arr = new List<int>();
                for (int x = 0; x < 3; x++)
                {
                    if (cardsDealt == 3)
                    {
                        break;
                    }
                    else if (shuffledDeck[i] != 0)
                    {
                        arr.Add(shuffledDeck[]);
                        shuffledDeck[i] = 0;
                        
                        cardsDealt++;
                    }
                }
                players[i].GetComponent<PlayerStats>().SetDeck(arr);
            }
        }
        */
    }




    //public void DealDeck()
    //{
    //    int dealAmount = 0;
    //    int cardsDealt  = 0;

    //    dealAmount = deckSize / playerCount;
    //    for (int x = 0; x < playerCount; x++)
    //    {
    //        cardsDealt = 0;
    //        List<int> arr = new List<int>();
    //        for (int i = 0; i < deckSize; i++)
    //        {
    //            if (cardsDealt == dealAmount)
    //            {
    //                break;
    //            }
    //            else if (shuffledDeck[i] != 0)
    //            {
    //                arr.Add(shuffledDeck[i]);
    //                shuffledDeck[i] = 0;
    //                cardsDealt++;
    //            }

    //        }
    //        print(x);
    //        SendDecks(arr.ToArray(), (x +""));
    //    }
    //    print("Math result: " + dealAmount);
    //}

    //void SendDecks(int[] arr, string pl)
    //{
    //    for (int i = 0; i < arr.Length; i++)
    //    {
    //        print("card to player " + arr[i]);
    //    }
    //    server.SendPlayerDeck(1,1,arr);

    //}

}
