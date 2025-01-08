using GameProject.Cards;
using System.Collections.Generic;
using System;

namespace GameProject.Players
{
    public class Player
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Defense { get; set; }
    
        public List<Card> Hand { get; set; } = new List<Card>();
        public List<string> Log { get; set; } = new List<string>();

        public Player(string name)
        {
            Name = name;
            Health = 100;
            Defense = 0;
            
        }

        public void DrawCard(List<Card> deck)
        {
            
            if (Hand.Count >= 3) return;

            if (deck.Count > 0)
            {
                Random rand = new Random();
                int index = rand.Next(deck.Count);
                Hand.Add(deck[index]);
                deck.RemoveAt(index);
            }
        }
        public void InitializeRandomHand(List<Card> deck)
        {
            Random random = new Random();
            Hand.Clear();
            for (int i = 0; i < 3; i++) // Give each player 3 random cards
            {
                int cardIndex = random.Next(deck.Count);
                Hand.Add(deck[cardIndex]);
                deck.RemoveAt(cardIndex); // Remove the card from the deck to avoid duplicates
            }
        }
        // Check if the player can play the card
        public bool CanPlayCard(Card card)
        {
            
            return true; 
        }

        public void PlayCard(Card card, Player opponent)
        {
            if (CanPlayCard(card))
            {
                card.Play(this, opponent);
                Hand.Remove(card);
                Log.Add($"{Name} played {card.Name}.");
            }
            else
            {
                Log.Add($"Недостаточно маны для использования карты {card.Name}.");
            }
        }

        
        public void Heal(int amount)
        {
            Health += amount;
            Log.Add($"{Name} healed for {amount}. Current health: {Health}");
        }

        public void Improve(int amount)
        {
            Defense += amount; 
            Log.Add($"{Name} improved defense by {amount}. Current defense: {Defense}");
        }

        public void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(damage - Defense, 0); // Damage minus defense
            Health -= actualDamage;
            Log.Add($"{Name} took {actualDamage} damage. Current health: {Health}");
        }
    }
}

