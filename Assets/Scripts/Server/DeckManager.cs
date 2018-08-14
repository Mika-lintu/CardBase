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
    }

}
