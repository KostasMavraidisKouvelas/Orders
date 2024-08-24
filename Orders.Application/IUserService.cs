using Orders.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application
{
    public interface IUserService
    {
        Task<string> Register(RegisterUserDto user);
        Task<string> Login(LoginDto user);

    }
}
