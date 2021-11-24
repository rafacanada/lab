class AsyncBreakfast
{
    public static async Task ExecuteAsync()
    {
        Coffee cup = PourCoffee();
        Console.WriteLine("Coffee is ready");

        var eggsTask = FryEggsAsync(2);
        var baconTask = FryBaconAsync(3);
        var toastTask = MakeToastWithButterAndJamAsync(2);

        // Initial version
        // var eggs = await eggsTask;
        // Console.WriteLine("eggs are ready");
        // var bacon = await baconTask;
        // Console.WriteLine("bacon is ready");
        // var toast = await toastTask;
        // Console.WriteLine("toast is ready");

        // The series of await statements at the end of the preceding code can be improved by using  WhenAll,
        // which returns a Task that completes when all the tasks in its argument list have completed.
        // await Task.WhenAll(eggsTask, baconTask, toastTask);
        // Console.WriteLine("Eggs are ready");
        // Console.WriteLine("Bacon is ready");
        // Console.WriteLine("Toast is ready");
        // Console.WriteLine("Breakfast is ready!");

        // Another option is to use WhenAny, which returns a Task<Task> that completes when any of its arguments completes.
        // You can await the returned task, knowing that it has already finished.
        var breakfastTasks = new List<Task> { eggsTask, baconTask, toastTask };
        while (breakfastTasks.Count > 0)
        {
            Console.WriteLine($"breakfastTasks.Count: {breakfastTasks.Count}");
            Task finishedTask = await Task.WhenAny(breakfastTasks);
            if (finishedTask == eggsTask)
            {
                Console.WriteLine("Eggs are ready");
            }
            else if (finishedTask == baconTask)
            {
                Console.WriteLine("Bacon is ready");
            }
            else if (finishedTask == toastTask)
            {
                Console.WriteLine("Toast is ready");
            }
            breakfastTasks.Remove(finishedTask);
        }

        Juice oj = PourOJ();
        Console.WriteLine("Orange juice is ready");
        Console.WriteLine("Breakfast is ready!");
    }
    
    private static Juice PourOJ()
    {
        Console.WriteLine("Pouring orange juice");
        return new Juice();
    }

    private static void ApplyJam(Toast toast) => 
        Console.WriteLine("Putting jam on the toast");

    private static void ApplyButter(Toast toast) => 
        Console.WriteLine("Putting butter on the toast");

    private static async Task<Toast> ToastBreadAsync(int slices)
    {
        for (int slice = 0; slice < slices; slice++)
        {
            Console.WriteLine("Putting a slice of bread in the toaster");
        }

        Console.WriteLine("Start toasting...");
        await Task.Delay(2000);
        // Console.WriteLine("Fire! Toast is ruined!");
        // throw new InvalidOperationException("The toaster is on fire");
        // await Task.Delay(1000);
        Console.WriteLine("Remove toast from toaster");

        return new Toast();
    }

    private static async Task<Bacon> FryBaconAsync(int slices)
    {
        Console.WriteLine($"putting {slices} slices of bacon in the pan");
        Console.WriteLine("cooking first side of bacon...");
        await Task.Delay(3000);
        for (int slice = 0; slice < slices; slice++)
        {
            Console.WriteLine("flipping a slice of bacon");
        }
        Console.WriteLine("cooking the second side of bacon...");
        await Task.Delay(3000);
        Console.WriteLine("Put bacon on plate");

        return new Bacon();
    }

    private static async Task<Egg> FryEggsAsync(int howMany)
    {
        Console.WriteLine("Warming the egg pan...");
        await Task.Delay(3000);
        Console.WriteLine($"cracking {howMany} eggs");
        Console.WriteLine("cooking the eggs ...");
        await Task.Delay(3000);
        Console.WriteLine("Put eggs on plate");

        return new Egg();
    }

    private static Coffee PourCoffee()
    {
        Console.WriteLine("Pouring coffee");
        return new Coffee();
    }

    private static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
    {
        var toast = await ToastBreadAsync(number);
        ApplyButter(toast);
        ApplyJam(toast);

        return toast;
    }
}