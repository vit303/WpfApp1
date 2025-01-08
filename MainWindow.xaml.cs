using GameProject.Cards;
using GameProject.Game;
using GameProject.Players;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace CardGameWPF
{
    public partial class MainWindow : Window
    {
        private CardGame game;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
            UpdateUI();
        }

        private void InitializeGame()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");
            game = new CardGame(player1, player2);

            player1.InitializeRandomHand(game.Deck);
            player2.InitializeRandomHand(game.Deck);
        }

        private void UpdateUI()
        {
            CurrentPlayerName.Text = $"Current Player: {game.CurrentPlayer.Name}";
            OpponentPlayerName.Text = $"Opponent: {game.Opponent.Name}";
            CurrentPlayerHealth.Text = $"Health: {game.CurrentPlayer.Health}";
            OpponentPlayerHealth.Text = $"Health: {game.Opponent.Health}";

            PlayerCards.ItemsSource = game.CurrentPlayer.Hand; // Привязка карточек игрока
            GameLog.Text = string.Join(Environment.NewLine, game.GameLog);
        }

        private void PlayCardButton_Click(object sender, RoutedEventArgs e)
        {
            if (PlayerCards.SelectedItem is Card selectedCard)
            {
                game.CurrentPlayer.PlayCard(selectedCard, game.Opponent);
                UpdateUI();

                if (game.Opponent.Health <= 0)
                {
                    MessageBox.Show($"{game.CurrentPlayer.Name} wins!");
                    return;
                }

                game.NextTurn();
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Please select a card to play.");
            }
        }

        private void SkipTurnButton_Click(object sender, RoutedEventArgs e)
        {
            game.NextTurn();
            UpdateUI();
        }
    }
}
