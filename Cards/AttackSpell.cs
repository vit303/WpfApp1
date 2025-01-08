using GameProject.Players;


namespace GameProject.Cards
{
    public class DamageCard : Card
    {
        public DamageCard(int id, string name, int cardDamage, string description, string imageLink)
        {
            Id = id;
            Name = name;
            CardDamage = cardDamage; 
            Description = description;
            ImageLink = imageLink;
            CardType = "damage";
        }

        public override void Play(Player player, Player opponent)
        {
            // Calculate the new health after damage
            int newHealth = opponent.Health - CardDamage;

            
            opponent.Health = newHealth < 0 ? 0 : newHealth;

            
        }
    }
}
