using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Zip
    {
        public string ZIPCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }

    public enum State
    {
        AL ,
        AK ,
        AZ ,
        AR ,
        CA ,
        CO ,
        CT ,
        DE ,
        DC ,
        FL ,
        GA ,
        HI ,
        ID ,
        IL ,
        IN ,
        IA ,
        KS ,
        KY ,
        LA ,
        ME ,
        MD ,
        MA ,
        MI ,
        MN ,
        MS ,
        MO ,
        MT ,
        NE ,
        NV ,
        NH ,
        NJ ,
        NM ,
        NY ,
        NC ,
        ND ,
        OH ,
        OK ,
        OR ,
        PA ,
        RI ,
        SC ,
        SD ,
        TN ,
        TX ,
        UT ,
        VT ,
        VA ,
        WA ,
        WV ,
        WI ,
        WY
    }
}
