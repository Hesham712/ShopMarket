﻿using ShopMarket_Web_API.Dtos.Shift;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Reprository.Interface
{
    public interface IShiftRepository
    {
        Task<bool> isActiveShift(int ShiftId);
        Task<Shift> OpenShift(NewShiftDto shiftDto);
        Task<bool> CloseShift(string UserName);
    }
}
