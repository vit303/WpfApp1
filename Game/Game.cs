using GameProject.Cards;
using GameProject.Players;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System;
using лаба3.Cards;

namespace GameProject.Game
{
    public class CardGame
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player CurrentPlayer { get; set; }
        public Player Opponent { get; set; }
        public Random Dice { get; set; } = new Random();

        public List<Card> Deck { get; set; } = new List<Card>();
        public List<string> GameLog { get; set; } = new List<string>();

        public CardGame(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;

            // Initialize the deck from JSON
            LoadCardsFromJson("D:\\cs_projects\\lab3\\лаба3\\database\\cardsDatabase.json");

            
            var diceRoll1 = Dice.Next(1, 7);
            var diceRoll2 = Dice.Next(1, 7);
            if (diceRoll1 >= diceRoll2)
            {
                CurrentPlayer = player1;
                Opponent = player2;
            }
            else
            {
                CurrentPlayer = player2;
                Opponent = player1;
            }

            GameLog.Add($"Кубик: Игрок {Player1.Name} - {diceRoll1}, Игрок {Player2.Name} - {diceRoll2}. Ходит {CurrentPlayer.Name}.");
        }

        private void LoadCardsFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл не найден: {filePath}");
            }

            var json = File.ReadAllText(filePath);
            var cardData = JsonConvert.DeserializeObject<List<CardData>>(json);

            foreach (var card in cardData)
            {
                Card cardInstance = null;
                switch (card.CardType.ToLower())
                {
                    case "heal":
                        cardInstance = new HealCard(card.Id, card.CardName, card.CardDamage, card.Description, card.ImageLink);
                        break;
                    case "improve":
                        cardInstance = new ImproveCard(card.Id, card.CardName, card.CardDamage, card.Description, card.ImageLink);
                        break;
                    case "damage":
                        cardInstance = new DamageCard(card.Id, card.CardName, card.CardDamage, card.Description, card.ImageLink);
                        break;
                    default:
                        throw new InvalidOperationException($"Неизвестный тип карты: {card.CardType}");
                }

                Deck.Add(cardInstance);
            }
        }

        public void NextTurn()
        {
            CurrentPlayer.DrawCard(Deck);
            GameLog.Add($"Игрок {CurrentPlayer.Name} тянет карту.");

            if (Opponent.Health <= 0)
            {
                GameLog.Add($"Игрок {CurrentPlayer.Name} победил!");
                return;
            }

            // Swap players
            var temp = CurrentPlayer;
            CurrentPlayer = Opponent;
            Opponent = temp;
        }
    }

    public class CardData
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public int CardDamage { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public string CardType { get; set; }
    }
}
