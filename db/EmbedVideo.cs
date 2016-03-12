using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace discordbot.db
{
    [Table(Name = "embedVideos")]
    public class EmbedVideo
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public ulong? id { get; set; }

        [Column(Name = "embedID")]
        public ulong embedID { get; set; }

        [Column(Name = "url")]
        public string url { get; set; }

        [Column(Name = "proxyURL")]
        public string proxyURL { get; set; }

        [Column(Name = "width")]
        public int? width { get; set; }

        [Column(Name = "height")]
        public int? height { get; set; }
    }
}
