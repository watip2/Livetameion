using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.BridgePay.Models
{
    /*
    Result = 0 approved
    Result = 24 invalid expiry date
    Result = 110 duplicate transaction
    */
    public class TransactionResponse
    {
        public int Result { get; set; }
        public string RespMSG { get; set; }
        public string ExtData { get; set; }
        public string Message { get; set; }
        public string Message1 { get; set; }
        public string AuthCode { get; set; }
        public string PNRef { get; set; }
        public string HostCode { get; set; }
        public string GetAVSResult { get; set; }
        public string GetAVSResultTXT { get; set; }
        public string GetStreetMatchTXT { get; set; }
        public string GetZipMatchTXT { get; set; }
        public string GetCVResult { get; set; }
        public string GetCVResultTXT { get; set; }
        public string GetCommercialCard { get; set; }
    }
}
