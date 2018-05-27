using System;
using System.Collections.Generic;
using OnlineBanking.Security;
using OnlineBankingPrism.SharedEntities.Entities;
using Web.Infrastructure.Database.Repositories;

namespace OnlineBanking.Test
{
    class Program
    {
        private static void AddTestUser()
        {
            var userRepository = new GenericRepository<User>();
            var user = new User
            {
                Password = PasswordEncoder.GetHash("user"),
                Id = "user",
                CardNumbers = new List<string>
                {
                    "1111222233334444",
                    "1234567890123456",
                    "9999888877776666"
                }
            };
            userRepository.Insert(user).GetAwaiter().GetResult();
        }
        private static void AddTestCards()
        {
            var cardRepository = new GenericRepository<Card>();
            var card1 = new Card
            {
                Balance = 33,
                Id = "1111222233334444",
                ExpireDate = DateTime.Now.AddYears(1),
                OwnerId = "user"
            };

            var card2 = new Card
            {
                Balance = 1222,
                Id = "1234567890123456",
                ExpireDate = DateTime.Now.AddYears(1),
                OwnerId = "user"
            };
            var card3 = new Card
            {
                Balance = 9222,
                Id = "9999888877776666",
                ExpireDate = DateTime.Now.AddYears(1),
                OwnerId = "user"
            };
            cardRepository.Insert(card2).GetAwaiter().GetResult();
            cardRepository.Insert(card1).GetAwaiter().GetResult();
            cardRepository.Insert(card3).GetAwaiter().GetResult();
        }
        private static void AddTestTransactions()
        {
            var transactionRepository = new GenericRepository<CardTransactions>();
            var transactions1 = new CardTransactions
            {
                Id = "1111222233334444",
                Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Destination = "9999888877776666",
                        TransactionSum = 10,
                        SourceCard = "1111222233334444",
                        Date = DateTime.Now
                    },
                    new Transaction
                    {
                        Destination = "1234567890123456",
                        TransactionSum = 100,
                        SourceCard = "1111222233334444",
                        Date = DateTime.Now
                    }
                }
            };
            var transactions2 = new CardTransactions
            {
                Id = "9999888877776666",
                Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Destination = "1111222233334444",
                        TransactionSum = 10,
                        SourceCard = "9999888877776666",
                        Date = DateTime.Now
                    },
                    new Transaction
                    {
                        Destination = "1234567890123456",
                        TransactionSum = 100,
                        SourceCard = "9999888877776666",
                        Date = DateTime.Now
                    }
                }
            };
            var transactions3 = new CardTransactions
            {
                Id = "1234567890123456",
                Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Destination = "1111222233334444",
                        TransactionSum = 10,
                        SourceCard = "1234567890123456",
                        Date = DateTime.Now
                    },
                    new Transaction
                    {
                        Destination = "9999888877776666",
                        TransactionSum = 100,
                        SourceCard = "1234567890123456",
                        Date = DateTime.Now
                    }
                }
            };
            transactionRepository.Insert(transactions3).GetAwaiter().GetResult();
            transactionRepository.Insert(transactions2).GetAwaiter().GetResult();
            transactionRepository.Insert(transactions1).GetAwaiter().GetResult();
        }
        public static void InitialDataInput()
        {
            AddTestUser();
            AddTestCards();
            AddTestTransactions();
        }
        static void Main()
        {
            InitialDataInput();
        }
    }
}
