using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fields : MonoBehaviour
{
    static List<Card> cuerpoACuerpo =new List<Card>();
    static List<Card> ataqueAdistancia =new List<Card>();
    static List<Card> asedio =new List<Card>();
    static List<Card> side =new List<Card>();

    public void PlayCard(Card card)
    {
        if (card.campo=="M")
        {cuerpoACuerpo.Add(card);}
        else if (card.campo=="R")
        {ataqueAdistancia.Add(card);}
        else if (card.campo=="S")
        {asedio.Add(card);}
        else if (card.campo=="Side")
        {side.Add(card);}
        
    }
}