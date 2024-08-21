using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Asn1.Cms;
using ShopMarket_Web_API.Data;
using ShopMarket_Web_API.Dtos.Shift;
using ShopMarket_Web_API.Models;
using ShopMarket_Web_API.Reprository.Interface;

namespace ShopMarket_Web_API.Reprository.repository
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

        public async Task<bool> CloseShift(string UserName)
        {
            var shift = await _context.Shifts.Include(x=>x.User).FirstOrDefaultAsync(x=>x.User.UserName == UserName);
            if (shift != null)
            {
                if(shift.EndShift == null)
                {
                    shift.EndShift = DateTime.Now;
                    await _context.SaveChangesAsync();
                    return true;
                }
                throw new ArgumentException("Shift already ended.");
            }
            throw new ArgumentException("Shift does not exist or has already ended.");
        }

        public async Task<bool> isActiveShift(int ShiftId)=>
            await _context.Shifts.AnyAsync(m => m.Id == ShiftId && m.EndShift == null);

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
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine("ObjectDisposedException: " + ex.Message);
                throw;
            }
        }
    }
}
