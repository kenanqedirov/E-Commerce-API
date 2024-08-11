using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.Application.Abstraction.Token
{
    public interface ITokenHandler
    {
        Dtos.Token CreateAccessToken(int minute); 
    }
}
