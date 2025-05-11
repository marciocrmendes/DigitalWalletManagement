using DigitalWalletManagement.Commons.Enums;
using DigitalWalletManagement.Entities;
using DigitalWalletManagement.Infraestructure.Repositories;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DigitalWalletManagement.Features.Wallets.AddTransaction
{
    public class AddTransactionEndpoint(WalletRepository walletRepository) : Endpoint<AddTransactionRequest, AddTransactionResponse>
    {
        public override void Configure()
        {
            Post("/api/wallet/transaction");
            Description(x => x
                .WithTags("Wallets")
                .WithName("Add Transaction")
                .Produces<AddTransactionResponse>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status404NotFound));
            Validator<AddTransactionValidator>();
        }
        
        public override async Task HandleAsync(AddTransactionRequest request, CancellationToken cancellationToken)
        {
            // Obter o ID do usuário autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userIdGuid))
            {
                ThrowError("User not authenticated or invalid user ID");
                return;
            }
            
            // Buscar a carteira
            var wallet = await walletRepository.GetAll()
                .Include(w => w.Transactions)
                .FirstOrDefaultAsync(w => w.Id == request.WalletId && w.UserId == userIdGuid, cancellationToken);

            if (wallet == null)
            {
                await SendNotFoundAsync(cancellationToken);
                return;
            }
            
            // Validar o saque (se for o caso)
            if (request.TransactionType == TransactionTypeEnum.Withdraw && wallet.Balance < request.Amount)
            {
                AddError("Insufficient funds for withdrawal");
                await SendErrorsAsync(StatusCodes.Status400BadRequest, cancellation: cancellationToken);
                return;
            }
            
            // Criar a transação
            var transaction = new WalletTransaction
            {
                WalletId = wallet.Id,
                Amount = request.Amount,
                Currency = wallet.Currency,
                TransactionType = request.TransactionType,
                TransactionDate = DateTime.UtcNow
            };
            
            // Atualizar o saldo da carteira
            if (request.TransactionType == TransactionTypeEnum.Deposit)
            {
                wallet.Balance += request.Amount;
            }
            else if (request.TransactionType == TransactionTypeEnum.Withdraw)
            {
                wallet.Balance -= request.Amount;
            }
            else if (request.TransactionType == TransactionTypeEnum.Transfer)
            {
                // Para transferências, aqui seria implementada a lógica adicional
                // como identificar a carteira de destino e atualizar seu saldo
                wallet.Balance -= request.Amount;
            }
            
            // Adicionar a transação à carteira e salvar
            wallet.Transactions.Add(transaction);
            await walletRepository.UpdateAsync(wallet, cancellationToken);
            
            // Retornar a resposta
            var response = new AddTransactionResponse
            {
                Id = transaction.Id,
                WalletId = wallet.Id,
                Amount = transaction.Amount,
                TransactionType = transaction.TransactionType.ToString(),
                NewBalance = wallet.Balance,
                TransactionDate = transaction.TransactionDate
            };
            
            await SendCreatedAtAsync<GetTransactionEndpoint>(
                new { id = transaction.Id }, 
                response, 
                generateAbsoluteUrl: true, 
                cancellation: cancellationToken);
        }
    }
    
    // Endpoint fictício para usar no CreatedAt
    public class GetTransactionEndpoint : EndpointWithoutRequest
    {
        public override void Configure() => Get("/api/wallet/transaction/{id}");
        public override Task HandleAsync(CancellationToken ct) => Task.CompletedTask;
    }
}