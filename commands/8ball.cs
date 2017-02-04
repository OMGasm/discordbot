using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace Bot.commands
{
    public class _8ball : ModuleBase
    {
        string[] answers = {
            "It is certain",
            "It is decidedly so",
            "Without a doubt",
            "Yes, definitely",
            "You may rely on it",
            "As I see it, yes",
            "Most likely",
            "Outlook good",
            "Yes",
            "Signs point to yes",
            "Reply hazy try again",
            "Ask again later",
            "Better not tell you now",
            "Cannot predict now",
            "Concentrate and ask again",
            "Don't count on it",
            "My reply is no",
            "My sources say no",
            "Outlook not so good",
            "Very doubtful" };
        Random r = new Random();

        [Command("8ball"), Summary("Magic 8 ball")]
        [Alias("does", "did", "is", "are", "was", "will", "am")]
        public async Task action([Remainder] string msg)
        {
            await ReplyAsync(answers[r.Next(answers.Length - 1)]);
        }
    }
}
