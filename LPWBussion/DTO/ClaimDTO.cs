using LPWService.StaticFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPWBussion.DTO
{
    public class ClaimDTO
    {
        private string _id;
        public string Email { get; set; }
        public object Id { get { return  int.Parse( _id.ToString().Sha256Decrypto()); } set { _id =value.ToString(); } }
    }
}
