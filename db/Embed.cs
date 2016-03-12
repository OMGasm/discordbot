using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace discordbot.db
{
    [Table(Name = "embeds")]
    public class Embed
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public ulong? id { get; set; }

        [Column(Name = "messageID")]
        public ulong messageID { get; set; }

        [Column(Name = "url")]
        public string url { get; set; }

        [Column(Name = "title")]
        public string title { get; set; }

        [Column(Name = "description")]
        public string description { get; set; }

        [Column(Name = "thumbnailProxyURL")]
        public string thumbnailProxyURL { get; set; }

        [Column(Name = "thumbnailURL")]
        public string thumbnailURL { get; set; }

        [Column(Name = "thumbnailWidth")]
        public int? thumbnailWidth { get; set; }

        [Column(Name = "thumbnailHeight")]
        public int? thumbnailHeight { get; set; }

        [Column(Name = "providerName")]
        public string providerName { get; set; }

        [Column(Name = "providerURL")]
        public string providerURL { get; set; }
    }
}
