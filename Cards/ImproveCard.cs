using GameProject.Cards;
using GameProject.Players;


namespace лаба3.Cards
{
    public class ImproveCard : Card
    {
        public ImproveCard(int id, string name, int cardDamage, string description, string imageLink)
        {
            Id = id;
            Name = name;
            CardDamage = cardDamage; 
            Description = description;
            ImageLink = imageLink;
            CardType = "improve";
        }

        public override void Play(Player player, Player opponent)
        {
            
            player.Improve(CardDamage);
        }
    }
}
