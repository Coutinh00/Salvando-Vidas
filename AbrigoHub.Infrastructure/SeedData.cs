using AbrigoHub.Core.Entities;
using AbrigoHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AbrigoHub.Infrastructure
{
    public static class SeedData
    {
        public static async Task EnsurePopulated(IServiceProvider serviceProvider)
        {
            AbrigoHubContext context = serviceProvider.GetRequiredService<AbrigoHubContext>();

            if (!context.Usuarios.Any())
            {
                var adminUser = new Usuario
                {
                    Nome = "Admin",
                    Email = "admin@example.com",
                    SenhaHash = "hashedpassword", // Em um cenário real, use um hash seguro
                    TipoUsuario = "Administrador",
                    Telefone = "11999999999"
                };
                context.Usuarios.Add(adminUser);
                await context.SaveChangesAsync();
            }

            // Garante que os abrigos sejam populados, mesmo se o usuário já existia.
            if (!context.Abrigos.Any())
            {
                // Tenta obter o usuário admin (se já existe ou foi recém-criado)
                var adminUser = await context.Usuarios.FirstOrDefaultAsync(u => u.Email == "admin@example.com");

                if (adminUser != null)
                {
                    context.Abrigos.AddRange(
                        new Abrigo
                        {
                            Nome = "Abrigo Esperança",
                            Descricao = "Um abrigo para famílias desabrigadas.",
                            Endereco = "Rua da Paz, 123",
                            Cidade = "São Paulo",
                            Estado = "SP",
                            Cep = "01000-000",
                            Capacidade = 100,
                            OcupacaoAtual = 50,
                            CriadoEm = DateTime.Now,
                            Status = "Ativo",
                            UsuarioId = adminUser.Id // Associe ao usuário recém-criado
                        },
                        new Abrigo
                        {
                            Nome = "Casa Acolhedora",
                            Descricao = "Apoio a vítimas de desastres naturais.",
                            Endereco = "Av. da Solidariedade, 456",
                            Cidade = "São Paulo",
                            Estado = "SP",
                            Cep = "02000-000",
                            Capacidade = 75,
                            OcupacaoAtual = 30,
                            CriadoEm = DateTime.Now,
                            Status = "Ativo",
                            UsuarioId = adminUser.Id // Associe ao usuário recém-criado
                        }
                    );
                    await context.SaveChangesAsync();
                }
            }
        }
    }
} 