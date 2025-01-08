using GameProject.Players;

namespace GameProject.Cards
{
    public abstract class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CardDamage { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }
        public string CardType { get; set; }

        public abstract void Play(Player player, Player opponent);
    }

}
