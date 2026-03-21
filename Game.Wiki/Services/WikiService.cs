using Game.Kafka.Contracts;
using Game.Wiki.Data;
using Game.Wiki.DTOs;
using Game.Wiki.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Xml.Linq;

namespace Game.Wiki.Services
{
    public class WikiService
    {
        private readonly WikiDbContext _context;
        private readonly KafkaClient _kafkaClient;

        public WikiService(WikiDbContext context, KafkaClient kafkaClient)
        {
            _context = context;
            _kafkaClient = kafkaClient;
        }

        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            return await _context.Location
                .Select(l => new Location
                {
                    LocationUid = l.LocationUid,
                    Name = l.Name,
                    Description = l.Description,
                })
                .ToListAsync();
        }

        public async Task<LocationItem?> GetByLocation(string name)
        {
            var location = await _context.Location
                .Include(l => l.Items)
                .FirstOrDefaultAsync(l => l.Name == name);

            if (location == null) return null;

            return new Location
            {
                LocationUid = location.LocationUid,
                Name = location.Name,
                Description = location.Description,
                Items = location.Items.Select(i => new LocationItem
                {
                    ItemUid = i.ItemUid,
                    Location = i.Location,
                    Name = i.Name,
                    Description = i.Description,
                }).ToList()
            };
        }

        public async Task<IEnumerable<Category>> GetAllTypes()
        {
            return await _context.Type
                .Select(c => new Category
                {
                    TypeUid = c.TypeUid,
                    Name = c.Name,
                    Description = c.Description,
                })
                .ToListAsync();
        }

        public async Task<TypeItem?> GetByType(string name)
        {
            var type = await _context.Type
                .Include(t => t.Items)
                .FirstOrDefaultAsync(t => t.Name == name);

            if (type == null) return null;

            return new Category
            {
                TypeUid = type.TypeUid,
                Name = type.Name,
                Description = type.Description,
                Items = type.Items.Select(i => new TypeItem
                {
                    ItemUid = i.ItemUid,
                    Type = i.Category,
                    Name = i.Name,
                    Description = i.Description,
                }).ToList()
            };
        }
    }
}
