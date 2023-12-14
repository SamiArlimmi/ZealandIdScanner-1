using Microsoft.EntityFrameworkCore;
using ZealandIdScanner.EBbContext;
using ZealandIdScanner.Models;

namespace ZealandIdScanner.Services
{
    public class DbService
    {
        private readonly ZealandIdContext _dbContext;

        public DbService(DbContextOptions<ZealandIdContext> options)
        {
            _dbContext = new ZealandIdContext(options);
        }

        public async Task<List<Lokaler>> GetLokaler()
        {
            return await _dbContext.Lokaler.ToListAsync();
        }

        public async Task AddLokaler(Lokaler lokaler)
        {
            _dbContext.Lokaler.Add(lokaler);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveLokaler(List<Lokaler> lokalerList)
        {
            _dbContext.Lokaler.AddRange(lokalerList);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Addsensors(Sensors sensors)
        {
            _dbContext.Sensors.Add(sensors);
            await _dbContext.SaveChangesAsync();
        }
        public async Task SaveSensors(List<Sensors> sensorsList)
        {
            _dbContext.Sensors.AddRange(sensorsList);
            await _dbContext.SaveChangesAsync();
        }


    }
}
