using Google.Apis.Discovery.v1;
using Google.Apis.Discovery.v1.Data;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using System.Diagnostics;

namespace gmail_netcore_pof
{
    class Program
    {
        const string apiKey = "AIzaSyAyLBX_GK6vFFqhUOxJ__NY6e7n2rMwk38";
        const string hostEmailAddress = "me";
        static async Task Main(string[] args)
        {
            Console.WriteLine("Discovery API Sample");
            Console.WriteLine("====================");

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
            var service = await GmailAPIHelper.GetService();
            // Run the request.
            Console.WriteLine("Executing a list request...");
            var msgList = service.Users.Messages.List(hostEmailAddress);
            msgList.LabelIds = "INBOX";
            msgList.Q = "is:read";
            msgList.MaxResults = 30;
            var result = await msgList.ExecuteAsync();
            var newNextPageToken = result.NextPageToken;
            // Estimated total number of results.
            var estimated = result.ResultSizeEstimate;

            int counter = 0;
            // Display the results.
            // List of messages. Note that each message resource contains only an `id` and a `threadId`. 
            // Additional message details can be fetched using the messages.get

            var list = new List<string>();
            if (result.Messages != null)
            {
                ParallelLoopResult r = Parallel.ForEach(result.Messages, async msg =>
                                {
                                    counter++;
                                    //MESSAGE MARKS AS READ AFTER READING MESSAGE

                                    ModifyMessageRequest mods = new();
                                    mods.AddLabelIds = null;
                                    mods.RemoveLabelIds = new List<string> { "UNREAD" };
                                    service.Users.Messages.Modify(mods, hostEmailAddress, msg.Id).Execute();


                                    var Message = service.Users.Messages.Get(hostEmailAddress, msg.Id);
                                    //Console.WriteLine($"-----------------NEW MAIL:{counter}----------------------");
                                    //Console.WriteLine("Message ID:" + msg.Id);

                                    //MAKE ANOTHER REQUEST FOR THAT EMAIL ID...
                                    Message msgContent = await Message.ExecuteAsync();

                                    if (msgContent != null)
                                    {
                                        string fromAddress = string.Empty;
                                        string date = string.Empty;
                                        string subject = string.Empty;                                        
                                        string mailBody = string.Empty;
                                        string readableText = string.Empty;

                                        // fromAddress = msgContent.Payload.Headers.First(h => h.Name == "From").Value;
                                        // date = msgContent.Payload.Headers.First(h => h.Name == "Date").Value;
                                        // date = msgContent.Payload.Headers.First(h => h.Name == "Subject").Value;

                                        //LOOP THROUGH THE HEADERS AND GET THE FIELDS WE NEED (SUBJECT, MAIL)
                                        foreach (var msgParts in msgContent.Payload.Headers)
                                        {
                                            if (msgParts.Name == "From")
                                            {
                                                fromAddress = msgParts.Value;
                                            }
                                            else if (msgParts.Name == "Date")
                                            {
                                                date = msgParts.Value;
                                            }
                                            else if (msgParts.Name == "Subject")
                                            {
                                                subject = msgParts.Value;
                                                list.Add(subject);
                                            }
                                        }


                                        // mailBody = string.Empty;
                                        // if (msgContent.Payload.Parts == null && msgContent.Payload.Body != null)
                                        // {
                                        //   mailBody = msgContent.Payload.Body.Data;
                                        // }
                                        // else if (msgContent.Payload.Parts != null)
                                        // {
                                        //   mailBody = GmailAPIHelper.MsgNestedParts(msgContent.Payload.Parts);
                                        // }

                                        // //BASE64 TO READABLE TEXT--------------------------------------------------------------------------------
                                        // readableText = string.Empty;
                                        // readableText = GmailAPIHelper.Base64Decode(mailBody);

                                        //Console.WriteLine($"fromAddress: {fromAddress}");
                                        //Console.WriteLine($"date: {date}");
                                        //Console.WriteLine($"subject: {subject}");
                                        // Console.WriteLine($"mailBody: {mailBody}");
                                        // Console.WriteLine($"readableText: {readableText}");

                                    }
                                });
                Console.WriteLine("Waiting");
                Console.WriteLine("Completou ? " + r.IsCompleted);
                if (r.IsCompleted)
                {
                    Console.WriteLine("COMPLETADO");
                    foreach (var item in list)
                    {
                        Console.WriteLine(item);
                    }
                }
            }
            
            return newNextPageToken;
        }

    }
}