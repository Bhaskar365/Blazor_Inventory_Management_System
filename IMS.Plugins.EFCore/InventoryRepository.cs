using IMS.CoreBusiness;
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
            return await this._db.Inventories.Where(x => x.InventoryName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                        string.IsNullOrWhiteSpace(name)).ToListAsync();
        }

        public async Task AddInventoryAsync(Inventory inventory) 
        {
            this._db.Add(inventory);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateInventoryAsync(Inventory inventory) 
        {
            var inv = await this._db.Inventories.FindAsync(inventory.InventoryId);
            if (inv != null) 
            {
                inv.InventoryId = inventory.InventoryId;
                inv.Price = inventory.Price;
                inv.Quantity = inventory.Quantity;

                await _db.SaveChangesAsync();
            }
        }

        public async Task<Inventory?> GetInventoryByIdAsync(int inventoryId)
        {
            return await this._db.Inventories.FindAsync(inventoryId);
        }
    }
}
