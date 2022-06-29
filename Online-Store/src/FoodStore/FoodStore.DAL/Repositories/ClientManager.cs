using FoodStore.DAL.EF;
using FoodStore.DAL.Entities;
using FoodStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodStore.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public StoreDbContext Database { get; set; }
        public ClientManager(StoreDbContext context) =>
            Database = context;

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
