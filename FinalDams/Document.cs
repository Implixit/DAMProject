using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDams
{
    public class Document
    {
        public int ID { get; set; }
        public DateTime UploadDate { get; set; }
        public int UserID { get; set; }
        public AssetType AssetType { get; set; }
        public string Path { get; set; }
        public List<Data> MetaDataValues { get; set; }
    }
}
