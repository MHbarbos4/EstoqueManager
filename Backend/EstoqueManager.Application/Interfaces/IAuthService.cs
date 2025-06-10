using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstoqueManager.Application.DTOs;

namespace EstoqueManager.Application.Interfaces;

public interface IAuthService
{
    Task<TokenResponseDTO> LoginAsync(LoginDTO dto);
    Task RegisterAsync(RegisterDTO dto);
}