using Google.Apis.Discovery.v1;
using Google.Apis.Discovery.v1.Data;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using System.Diagnostics;

namespace gmail_netcore_pof
{
    class Program
    {
        const string apiKey = "AIzaSyAyLBX_GK6vFFqhUOxJ__NY6e7n2rMwk38";
        const string hostEmailAddress = "me";
        public static GmailService service = new GmailService();
        public static List<String> list = new List<string>();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Discovery API Sample");
            Console.WriteLine("====================");
            service = await GmailAPIHelper.GetService();
            var timer = new Stopwatch();
            timer.Start();

            try
            {
                var instance = new Program();
                //int pageCounter = 1;
                //Console.Write($"MESSAGES PAGE: {pageCounter} --------------------------------");
                //string nextPageToken = await instance.GetMessages();
                //while (nextPageToken != null)
                //{
                //    pageCounter++;
                //Console.Write($"MESSAGES PAGE: {pageCounter} --------------------------------");
                await instance.GetMessages("");
                //}
                timer.Stop();
                TimeSpan timeTaken = timer.Elapsed;
                Console.WriteLine("TEMPO TOTAL !!!: " + timeTaken.ToString(@"m\:ss\.fff"));
                Console.WriteLine("TEMPO TOTAL !!!: " + timeTaken.ToString(@"m\:ss\.fff"));
                Console.WriteLine("TEMPO TOTAL !!!: " + timeTaken.ToString(@"m\:ss\.fff"));
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
            }
            Console.WriteLine("Press any key to continue...");
            //Console.ReadKey();
        }

        private async Task GetAPIS()
        {
            // Create the service.
            var service = new DiscoveryService(new BaseClientService.Initializer
            {
                ApplicationName = "Discovery Sample",
                ApiKey = apiKey,
            });

            // Run the request.
            Console.WriteLine("Executing a list request...");
            var result = await service.Apis.List().ExecuteAsync();

            // Display the results.
            if (result.Items != null)
            {
                foreach (DirectoryList.ItemsData api in result.Items)
                {
                    Console.WriteLine(api.Id + " - " + api.Title);
                }
            }
        }
 
        private async Task<string> GetMessages(string? nextPageToken = null)
        {
            // Run the request.
            Console.WriteLine("Executing a list request...");
            var msgList = service.Users.Messages.List(hostEmailAddress);
            msgList.LabelIds = "INBOX";
            msgList.Q = "is:read";
            msgList.MaxResults = 50;
            var result = await msgList.ExecuteAsync();
            var newNextPageToken = result.NextPageToken;
            // Estimated total number of results.
            var estimated = result.ResultSizeEstimate;

            // Display the results.
            // List of messages. Note that each message resource contains only an `id` and a `threadId`. 
            // Additional message details can be fetched using the messages.get
            
            var tasksList = new List<Task>(result.Messages.Count);
            if (result.Messages != null)
            {

                //ThreadPool.QueueUserWorkItem()

                 foreach (var msg in result.Messages)
                 {
                    var task = Task.Run(async () =>
                    {
                        Console.WriteLine("Application thread ID: {0}",
                                                System.Threading.Thread.CurrentThread.ManagedThreadId);
                        try
                        {
                            Message m = (Message)msg;
                            var Message = service.Users.Messages.Get(hostEmailAddress, m.Id);
                            Message msgContent = await Message.ExecuteAsync();
                            string subject = msgContent.Payload.Headers.FirstOrDefault(h => h.Name == "Subject").Value;
                            list.Add(subject);
                        }
                        catch (Google.GoogleApiException e)
                        {                            
                            if (e.HttpStatusCode == System.Net.HttpStatusCode.TooManyRequests)
                            {
                                Console.WriteLine("Ooppss");
                            }
                        }
                        await Task.Delay(100);
                    });
                    tasksList.Add(task);
                 }

                await Task.WhenAll(tasksList.ToArray());

                

               /* foreach (var msg in result.Messages)
                {
                    var Message = service.Users.Messages.Get(hostEmailAddress, msg.Id);
                    Message msgContent = await Message.ExecuteAsync();
                    string subject = msgContent.Payload.Headers.FirstOrDefault(h => h.Name == "Subject").Value;
                    Console.WriteLine(subject);
                    list.Add(subject);

                }*/

                Console.WriteLine(list.Count);
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
            }

            return newNextPageToken;
        }
    }
}