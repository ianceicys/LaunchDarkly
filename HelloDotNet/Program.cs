using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LaunchDarkly.Client;

namespace HelloDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Enter your LaunchDarkly API key here
            LdClient client = new LdClient("sdk-51cd9173-cc65-4345-af2e-0c31c7d4afe9");
            //User user = User.WithKey("bob@example.com")
            //    .AndFirstName("Bob")
            //    .AndLastName("Loblaw")
            //    .AndCustomAttribute("groups", "beta_testers")
            //    .AndCustomAttribute("favoritenumber", 42);

            //Random Number
            Random favoritenumber = new Random();

            //List of Users with Custom Keys
            var users = new List<User>();
            users.Add(User.WithKey("bob@example.com").AndFirstName("Bob").AndLastName("Loblaw").AndCustomAttribute("groups", "beta_testers").AndCustomAttribute("favoritenumber", favoritenumber.Next(39, 43)));
            users.Add(User.WithKey("tim@example.com").AndFirstName("Tim").AndLastName("Doe").AndCustomAttribute("groups", "beta_testers").AndCustomAttribute("favoritenumber", favoritenumber.Next(39, 43)));
            users.Add(User.WithKey("steve@example.com").AndFirstName("Steve").AndLastName("Davis").AndCustomAttribute("groups", "beta_testers").AndCustomAttribute("favoritenumber", favoritenumber.Next(39, 43)));
            users.Add(User.WithKey("mike@example.com").AndFirstName("Mike").AndLastName("Rogers").AndCustomAttribute("groups", "beta_testers").AndCustomAttribute("favoritenumber", favoritenumber.Next(39, 43)));
            users.Add(User.WithKey("Alfred@example.com").AndFirstName("Alfred").AndLastName("Brown").AndCustomAttribute("groups", "beta_testers").AndCustomAttribute("favoritenumber", favoritenumber.Next(39, 43)));
            users.Add(User.WithKey("John@example.com").AndFirstName("John").AndLastName("Moore").AndCustomAttribute("groups", "beta_testers").AndCustomAttribute("favoritenumber", favoritenumber.Next(39, 43)));
            users.Add(User.WithKey("David@example.com").AndFirstName("David").AndLastName("Harris").AndCustomAttribute("groups", "beta_testers").AndCustomAttribute("favoritenumber", favoritenumber.Next(39, 43)));
            users.Add(User.WithKey("Brian@example.com").AndFirstName("Brian").AndLastName("Lewis").AndCustomAttribute("groups", "beta_testers").AndCustomAttribute("favoritenumber", favoritenumber.Next(39, 43)));
            users.Add(User.WithKey("Ralph@example.com").AndFirstName("Ralph").AndLastName("Walker").AndCustomAttribute("groups", "beta_testers").AndCustomAttribute("favoritenumber", favoritenumber.Next(39, 43)));
            users.Add(User.WithKey("Mark@example.com").AndFirstName("Mark").AndLastName("Baker").AndCustomAttribute("groups", "beta_testers").AndCustomAttribute("favoritenumber", favoritenumber.Next(39, 43)));
            
            //Custom configuration object:
            Configuration config = LaunchDarkly.Client.Configuration.Default()
            .WithApiKey("sdk-51cd9173-cc65-4345-af2e-0c31c7d4afe9")
            .WithEventQueueFrequency(TimeSpan.FromSeconds(2));
            LdClient ldClient = new LdClient(config);

            //Loop through each user and check feature flag toggle and tracking
            int userswithflag = 0;
            int userswithoutflag = 0;
            foreach (var user in users)
            {

                var value = client.Toggle("new-wizbang-feature", user, false);

                if (value)
                {
                    userswithflag++;
                    ldClient.Track("new-wizbang-feature", user, "Feature Enabled");
                    Console.WriteLine("Showing feature for user " + user.Key);
                }
                else
                {
                    userswithoutflag++;
                    Console.WriteLine("NOT showing feature for user " + user.Key);
                }
            }
            client.Flush();
            Console.WriteLine(userswithflag+" Users Shown Flag");
            Console.WriteLine(userswithoutflag + " Users NOT Shown Flag");

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
