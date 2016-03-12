using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace discordbot.db
{
    [Table(Name = "messages")]
    public class Message
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public ulong id { get; set; }

        [Column(Name = "channelID")]
        public ulong channelID { get; set; }

        [Column(Name = "userID")]
        public ulong userID { get; set; }

        [Column(Name = "message")]
        public string message { get; set; }

        [Column(Name = "time")]
        public long time { get; set; }
    }
}
