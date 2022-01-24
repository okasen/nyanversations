using System;
using System.Threading.Tasks;

using static Tensorflow.Binding;
using static Tensorflow.KerasApi;
using Tensorflow;
using Tensorflow.NumPy;

using Tweetinvi;

namespace TweetHandler
{
    
    class SendTweet
    {
        
        static async Task Main(string[] args)
        {
            var apiKey = Environment.GetEnvironmentVariable("TWITTERAPIKEY");
            var secretKey = Environment.GetEnvironmentVariable("TWITTERSECRETKEY");
            var accessToken = Environment.GetEnvironmentVariable("TWITTERACCESSTOKEN");
            var secretToken = Environment.GetEnvironmentVariable("TWITTERSECRETTOKEN");

            Console.WriteLine(apiKey + secretKey + accessToken + secretToken);
            var userClient = new TwitterClient(apiKey, secretKey, accessToken, secretToken);

            string tweetText = TweetView.ComposeTweet.ConcatenateMessage();
    
            if(TweetView.CheckTweet.ShouldTweet(tweetText) && TweetView.CheckTweet.LengthAllowed(tweetText))
            {
                var tweet = await userClient.Tweets.PublishTweetAsync(tweetText);
                Console.WriteLine("You published the tweet : " + tweetText);
            }
            else
            {
                Console.WriteLine("You published nothing.");
            }
           
        }
    }
}

namespace TweetView
{
    class ComposeTweet
    {
        public static string ConcatenateMessage()
        {
            Console.WriteLine("Enter the text you'd like to tweet:");
            var tweetContent = Console.ReadLine();
            return "This is another bot tweet! " + tweetContent;
        }
    }

    class CheckTweet
    {
        public static bool ShouldTweet(string tweetText)
        {
            while(true){
                Console.WriteLine("Do you really want to tweet this: " + tweetText + " y/n");
                var answer = Console.ReadLine();
                if(answer == "y")
                {
                    return true;
                }         
                else if(answer == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("please only enter y or n");
                }
            }
        }

        public static bool LengthAllowed(string tweetText)
        {
            Console.WriteLine(tweetText.Length);
            if(tweetText.Length > 280)
            {
                Console.WriteLine("You can't tweet that, it's too long!");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}