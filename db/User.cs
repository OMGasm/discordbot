using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace discordbot.db
{
    [Table(Name = "users")]
    public class User
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public long id { get; set; }

        [Column(Name = "name")]
        public string name { get; set; }

        [Column(Name = "discriminator")]
        public int discriminator { get; set; }

        [Column(Name = "joinedAt")]
        public long joinedAt { get; set; }

        [Column(Name = "mention")]
        public string mention { get; set; }
    }
}
