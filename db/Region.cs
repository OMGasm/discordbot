using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace discordbot.db
{
    [Table(Name = "regions")]
    public class Region
    {
        [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public ulong id { get; set; }

        [Column(Name = "regionID")]
        public string regionID { get; set; }

        [Column(Name = "name")]
        public string name { get; set; }

        [Column(Name = "hostname")]
        public string hostname { get; set; }

        [Column(Name = "port")]
        public int port { get; set; }

        [Column(Name = "vip")]
        public int vip { get; set; }
    }
}
