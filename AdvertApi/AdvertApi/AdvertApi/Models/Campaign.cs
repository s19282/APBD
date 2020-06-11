using AdvertApi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models
{
    public class Campaign
    {
        public int IdCampaign { get; set; }
        //[ForeignKey("IdClient")]
        public int? IdClient { get; set; }
        public Client Client { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerSquareMeter { get; set; }
        public virtual ICollection<Banner> Banners { get; set; }
        //[ForeignKey("FromIdBuilding")]
        public int? FromIdBuilding { get; set; }
        public Building FBulidling { get; set; }
        //[ForeignKey("ToIdBuilding")]
        public int? ToIdBuilding { get; set; }
        public Building TBulidling { get; set; }
    }
}
