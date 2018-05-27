using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineBankingPrism.SharedEntities.Entities;
using Web.Infrastructure.Database.Repositories;

namespace Web.ApplicationCore.Services
{
    public class UserService
    {
        public UserService()
        {
            _userRepository = new GenericRepository<User>();
            _transactionRepository = new GenericRepository<CardTransactions>();
            _cardRepository = new GenericRepository<Card>();
        }

        public async Task<ApplicationUser> GetUser(String login)
        {
            var user = await _userRepository.GetById(login);
            if (user == null)
            {
                return null;
            }
            var cardsList = new List<Card>();
            var cardTransactionsList = new List<CardTransactions>();
            foreach (var cardNumber in user.CardNumbers)
            {
                var card = await _cardRepository.GetById(cardNumber);
                if (card == null)
                {
                    continue;
                }
                cardsList.Add(card);
                var transactions = await _transactionRepository.GetById(cardNumber);
                if (transactions == null)
                {
                    continue;
                }
                cardTransactionsList.Add(transactions);
            }
            return new ApplicationUser(user, cardTransactionsList, cardsList);
        }

        public async Task<Boolean> TransferMoney(Transaction transaction)
        {
            if (!VerifyTransferTransactionDestination(transaction))
            {
                return false;
            }
            var sourceCard = await _cardRepository.GetById(transaction.SourceCard);
            if (sourceCard == null)
            {
                return false;
            }

            if (sourceCard.Balance < transaction.TransactionSum)
            {
                return false;
            }
            var destinationCard = await _cardRepository.GetById(transaction.Destination);
            sourceCard.Balance -= transaction.TransactionSum;

            if (await _cardRepository.Update(sourceCard))
            {
                destinationCard.Balance += transaction.TransactionSum;
                if (!await _cardRepository.Update(destinationCard))
                {
                    sourceCard.Balance += transaction.TransactionSum;
                    await _cardRepository.Update(sourceCard);
                    return false;
                }
            }

            return await SaveTransaction(transaction);
        }

        public async Task<Boolean> ReplenishMoney(Transaction transaction)
        {
            if (!VerifyReplenishmentTransactionDestination(transaction))
            {
                return false;
            }
            var sourceCard = await _cardRepository.GetById(transaction.SourceCard);
            if (sourceCard == null)
            {
                return false;
            }

            if (sourceCard.Balance < transaction.TransactionSum)
            {
                return false;
            }
            sourceCard.Balance -= transaction.TransactionSum;

            if (await _cardRepository.Update(sourceCard))
            {
                return await SaveTransaction(transaction);
            }

            return false;
        }

        private async Task<Boolean> SaveTransaction(Transaction transaction)
        {
            var cardTransaction = await _transactionRepository.GetById(transaction.SourceCard);
            if (cardTransaction == null)
            {
                cardTransaction = new CardTransactions
                {
                    Id = transaction.SourceCard,
                    Transactions = new List<Transaction> { transaction }
                };
                return await _transactionRepository.Insert(cardTransaction);
            }

            cardTransaction.Transactions.Add(transaction);
            return await _transactionRepository.Update(cardTransaction);
        }
        private Boolean VerifyTransferTransactionDestination(Transaction transaction)
            => transaction.Destination.Length == 16 && transaction.Destination.All(char.IsDigit);

        private Boolean VerifyReplenishmentTransactionDestination(Transaction transaction)
        {
            if (transaction.Destination.Length != 13)
            {
                return false;
            }

            if (transaction.Destination[0] != '+')
            {
                return false;
            }

            var substring = transaction.Destination.Substring(1, transaction.Destination.Length - 1);

            return substring.All(char.IsDigit);
        }
        private readonly GenericRepository<Card> _cardRepository;
        private readonly GenericRepository<CardTransactions> _transactionRepository;
        private readonly GenericRepository<User> _userRepository;
    }
}
