using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace craigs.Function
{
    public class MagicEightBall
    {
        public record Answer(string Text, string AnswerType, string Color);
        private static List<Answer> Answers = new(){
            new Answer("It is certain", "affirmative", "#00ff00"),
            new Answer("It is decidedly so", "affirmative", "#00ff00"),
            new Answer("Without a doubt", "affirmative", "#00ff00"),
            new Answer("Yes, definitely", "affirmative", "#00ff00"),
            new Answer("You may rely on it", "affirmative", "#00ff00"),
            new Answer("As I see it, yes", "affirmative", "#00ff00"),
            new Answer("Most likely", "affirmative", "#00ff00"),
            new Answer("Outlook good", "affirmative", "#00ff00"),
            new Answer("Yes", "affirmative", "#00ff00"),
            new Answer("Signs point to yes", "affirmative", "#00ff00"),
            new Answer("Reply hazy try again", "non-committal", "#ffff00"),
            new Answer("Ask again later", "non-committal", "#ffff00"),
            new Answer("Better not tell you now", "non-committal", "#ffff00"),
            new Answer("Cannot predict now", "non-committal", "#ffff00"),
            new Answer("Concentrate and ask again", "non-committal", "#ffff00"),
            new Answer("Don't count on it", "negative", "#ff0000"),
            new Answer("My reply is no", "negative", "#ff0000"),
            new Answer("My sources say no", "negative", "#ff0000"),
            new Answer("Outlook not so good", "negative", "#ff0000"),
            new Answer("Very doubtful", "negative", "#ff0000")
        };
        private static readonly Random r = new();

        private readonly ILogger _logger;

        public MagicEightBall(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MagicEightBall>();
        }

        [Function("MagicEightBall")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            var answer = Answers[r.Next(0, Answers.Count)];
            response.WriteString(answer.Text);
            //response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
