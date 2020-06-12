using AdvertApi.DTOs.Requests;
using AdvertApi.Models;
using System;
using System.Collections.Generic;


namespace AdvertApi.Services
{
    public interface IAdvertService
    {
        List<Banner> calculateArea(List<Model.Building> buildings,Campaign campaign,decimal pricePerSquareMeter);
    }
}
