
using Tekus.Application.Dtos;

namespace Tekus.Application.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(UserDto userDto);
    }
}
