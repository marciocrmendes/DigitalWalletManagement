using DigitalWalletManagement.Entities;
using DigitalWalletManagement.Infraestructure.Repositories;
using FastEndpoints;
using System.Security.Claims;

namespace DigitalWalletManagement.Features.Wallets.CreateWallet
{
    public class CreateWalletEndpoint(WalletRepository walletRepository) : Endpoint<CreateWalletRequest, CreateWalletResponse>
    {
        public override void Configure()
        {
            Tags("Wallets");
            Post("/api/wallet");
            Description(x => x
                .WithTags("Wallets")
                .WithName("Create Wallet")
                .Produces<CreateWalletResponse>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized));
            Validator<CreateWalletValidator>();
        }
        
        public override async Task HandleAsync(CreateWalletRequest request, CancellationToken cancellationToken)
        {
            // Obter o ID do usuário autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userIdGuid))
            {
                ThrowError("User not authenticated or invalid user ID");
                return;
            }
            
            // Criar uma nova carteira
            var wallet = new Wallet
            {
                UserId = userIdGuid,
                Name = request.Name,
                Description = request.Description,
                Balance = request.InitialBalance,
                Currency = request.Currency
            };

            // Salvar a carteira no banco de dados
            await walletRepository.AddAsync(wallet, cancellationToken);
            
            // Retornar a resposta
            var response = new CreateWalletResponse
            {
                Id = wallet.Id,
                Name = wallet.Name,
                Description = wallet.Description,
                Balance = wallet.Balance,
                Currency = wallet.Currency.ToString(),
                CreatedAt = wallet.CreatedAt
            };
            
            await SendCreatedAtAsync<GetWalletEndpoint>(
                new { id = wallet.Id }, 
                response, 
                generateAbsoluteUrl: true, 
                cancellation: cancellationToken);
        }
    }
    
    // Endpoint fictício para usar no CreatedAt
    public class GetWalletEndpoint : EndpointWithoutRequest
    {
        public override void Configure() => Get("/api/wallet/{id}");
        public override Task HandleAsync(CancellationToken ct) => Task.CompletedTask;
    }
}