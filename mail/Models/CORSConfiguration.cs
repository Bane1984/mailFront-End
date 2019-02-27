using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mail.Interfaces;

namespace mail.Models
{
    public class CORSConfiguration : ICORSConfiguration
    {
       public string[] WithOrigin { get; set; }
       public string[] WithMethod { get; set; }
       public string[] WithHeader { get; set; }
       public string[] WithCredentials { get; set; }
    }
}
