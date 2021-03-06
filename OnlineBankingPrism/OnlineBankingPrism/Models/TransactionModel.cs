﻿using System;
using OnlineBankingPrism.SharedEntities.Entities;
using OnlineBankingPrism.SharedEntities.Enums;

namespace OnlineBankingPrism.Models
{

    public class TransactionModel
    {
        public TransactionModel() { }

        public TransactionModel(Transaction transaction)
        {
            SourceCard = transaction.SourceCard;
            Destination = transaction.Destination;
            if (transaction.TransactionDestinationRole == TransactionDestinationRole.Receiver)
            {
                TransactionSum = "+" + transaction.TransactionSum+"UAH";
            }
            else
            {
                TransactionSum = "-" + transaction.TransactionSum + "UAH";
            }
            TransactionType = transaction.TransactionType;
        }

        public String SourceCard { get; set; }
        public String Destination { get; set; }

        public String TransactionSum { get; set; }

        public DateTime Date { get; set; }

        public TransactionTypes TransactionType { get; set; }
    }
}