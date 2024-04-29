using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GwentEngine
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public string CardName;
        public int Attack;
        public Faction CardFaction;
        public CardTipe Cardtipe;
        public Position Position1;
        public Position Position2;
        public Position Position3;
        public Rank CardRank;
        public Effect ID;
        public string Description;
        public string Positions;
        public Sprite CardFront;

        

        public enum Faction
        {
            VampirosEnlaHabana,
            ElpidioValdez,
            otras
        }
        public enum CardTipe
        {
            Weather,
            ClearWeather,
            Lure,
            Upgrade,
            Unit
        }
        public enum Position
        {
            M,
            R,
            S,
            U,
            W

        }
        public enum Rank
        {
            Special,
            Gold,
            Silver
        }
        public enum Effect
        {
            Upgrade,
            Weather,
            ClearWeather,
            Lure,
            DestroyCard,
            CardUp,
            CardDown,
            DrawCard,
            NoEffect
        }
    }
}
