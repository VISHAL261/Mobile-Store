using MobileStore.Dto;
using MobileStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileStore.Interfaces
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(LoginDto user);
    }
}
