using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDams
{
    public class AssetType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Access ACL { get; set; }

        //navigation properties
        public List<Document> Documents { get; set; }
        public List<Meta> MetaDataTypes { get; set; }
    }
}
