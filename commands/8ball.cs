using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace discordbot.commands
{
    class _8ball : CommandBase
    {
        static string[] answers = { "It is certain", "It is decidedly so",
            "Without a doubt", "Yes, definitely",
            "You may rely on it", "As I see it, yes",
            "Most likely", "Outlook good",
            "Yes", "Signs point to yes",
            "Reply hazy try again", "Ask again later",
            "Better not tell you now", "Cannot predict now",
            "Concentrate and ask again", "Don't count on it",
            "My reply is no", "My sources say no",
            "Outlook not so good", "Very doubtful" };
        static Random r = new Random();

        public override async Task action(CommandEventArgs e)
        {
            await e.Channel.SendMessage(answers[r.Next(answers.Length - 1)]);
        }

        public _8ball() : base("8ball",
            "magic 8 ball",
            new string[] { "does", "is", "will", "are" },
            new KeyValuePair<string, ParameterType>[] { new KeyValuePair<string, ParameterType>("msg", ParameterType.Unparsed) })
        { }
    }
}
