using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GwentEngine;

public class Player : MonoBehaviour
{
    GameObject Faction1;
    GameObject Faction2;
    public bool Played = false;
    public bool Passed = false;
    public bool Drawed = false;
    public bool IsPlaying = false;
    public bool SustituteSelected = false;

    public Leader Leader;
    public List<Card> Cards;
    public GameObject MeleeZone;
    public GameObject RangeZone;
    public GameObject SiegeZone;
    public GameObject MeleeBuff;
    public GameObject RangeBuff;
    public GameObject SiegeBuff;
    
    int MeleeLinePower = 0;
    int RangeLinePower = 0;
    int SiegeLinePower = 0;
    int TotalPower = 0;

    public TMP_Text MLP;
    public TMP_Text RLP;
    public TMP_Text SLP;
    public TMP_Text TP;

    //This Method calculate player power on each line constantly
    public void GetLinePower(GameObject LineZone)
    {
        int LinePower = 0;
        List<CardDisplay> list = new List<CardDisplay>();
        foreach(Transform card in LineZone.transform)
        {
            CardDisplay carta = card.GetComponent<CardDisplay>();
            if(carta != null && card.gameObject.activeInHierarchy == true)
            {
                list.Add(carta);
            }
        }
        foreach(CardDisplay carta in list)
        {
            Card.Rank rank = carta.CardRank;
            Card.CardTipe tipe = carta.Cardtipe;
            int Power = carta.AttackPower;
            bool x = carta.Buffed;
            bool y = carta.Debuffed;
            bool z = carta.Upgraded;
            bool a = carta.AffectedByWeather;

            if(tipe == Card.CardTipe.Unit)
            {
                if(rank == Card.Rank.Silver)
                {
                    if(z == true)
                    {
                        Power *= 2;
                    }
                    if(y == true)
                    {
                        Power -= 2;
                    }
                    if(x == true)
                    {
                        Power += 2;
                    }
                    if (a == true)
                    {
                        Power = 1;
                    }
                    LinePower += Power;
                }
                else
                {
                    LinePower += Power;
                }
            }

            else
            {
                LinePower += 0;
            }


        }
        if(LineZone == MeleeZone)
        {
            MeleeLinePower = LinePower;
        }
        else if(LineZone == RangeZone)
        {
            RangeLinePower = LinePower;
        }
        else if(LineZone == SiegeZone)
        {
            SiegeLinePower = LinePower;
        }

        
    }

    //This Method calculate player total power constantly
    public void GetTotalPower()
    {
        TotalPower = MeleeLinePower + RangeLinePower + SiegeLinePower;
    }

    //This Method return player total power
    public int GetFinalPower()
    {
        int Power = TotalPower;
        return Power;
    }

    //This Method Shuffle a list of cards
    public static List<Card> Shuffle(List<Card> cards)
    {
        System.Random random = new();
        List<Card> list = new();

        while (cards.Count > 0)
        {
            int index = random.Next(0, cards.Count);
            list.Add(cards[index]);
            cards.RemoveAt(index);
        }
        return list;
    }

    void Start()
    {
       Faction1 = GameObject.Find("Faction1");
       Faction2 = GameObject.Find("Faction2");
       if(Leader.CardFaction == Card.Faction.ElpidioValdez)
       {
           foreach (var x in Faction1.GetComponent<FactionDeck>().DeckFaction)
                Cards.Add(x);
           
       }
       else
       {
            foreach (var x in Faction2.GetComponent<FactionDeck>().DeckFaction)
                Cards.Add(x);
       }
    }
   
    void Update()
    {
        if(IsPlaying == false || Passed == false)
        {
            GetLinePower(MeleeZone);
            GetLinePower(RangeZone);
            GetLinePower(SiegeZone);
            GetTotalPower();
        }
        MLP.text = MeleeLinePower.ToString();
        RLP.text = RangeLinePower.ToString();
        SLP.text = SiegeLinePower.ToString();
        TP.text = TotalPower.ToString();
     }

}
