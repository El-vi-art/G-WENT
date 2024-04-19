using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : ScriptableObject
{
    public List<Card> hand = new List<Card>();
    //List<Card> hand2 = new List<Card>();
    void Start()
    {
        
        Deck.SacarCartas(10,Deck.deck1,hand);
        //Deck.SacarCartas(10,Deck.deck1,hand2);
        Debug.Log("manos repartidas");
    }

    public void RemoveCard(Card card,Hands hand)
    {
        hand.Remove(card);
    }

    private void Remove(Card card)
    {
        hand.Remove(card);
    }
}
