using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;

namespace discordbot.db
{
    [Table(Name = "attachmentImage")]
    public class AttachmentImage
    {
        [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true)]
        public ulong id { get; set; }

        [Column(Name = "attachmentID")]
        public ulong attachmentID { get; set; }

        [Column(Name = "imageWidth")]
        public int imageWidth { get; set; }

        [Column(Name = "ImageHeight")]
        public int imageHeight { get; set; }
    }
}
