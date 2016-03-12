using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace discordbot.db
{
    [Table(Name = "logs")]
    public class Log
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public ulong? id { get; set; }

        [Column(Name = "severity")]
        public int severity { get; set; }

        [Column(Name = "source")]
        public string source { get; set; }

        [Column(Name = "message")]
        public string message { get; set; }

        [Column(Name = "time")]
        public long time { get; set; }
    }
}
