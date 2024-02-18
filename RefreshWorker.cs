using EFMemoryExample.Data;

namespace EFMemoryExample
{
    public class RefreshWorker : BackgroundService
    {
        private readonly IServiceProvider _provider;
        public RefreshWorker(IServiceProvider provider)
        {
            _provider = provider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //I am doing some htpps request here but i'll generate some for test
                    var testResponseData = new List<Item>();
                    for (int i = 0; i < 20000; i++)
                    {
                        var item = new Item()
                        {
                            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam",
                            Name = $"{i} Name",
                            Type = "postItem",
                        };
                        testResponseData.Add(item);
                    }
                    //All new object from http request should be added to Db
                    using var scope = _provider.CreateScope();
                    using var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    db.Items.AddRange(testResponseData);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Error while refreshing Items: " + ex);
                }

                //It is supposed to be 8 hours but for example i left 5 sec
                await Task.Delay(5000, stoppingToken);
                GC.Collect();
            }
        }
    }
}
