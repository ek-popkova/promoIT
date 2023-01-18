using promoit_backend_cs.Services;
using promoit_backend_cs_api.Data;
using promoit_backend_cs_api.ModelsDTO;

namespace promoit_backend_cs_api.Services
{
	public class SaTransactionService
	{
		private readonly promo_itContext _context;
		private readonly ILogger<SaTransactionService> _logger;
		private readonly ExistsService _existsService;

		public SaTransactionService(promo_itContext context, ILogger<SaTransactionService> logger, ExistsService existsService)
		{
			_context = context;
			_logger = logger;
			_existsService = existsService;
		}

/*		public async Task<SaTransactionDTO> EditTransaction(int id, SaTransactionDTO transaction)
		{
			var existingTransaction = await _context.SaTransactions.FindAsync(id);

			if (existingTransaction != null)
			{
				throw new Exception($"There is no such transaction");
			}
			if (id != transaction.Id)
			{
				throw new Exception($"The transaction with the ID {id} was not found");
			}

			existingTransaction.SaId = transaction.SaId;
			existingTransaction.BcrId = transaction.BcrId;
			existingTransaction.ProductId= transaction.ProductId;
			existingTransaction.ProductsNumber= transaction.ProductsNumber;
			existingTransaction.Price= transaction.Price;
			existingTransaction
		}*/
	}
}
