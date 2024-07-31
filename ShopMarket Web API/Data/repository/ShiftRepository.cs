using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopMarket_Web_API.Data.Interface;
using ShopMarket_Web_API.Dtos.Shift;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Data.repository
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ShiftRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> isActiveShift(int ShiftId)
        {
            var shiftResult = await _context.Shifts.AnyAsync(m => m.Id == ShiftId && m.EndShift == null);
            return shiftResult;
        }

        public async Task<Shift> OpenShift(NewShiftDto shiftDto)
        {
            var shiftExist = await _context.Shifts.FirstOrDefaultAsync(m => m.UserId == shiftDto.UserId && m.EndShift == null);
            try
            {
                if (shiftExist == null)
                {
                    var result = _mapper.Map<Shift>(shiftDto);
                    await _context.Shifts.AddAsync(result);
                    await _context.SaveChangesAsync();
                    return result;
                }
                return shiftExist;
            }
            catch (ObjectDisposedException  ex)
            {
                Console.WriteLine("ObjectDisposedException: " + ex.Message);
                throw;
            }

            
        }
    }
}
