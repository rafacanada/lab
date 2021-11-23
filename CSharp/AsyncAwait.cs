public class AsyncAwait
{
    public async Task<int> ExecuteAsync()
    {
        var client = new HttpClient();

        Task<string> getStringTask = client.GetStringAsync("https://docs.microsoft.com/dotnet");

        DoIndependentWork();

        string contents = await getStringTask;

        return contents.Length;
    }

    private void DoIndependentWork()
    {
        Console.WriteLine("Working...");
    }
}