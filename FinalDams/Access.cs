using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalDams
{
    public class Access
    {
        public int ID { get; set; }
        public int AccessLevel { get; set; }
        public string usertype { get; set; }
        public List<AssetType> AssetTypes { get; set; }
        public List<User> Users { get; set; }
    }
}
