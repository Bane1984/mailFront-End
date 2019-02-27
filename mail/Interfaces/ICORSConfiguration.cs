using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mail.Interfaces
{
    public interface ICORSConfiguration
    {
        string[] WithOrigin { get; set; }
        string[] WithMethod { get; set; }
        string[] WithHeader { get; set; }
        string[] WithCredentials { get; set; }
    }
}
