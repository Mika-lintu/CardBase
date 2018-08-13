using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{

    
    public Text myCardName;
    public float offset;

    public int myCardsPos;

    public bool hasCard = false;

    public int myCardsID;

    public Sprite cardBack;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCardName.transform.position = Camera.main.WorldToScreenPoint((Vector3.up * offset) + gameObject.transform.position);
    }


    public void SetCardInfo(int cardID, string cardName, Sprite cardSprite, int posInDeck)
    {
        spriteRenderer.sprite = cardSprite;
        myCardName.text = cardName + "   "  + cardID;
        myCardsID = cardID;

        myCardsPos = posInDeck;
        hasCard = true;
        
    }

    public void SetAsEmpty()
    {
        hasCard = false;
        spriteRenderer.sprite = cardBack;
    }

    //public void SetNewCardInfo(int newCardID, int cardInDeck)
    //{
    //    cardNumMydeck = cardInDeck;
    //    for (int i = 0; i < theDeck.listOfCards.Count; i++)
    //    {
    //        if(theDeck.listOfCards[i].cardID == newCardID)
    //        {
    //            spriteRenderer.sprite = theDeck.listOfCards[i].cardImage;
    //            myCardName.text = theDeck.listOfCards[i].cardName;
    //        }
    //    }
    //}


}
