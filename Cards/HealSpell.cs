using GameProject.Players;


namespace GameProject.Cards
{
    public class HealCard : Card
    {
        public HealCard(int id, string name, int cardDamage, string description, string imageLink)
        {
            Id = id;
            Name = name;
            CardDamage = cardDamage; 
            Description = description;
            ImageLink = imageLink;
            CardType = "heal";
        }

        public override void Play(Player player, Player opponent)
        {
            
            player.Heal(CardDamage);
        }
    }

}
