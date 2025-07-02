using System.Diagnostics;

namespace ProgrammazioneAsincrona;

internal class Program
{
    static readonly Stopwatch timer = new();
    // corrispettivo async (asincrono, cioè che va aspettato perchè richiede del tempo)
    // di un metodo void (che non ritorna risultati)
    public async Task FaiQualcosaAsync()
    {

    }

    // corrispettivo async
    // di un metodo che torna un valore
    public async Task<int> FaiQualcosaETornaUnValoreAsync()
    {
        return 1;
    }

    // ESEMPIO PRATICO

    // scenario sync
    // blocca il codice
    //static void Main(string[] args)
    //{
    //    timer.Start();
    //    Console.WriteLine($"Inizio programma: {timer.Elapsed}");

    //    Task1();
    //    Task2();

    //    Console.WriteLine($"Fine programma: {timer.Elapsed}");
    //    timer.Stop();
    //}

    // scenario async/await asincrono
    // non blocca il codice ma aspetta
    //static async Task Main(string[] args)
    //{
    //    timer.Start();
    //    Console.WriteLine($"Inizio programma: {timer.Elapsed}");

    //    await Task1Async();
    //    await Task2Async();

    //    Console.WriteLine($"Fine programma: {timer.Elapsed}");
    //    timer.Stop();
    //}

    // scenario async/await asincrono
    // non blocca il codice e non aspetta
    static async Task Main(string[] args)
    {
        timer.Start();
        Console.WriteLine($"Inizio programma: {timer.Elapsed}");

        Task t1 = Task1Async();
        Task t2 = Task2Async();

        await Task.WhenAll(t1, t2);

        Console.WriteLine($"Fine programma: {timer.Elapsed}");
        timer.Stop();
    }

    private static void Task1()
    {
        Console.WriteLine($"Task 1 Avviato: {timer.Elapsed.TotalSeconds}");
        Thread.Sleep(2000);
        Console.WriteLine($"Task 1 Completato: {timer.Elapsed.TotalSeconds}");
    }

    private static void Task2()
    {
        Console.WriteLine($"Task 2 Avviato: {timer.Elapsed.TotalSeconds}");
        Thread.Sleep(3000);
        Console.WriteLine($"Task 2 Completato: {timer.Elapsed.TotalSeconds}");
    }

    private static async Task Task1Async()
    {
        Console.WriteLine($"Async Task 1 Avviato: {timer.Elapsed.TotalSeconds}");
        await Task.Delay(4000);
        Console.WriteLine($"Async Task 1 Completato: {timer.Elapsed.TotalSeconds}");
    }

    private static async Task Task2Async()
    {
        Console.WriteLine($"Async Task 2 Avviato: {timer.Elapsed.TotalSeconds}");
        await Task.Delay(8000);
        Console.WriteLine($"Async Task 2 Completato: {timer.Elapsed.TotalSeconds}");
    }
}
