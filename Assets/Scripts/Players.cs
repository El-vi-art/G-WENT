using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public Deck deck;
    public Fields field;
    public Hands hand;
    public bool turno;

    void Start ()
    {
        player1();
        player2();
    }

    public void player1()
    {
        deck = new Deck();
       
        field = new Fields();
        hand = new Hands();
        turno = false;
    }
    public void player2()
    {
        deck = new Deck();
        
        field = new Fields();
        hand = new Hands();
        turno = false;
    }
       public void OnMouseDown()
    {
        if (turno)
        {
            Card selectedCard = hand.hand[0]; // Seleccionar la primera carta de la mano del jugador1
            field.PlayCard(selectedCard); // Llamar al método playCard() de la clase Fields
            hand.RemoveCard(selectedCard,hand); // Eliminar la carta seleccionada de la mano del jugador1
            Debug.Log("Jugador1 ha hecho una jugada");
            turno = false;
        }
        else if (!turno )
        {
            Card selectedCard = hand.hand[0]; // Seleccionar la primera carta de la mano del jugador2
            field.PlayCard(selectedCard); // Llamar al método playCard() de la clase Fields
            hand.RemoveCard(selectedCard,hand); // Eliminar la carta seleccionada de la mano del jugador2
            Debug.Log("Jugador2 ha hecho una jugada");
            turno = true;
        }
    }
}