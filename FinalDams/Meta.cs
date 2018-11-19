using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDams
{
    public class Meta
    {
        public int ID { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public List<AssetType> AssetTypes { get; set; }
        public List<Data> Data { get; set; }
    }
}
