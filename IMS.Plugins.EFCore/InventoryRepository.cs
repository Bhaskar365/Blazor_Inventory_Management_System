﻿using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EFCore
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMSContext _db;
        public InventoryRepository(IMSContext db) 
        {
            _db = db;
        }
        public async Task<IEnumerable<Inventory>> GetInventoriesByName(string name)
        {
            return await this._db.Inventories.Where(x => x.InventoryName.Contains(name) ||
                        string.IsNullOrWhiteSpace(name)).ToListAsync();
        }
    }
}
