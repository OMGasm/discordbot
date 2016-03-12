using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace discordbot.db
{
    [Table(Name = "attachments")]
    public class Attachment
    {
        [Column(Name = "id", IsPrimaryKey = true)]
        public ulong id { get; set; }

        [Column(Name = "attachmentID")]
        public string attachmentID { get; set; }

        [Column(Name = "messageID")]
        public ulong messageID { get; set; }

        [Column(Name = "filename")]
        public string filename { get; set; }

        [Column(Name = "size")]
        public int size { get; set; }

        [Column(Name = "proxyURL")]
        public string proxyURL { get; set; }

        [Column(Name = "url")]
        public string url { get; set; }
    }
}
