using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace discordbot.db
{
    [Table(Name = "servers")]
    public class Server
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public long id { get; set; }

        [Column(Name = "name")]
        public string name { get; set; }

        [Column(Name = "owner")]
        public ulong owner { get; set; }

        [Column(Name = "region")]
        public ulong region { get; set; }
    }
}
