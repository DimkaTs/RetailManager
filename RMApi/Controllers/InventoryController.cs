﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IConfiguration _config;

        public InventoryController(IConfiguration config)
        {
            _config = config;
        }

        [Authorize(Roles = "Manager,Admin")] //Coma meaning "OR" relationship. Or Manager or Admin can Get this info
        public List<InventoryModel> Get()
        {
            InventoryData data = new InventoryData(_config);
            return data.GetInventory();
        }

        //[Authorize(Roles = "WarehouseWorker")] Two lines meaning "AND". WarehouseWorker and Admin can Post the info
        [Authorize(Roles = "Admin")]
        public void Post(InventoryModel item)
        {
            InventoryData data = new InventoryData(_config);
            data.SaveInventoryRecord(item);
        }
    }
}