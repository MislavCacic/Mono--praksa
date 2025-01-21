using System.Linq;
using System.Numerics;

List<FootballPlayer> players = new List<FootballPlayer>();

bool running = true;
while (running)
{
    Console.WriteLine("Odaberite opciju:");
    Console.WriteLine("1. Vidi sve igrače");
    Console.WriteLine("2. Dodaj novog igrača");
    Console.WriteLine("3. Uredi broj golova");
    Console.WriteLine("4. Izlaz");

    Console.Write("Vaš odabir: ");

    string choice = Console.ReadLine() ;

    switch (choice)
    {
        case "1":
            // Prikaz svih igrača
            if (players.Count == 0)
            {
                Console.WriteLine("\nNema igrača u sustavu.\n");
            }
            else
            {
                Console.WriteLine("\nPopis igrača:");
                foreach (var player in players)
                {
                    Console.WriteLine($"{player.FirstName}, {player.Team}, {player.ID}");
                }
                Console.WriteLine();
            }

            break;

        case "2":

            Console.Write("Unesite ID igrača: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Unesite ime igrača: ");
            string name = Console.ReadLine() ?? "Nepoznato";

            Console.Write("Unesite tim igrača: ");
            string team = Console.ReadLine() ?? "N/A";


            Console.Write("Unesite broj golova po utakmici: ");
            int goals = int.Parse(Console.ReadLine() ?? "0");

            FootballPlayer novi = new FootballPlayer(id, name, goals, team){GoalsPerGame = goals};
            players.Add(novi);

            Console.WriteLine("\nIgrač je uspješno dodan!");

            Console.WriteLine(novi.DisplayData());

            break;

        case "3":
            Console.WriteLine("Unesite ID igrača kojem želite ažurirati broj golova:");
            id = int.Parse(Console.ReadLine());

            Console.Write("Unesite broj poena po utakmici: ");
            goals = int.Parse(Console.ReadLine() ?? "0");
            FootballPlayer? currentPlayer = players.FirstOrDefault(p => p.ID == id);

            if (currentPlayer == null)
            {
                Console.WriteLine("Error");
                break;
            }
            currentPlayer.GoalsPerGame = goals;


            Console.WriteLine(currentPlayer.DisplayData());

            break;

        case "4":
            running = false;
            break;
    }
}

Console.ReadLine();