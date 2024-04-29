using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GwentEngine
{
    [CreateAssetMenu(fileName = "New LeaderCard", menuName = "LeaderCard")]
    public class Leader : ScriptableObject
    {
        public string CardName;
        public Card.Faction CardFaction;
        public CardTipe Cardtipe = CardTipe.Leader;
        public Card.Position Position1;
        public Card.Position Position2;
        public Card.Position Position3;
        public Effect ID;
        public string Description;
        public string Positions;
        public Sprite CardFront;

        public enum Effect
        {
            Upgrade,
            Weather
        }
            
        public enum CardTipe
        {
            Leader
        }
            
            
    }
}