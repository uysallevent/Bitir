﻿using Core.Wrappers;
using Module.Shared.Entities.AuthModuleEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bitir.Mobile.Services.Interfaces
{
    public interface INeighbourhoodService
    {
        Task<ResponseWrapper<Neighbourhood>> GetProvince(Neighbourhood request);
    }
}
