using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models
{
    public class Banner
    {
        public int IdAdvertisment { get; set; }
        public int Name { get; set; }
        public decimal Price { get; set; }
        public int? IdCampaign { get; set; }
        public Campaign Campaign { get; set; }
        public decimal Area { get; set; }
    }
}
