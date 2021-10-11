using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier
{
    public class Address
    {
        public string City { get; set; }
        public string Postalcode { get; set; }
        public string Streetname { get; set; }
        public uint Housenumber { get; set; }
        #nullable enable
        public string? Addition { get; set; }
    }
}
