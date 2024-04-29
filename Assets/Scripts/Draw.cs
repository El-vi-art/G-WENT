using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentEngine;

public class Draw : MonoBehaviour
{
    
    public bool start = false;
    bool drawed;
    int DrawedCards = 0;
    public int CardsInHand;
    public GameObject player;
    public GameObject Card;
    public GameObject Hand;
    GameObject Cementery1;
    GameObject Cementery2;
    List<Card> Deck;
    
    void Start()
    {
        Deck = Player.Shuffle(player.GetComponent<Player>().Cards);
        CardsInHand = 0;
        Cementery1 = GameObject.Find("Cementery1");
        Cementery2 = GameObject.Find("Cementery2");
    }
    private void Update()
    {
        if (!start)
        {
            Invoke(nameof(EffectDraw), 1.5f);
            if (CardsInHand == 10)
            {
                start = true;
            }
        }
        drawed = player.GetComponent<Player>().Drawed;
    }

    //This method deal a card to player hand
    public void OnClick()
    {
        if (DrawedCards < Deck.Count && CardsInHand < 10 && drawed == false)
        {
            
            GameObject card = Instantiate(Card, new Vector2(0, 0), Quaternion.identity);
            card.GetComponent<CardDisplay>().card = Deck[DrawedCards];
            card.transform.SetParent(Hand.transform, false);
            DrawedCards++;
            CardsInHand++;
            player.GetComponent<Player>().Drawed = true;
        }
        else if (DrawedCards < Deck.Count && CardsInHand == 10 && (transform.parent == GameObject.Find("Deck1").transform || transform.parent == GameObject.Find("Deck2")) && drawed == false)
        {
            if (transform.parent == GameObject.Find("Deck1").transform)
            {
                GameObject card = Instantiate(Card, new Vector2(0, 0), Quaternion.identity);
                card.GetComponent<CardDisplay>().card = Deck[DrawedCards];
                card.transform.SetParent(Cementery1.transform, false);
                DrawedCards++;
                card.SetActive(false);
            }
            else
            {
                GameObject card = Instantiate(Card, new Vector2(0, 0), Quaternion.identity);
                card.GetComponent<CardDisplay>().card = Deck[DrawedCards];
                card.transform.SetParent(Cementery2.transform, false);
                DrawedCards++;
                card.SetActive(false);
            }
        }
    }

    //This method deal cards to player hand at begin of  a new round or by card effect
    public void EffectDraw()
    {
        if (DrawedCards < Deck.Count && CardsInHand < 10)
        {
            
            GameObject card = Instantiate(Card, new Vector2(0, 0), Quaternion.identity);
            card.GetComponent<CardDisplay>().card = Deck[DrawedCards];
            card.transform.SetParent(Hand.transform, false);
            DrawedCards++;
            CardsInHand++;
        }
        else if (DrawedCards < Deck.Count && CardsInHand == 10 && (transform.parent == GameObject.Find("Deck1").transform || transform.parent == GameObject.Find("Deck2")))
        {
            if (transform.parent == GameObject.Find("Deck1").transform)
            {
                GameObject card = Instantiate(Card, new Vector2(0, 0), Quaternion.identity);
                card.GetComponent<CardDisplay>().card = Deck[DrawedCards];
                card.transform.SetParent(Cementery1.transform, false);
                DrawedCards++;
                card.SetActive(false);
            }
            else
            {
                GameObject card = Instantiate(Card, new Vector2(0, 0), Quaternion.identity);
                card.GetComponent<CardDisplay>().card = Deck[DrawedCards];
                card.transform.SetParent(Cementery2.transform, false);
                DrawedCards++;
                card.SetActive(false);
            }
        }
    }
    
    
}
