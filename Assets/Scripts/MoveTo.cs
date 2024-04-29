using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GwentEngine;

public class MoveTo: MonoBehaviour
{
    GameObject Player1;
    GameObject Player2;
    GameObject Hand1;
    GameObject Hand2;
    GameObject Deck1;
    GameObject Deck2;
    GameObject Melee1;
    GameObject Melee2;
    GameObject Range1;
    GameObject Range2;
    GameObject Siege1;
    GameObject Siege2;
    GameObject MeleeUp1;
    GameObject MeleeUp2;
    GameObject RangeUp1;
    GameObject RangeUp2;
    GameObject SiegeUp1;
    GameObject SiegeUp2;
    GameObject MeleeWheather;
    GameObject RangeWheather;
    GameObject SiegeWheather;
    readonly int WheatherLimit = 1;

    Transform startParent;

    private void Start()
    {
        startParent = transform.parent;
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");
        Deck1 = GameObject.Find("Deck1");
        Deck2 = GameObject.Find("Deck2");
        Hand1 = GameObject.Find("Hand1");
        Hand2 = GameObject.Find("Hand2");
        Melee1 = GameObject.Find("Melee1");
        Melee2 = GameObject.Find("Melee2");
        Range1 = GameObject.Find("Range1");
        Range2 = GameObject.Find("Range2");
        Siege1 = GameObject.Find("Siege1");
        Siege2 = GameObject.Find("Siege2");
        MeleeUp1 = GameObject.Find("MeleeUp1");
        MeleeUp2 = GameObject.Find("MeleeUp2");
        RangeUp1 = GameObject.Find("RangeUp1");
        RangeUp2 = GameObject.Find("RangeUp2");
        SiegeUp1 = GameObject.Find("SiegeUp1");
        SiegeUp2 = GameObject.Find("SiegeUp2");
        MeleeWheather = GameObject.Find("ClimaOfMelee");
        RangeWheather = GameObject.Find("ClimaOfRange");
        SiegeWheather = GameObject.Find("ClimaOfSiege");
    }

    //This method set cards on the field
    public void MoveToMeleeZone()
    {
        var a = GetComponent<CardDisplay>().Cardtipe;
        var x = GetComponent<CardDisplay>().Position1;
        var y = GetComponent<CardDisplay>().Position2;
        var z = GetComponent<CardDisplay>().Position3;
        if(((startParent == Hand1.transform && Player1.GetComponent<Player>().IsPlaying == true && Player1.GetComponent<Player>().Played == false) || (startParent == Hand2.transform && Player2.GetComponent<Player>().IsPlaying == true && Player2.GetComponent<Player>().Played == false)) && a == Card.CardTipe.Weather)
        {
            int WheaterInField = 0;
            foreach(Transform  card in MeleeWheather.transform)
            {
                CardDisplay rb = card.GetComponent<CardDisplay>();
                if(rb != null)
                {
                    WheaterInField++;
                }
            }
            if (WheaterInField >= WheatherLimit)
            {

            }
            else
            {
                transform.SetParent(MeleeWheather.transform, false);
                GetComponent<CardDisplay>().InField = true;
                if (startParent == Hand1.transform)
                {
                    Player1.GetComponent<Player>().Played = true;
                    Deck1.GetComponent<Draw>().CardsInHand--;
                }
                else
                {
                    Player2.GetComponent<Player>().Played = true;
                    Deck2.GetComponent<Draw>().CardsInHand--;
                }
                transform.GetComponent<CardDisplay>().CastEffect();
            }
        }
        else if(((startParent == Hand1.transform && Player1.GetComponent<Player>().IsPlaying == true && Player1.GetComponent<Player>().Played == false) || (startParent == Hand2.transform && Player2.GetComponent<Player>().IsPlaying == true && Player2.GetComponent<Player>().Played == false)) && a == Card.CardTipe.Lure)
        {
            int CardInZoneP1 = 0;
            int CardInZoneP2 = 0;
            foreach (Transform card in Melee1.transform)
            {
                CardDisplay rb = card.GetComponent<CardDisplay>();
                if (rb != null)
                {
                    CardInZoneP1++;
                }
            }
            foreach (Transform card in Melee2.transform)
            {
                CardDisplay rb = card.GetComponent<CardDisplay>();
                if (rb != null)
                {
                    CardInZoneP2++;
                }
            }
            if (startParent == Hand1.transform && CardInZoneP1 > 0)
            {
                transform.SetParent(Melee1.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player1.GetComponent<Player>().Played = true;
                Deck1.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
            else if (startParent == Hand2.transform && CardInZoneP2 > 0)
            {
                transform.SetParent(Melee2.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player2.GetComponent<Player>().Played = true;
                Deck2.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
        }
        else if(startParent == Hand1.transform && Player1.GetComponent<Player>().IsPlaying == true && Player1.GetComponent<Player>().Played == false)
        {
            if ((a == Card.CardTipe.Unit && (x == Card.Position.M || y == Card.Position.M || z == Card.Position.M)) || a == Card.CardTipe.ClearWeather)
            {
                transform.SetParent(Melee1.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player1.GetComponent<Player>().Played = true;
                Deck1.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
            else if (a == Card.CardTipe.Upgrade)
            {
                transform.SetParent(MeleeUp1.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player1.GetComponent<Player>().Played = true;
                Deck1.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
        }
        else if(startParent == Hand2.transform && Player2.GetComponent<Player>().IsPlaying == true && Player2.GetComponent<Player>().Played == false)
        {
            if ((a == Card.CardTipe.Unit && (x == Card.Position.M || y == Card.Position.M || z == Card.Position.M)) || a == Card.CardTipe.ClearWeather)
            {
                transform.SetParent(Melee2.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player2.GetComponent<Player>().Played = true;
                Deck2.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
            else if (a == Card.CardTipe.Upgrade)
            { 
                transform.SetParent(MeleeUp2.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player2.GetComponent<Player>().Played = true;
                Deck2.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
           
        }
    }
    //This method set cards on the field
    public void MoveToRangeZone()
    {
        var a = GetComponent<CardDisplay>().Cardtipe;
        var x = GetComponent<CardDisplay>().Position1;
        var y = GetComponent<CardDisplay>().Position2;
        var z = GetComponent<CardDisplay>().Position3;
        if (((startParent == Hand1.transform && Player1.GetComponent<Player>().IsPlaying == true && Player1.GetComponent<Player>().Played == false) || (startParent == Hand2.transform && Player2.GetComponent<Player>().IsPlaying == true && Player2.GetComponent<Player>().Played == false)) && a == Card.CardTipe.Weather)
        {
            int WheaterInField = 0;
            foreach (Transform card in RangeWheather.transform)
            {
                CardDisplay rb = card.GetComponent<CardDisplay>();
                if (rb != null)
                {
                    WheaterInField++;
                }
            }
            if (WheaterInField >= WheatherLimit)
            {

            }
            else
            {
                transform.SetParent(RangeWheather.transform, false);
                GetComponent<CardDisplay>().InField = true;
                if (startParent == Hand1.transform)
                {
                    Player1.GetComponent<Player>().Played = true;
                    Deck1.GetComponent<Draw>().CardsInHand--;
                }
                else
                {
                    Player2.GetComponent<Player>().Played = true;
                    Deck2.GetComponent<Draw>().CardsInHand--;
                }
                transform.GetComponent<CardDisplay>().CastEffect();
            }
        }
        else if (((startParent == Hand1.transform && Player1.GetComponent<Player>().IsPlaying == true && Player1.GetComponent<Player>().Played == false) || (startParent == Hand2.transform && Player2.GetComponent<Player>().IsPlaying == true && Player2.GetComponent<Player>().Played == false)) && a == Card.CardTipe.Lure)
        {
            int CardInZoneP1 = 0;
            int CardInZoneP2 = 0;
            foreach (Transform card in Range1.transform)
            {
                CardDisplay rb = card.GetComponent<CardDisplay>();
                if (rb != null)
                {
                    CardInZoneP1++;
                }
            }
            foreach (Transform card in Range2.transform)
            {
                CardDisplay rb = card.GetComponent<CardDisplay>();
                if (rb != null)
                {
                    CardInZoneP2++;
                }
            }
            if (startParent == Hand1.transform && CardInZoneP1 > 0)
            {
                transform.SetParent(Range1.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player1.GetComponent<Player>().Played = true;
                Deck1.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
            else if (startParent == Hand2.transform && CardInZoneP2 > 0)
            {
                transform.SetParent(Range2.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player2.GetComponent<Player>().Played = true;
                Deck2.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
        }
        else if (startParent == Hand1.transform && Player1.GetComponent<Player>().IsPlaying == true && Player1.GetComponent<Player>().Played == false)
        {
            if ((a == Card.CardTipe.Unit && (x == Card.Position.R || y == Card.Position.R || z == Card.Position.R)) || a == Card.CardTipe.ClearWeather)
            {
                transform.SetParent(Range1.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player1.GetComponent<Player>().Played = true;
                Deck1.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
            else if (a == Card.CardTipe.Upgrade)
            {
                transform.SetParent(RangeUp1.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player1.GetComponent<Player>().Played = true;
                Deck1.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
        }
        else if (startParent == Hand2.transform && Player2.GetComponent<Player>().IsPlaying == true && Player2.GetComponent<Player>().Played == false)
        {
            if ((a == Card.CardTipe.Unit && (x == Card.Position.R || y == Card.Position.R || z == Card.Position.R)) || a == Card.CardTipe.ClearWeather)
            {
                transform.SetParent(Range2.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player2.GetComponent<Player>().Played = true;
                Deck2.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
            else if (a == Card.CardTipe.Upgrade)
            {
                transform.SetParent(RangeUp2.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player2.GetComponent<Player>().Played = true;
                Deck2.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }

        }
    }
    //This method set cards on the field
    public void MoveToSiegeZone()
    {
        var a = GetComponent<CardDisplay>().Cardtipe;
        var x = GetComponent<CardDisplay>().Position1;
        var y = GetComponent<CardDisplay>().Position2;
        var z = GetComponent<CardDisplay>().Position3;
        if (((startParent == Hand1.transform && Player1.GetComponent<Player>().IsPlaying == true && Player1.GetComponent<Player>().Played == false) || (startParent == Hand2.transform && Player2.GetComponent<Player>().IsPlaying == true && Player2.GetComponent<Player>().Played == false)) && a == Card.CardTipe.Weather)
        {
            int WheaterInField = 0;
            foreach (Transform card in SiegeWheather.transform)
            {
                CardDisplay rb = card.GetComponent<CardDisplay>();
                if (rb != null)
                {
                    WheaterInField++;
                }
            }
            if (WheaterInField >= WheatherLimit)
            {

            }
            else
            {
                transform.SetParent(SiegeWheather.transform, false);
                GetComponent<CardDisplay>().InField = true;
                if (startParent == Hand1.transform)
                {
                    Player1.GetComponent<Player>().Played = true;
                    Deck1.GetComponent<Draw>().CardsInHand--;
                }
                else
                {
                    Player2.GetComponent<Player>().Played = true;
                    Deck2.GetComponent<Draw>().CardsInHand--;
                }
                transform.GetComponent<CardDisplay>().CastEffect();
            }
        }
        else if (((startParent == Hand1.transform && Player1.GetComponent<Player>().IsPlaying == true && Player1.GetComponent<Player>().Played == false) || (startParent == Hand2.transform && Player2.GetComponent<Player>().IsPlaying == true && Player2.GetComponent<Player>().Played == false)) && a == Card.CardTipe.Lure)
        {
            int CardInZoneP1 = 0;
            int CardInZoneP2 = 0;
            foreach (Transform card in Siege1.transform)
            {
                CardDisplay rb = card.GetComponent<CardDisplay>();
                if (rb != null)
                {
                    CardInZoneP1++;
                }
            }
            foreach (Transform card in Siege2.transform)
            {
                CardDisplay rb = card.GetComponent<CardDisplay>();
                if (rb != null)
                {
                    CardInZoneP2++;
                }
            }
            if (startParent == Hand1.transform && CardInZoneP1 > 0)
            {
                transform.SetParent(Siege1.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player1.GetComponent<Player>().Played = true;
                Deck1.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
            else if (startParent == Hand2.transform && CardInZoneP2 > 0)
            {
                transform.SetParent(Siege2.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player2.GetComponent<Player>().Played = true;
                Deck2.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
        }
        else if (startParent == Hand1.transform && Player1.GetComponent<Player>().IsPlaying == true && Player1.GetComponent<Player>().Played == false)
        {
            if ((a == Card.CardTipe.Unit && (x == Card.Position.S || y == Card.Position.S || z == Card.Position.S)) || a == Card.CardTipe.ClearWeather)
            {
                transform.SetParent(Siege1.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player1.GetComponent<Player>().Played = true;
                Deck1.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
            else if (a == Card.CardTipe.Upgrade)
            {
                transform.SetParent(SiegeUp1.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player1.GetComponent<Player>().Played = true;
                Deck1.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
        }
        else if (startParent == Hand2.transform && Player2.GetComponent<Player>().IsPlaying == true && Player2.GetComponent<Player>().Played == false)
        {
            if ((a == Card.CardTipe.Unit && (x == Card.Position.S || y == Card.Position.S || z == Card.Position.S)) || a == Card.CardTipe.ClearWeather)
            {
                transform.SetParent(Siege2.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player2.GetComponent<Player>().Played = true;
                Deck2.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }
            else if (a == Card.CardTipe.Upgrade)
            {
                transform.SetParent(SiegeUp2.transform, false);
                GetComponent<CardDisplay>().InField = true;
                Player2.GetComponent<Player>().Played = true;
                Deck2.GetComponent<Draw>().CardsInHand--;
                transform.GetComponent<CardDisplay>().CastEffect();
            }

        }
    }


}
