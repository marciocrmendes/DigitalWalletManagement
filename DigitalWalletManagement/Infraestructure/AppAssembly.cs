using DigitalWalletManagement.Infraestructure.Context;
using System.Reflection;

namespace DigitalWalletManagement.Infraestructure
{
    public static class AppAssembly
    {
        public static readonly Assembly Assembly = typeof(AppDbContext).Assembly;
    }
}
