public abstract class Player : IPlayer
{
    public string? FirstName { get; set; }
    public int ID { get; set; }
    public string? Team { get; set; }

    public int? GoalsPerGame { get; set; } = 0;

    public abstract string DisplayData();
}