using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace discordbot.db
{
    [Table(Name = "channels")]
    public class Channel
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public long id { get; set; }

        [Column(Name = "name")]
        public string name { get; set; }

        [Column(Name = "server")]
        public ulong server { get; set; }

        [Column(Name = "private")]
        public int Private { get; set; }

        [Column(Name = "mention")]
        public string mention { get; set; }

        [Column(Name = "topic")]
        public string topic { get; set; }

        [Column(Name = "type")]
        public string type { get; set; }
    }
}
