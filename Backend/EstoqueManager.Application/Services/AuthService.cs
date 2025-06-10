using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstoqueManager.Application.DTOs;
using EstoqueManager.Application.Interfaces;
using EstoqueManager.Domain.Entities;
using GerenciamentoEstoque.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace EstoqueManager.Application.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<Usuario> _usuarioRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IRepository<Usuario> usuarioRepository, IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    public async Task<TokenResponseDTO> LoginAsync(LoginDTO dto)
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        var usuario = usuarios.FirstOrDefault(u => u.Email == dto.Email);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha))
            throw new Exception("Credenciais inválidas.");

        var token = GenerateJwtToken(usuario);
        return new TokenResponseDTO
        {
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(
                double.Parse(_configuration["Jwt:ExpiryMinutes"]))
        };
    }

    public async Task RegisterAsync(RegisterDTO dto)
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        if (usuarios.Any(u => u.Email == dto.Email))
            throw new Exception("Email já registrado.");

        var usuario = new Usuario
        {
            Email = dto.Email,
            Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            Role = dto.Role
        };
        await _usuarioRepository.AddAsync(usuario);
    }

    private string GenerateJwtToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(_configuration["Jwt:ExpiryMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}