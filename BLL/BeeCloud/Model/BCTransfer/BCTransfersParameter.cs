using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCTransfersParameter
    {
        public string channel { get; set; }
        public string batchNo { get; set; }
        public string accountName { get; set; }
        public List<BCTransferData> transfersData { get; set; }
    }
}
