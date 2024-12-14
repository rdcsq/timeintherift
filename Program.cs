using DiscordRPC;
using DiscordRPC.Logging;

const string leagueClientId = "401518684763586560";

var quitEvent = new ManualResetEvent(false);

if (args.Length == 0)
{
    Console.WriteLine("You have to provide the name of a champion as the first argument.");
    return;
}

var client = new DiscordRpcClient(leagueClientId)
{
    Logger = new ConsoleLogger() { Level = LogLevel.Warning }
};

client.OnReady += (_, _) => { Console.WriteLine("Ready"); };

client.Initialize();

client.SetPresence(new RichPresence()
{
    Details = "Summoner's Rift",
    State = "In Game",
    Timestamps = new Timestamps()
    {
        Start = DateTime.Now.ToUniversalTime()
            .AddSeconds(-new Random().Next(18000, 72000)) // between 5 and 20 hours
    },
    Assets = new Assets()
    {
        LargeImageKey = $"champ_{args[0]}"
    }
});

Console.CancelKeyPress += (_, e) =>
{
    quitEvent.Set();
    e.Cancel = true;
};
quitEvent.WaitOne();