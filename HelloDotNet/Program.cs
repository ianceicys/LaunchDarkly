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
            User user = User.WithKey("bob@example.com")
                .AndFirstName("Bob")
                .AndLastName("Loblaw")
                .AndCustomAttribute("groups", "beta_testers")
                .AndCustomAttribute("favoritenumber", 42);

            //Custom configuration object:
            Configuration config = LaunchDarkly.Client.Configuration.Default()
            .WithApiKey("sdk-51cd9173-cc65-4345-af2e-0c31c7d4afe9")
            .WithEventQueueFrequency(TimeSpan.FromSeconds(2));
            LdClient ldClient = new LdClient(config);

            // TODO: Enter the key for your feature flag key here
            var value = client.Toggle("new-wizbang-feature", user, false);

            if (value)
            {
                ldClient.Track("new-wizbang-feature", user,"Feature Enabled");
                Console.WriteLine("Showing feature for user " + user.Key);
            }
            else
            {
                Console.WriteLine("Not showing feature for user " + user.Key);
            }
            client.Flush();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
