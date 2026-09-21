using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain.Business_Entities
{
    public class InvoiceData
    {
        public long InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? TravelDate { get; set; }

        public decimal? Dl { get; set; }
        public decimal? Le { get; set; }

        public int VesselImo { get; set; }
        public string? VesselName { get; set; }

        public short? AgencyNumber { get; set; }
        public byte? Currency { get; set; }

        public decimal? GrossTonnage { get; set; }
        public decimal? NetTonnage { get; set; }
    }
}
