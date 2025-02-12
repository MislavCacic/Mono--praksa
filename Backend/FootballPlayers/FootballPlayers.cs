using System;

public class FootballPlayer : Player
{
    public FootballPlayer() { }

    public FootballPlayer(string name, int  goals) { FirstName = name; GoalsPerGame = goals; Team = "N/A"; }

    public FootballPlayer(int id, string name, int goals, string team) { Team = team; FirstName = name; ID = id; GoalsPerGame = goals; }

    public override string DisplayData()
    {
        return  $"This player {this.FirstName} - {this.Team}";
    }
}