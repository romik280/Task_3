﻿using System;

namespace AutomaticTelephoneExchange.BillingSystem
{
    public class Contract
    {
        public Guid Id { get; private set; }
        private Guid IdAbonent { get; set; }
        private Guid IdPhoneNumber { get; set; }
        private Guid IdTariff { get; set; }
        private DateTime DateLastTariffChange { get; set; }
        private int Balance { get; set; }


        public Contract(Guid _idAbonent, Guid _idPhoneNumber, Guid _idTariff, int _balance, Guid _idTerminal, Guid _id)
        {
            Id = Guid.NewGuid();
            IdAbonent = _idAbonent;
            IdPhoneNumber = _idPhoneNumber;
            IdTariff = _idTariff;
            DateLastTariffChange = DateTime.Now;
            Balance = _balance;
        }

        public bool ChangeTariff(Guid _idTariff)
        {
            DateTime date = DateTime.Now.Date;
            
            if (date.Month != DateLastTariffChange.Month)
            {
                IdTariff = _idTariff;
                DateLastTariffChange = date;

                Console.WriteLine("Тарифный план успешно измененю");
                return true;
            }
            else
            {
                Console.WriteLine("Тарифный план не изменен, т.к. доступен для изменения не чаще одного раза в месяц.");
                return false;
            }
        }

        public void UpBalans(int _amountPayment)
        {
            Balance = +_amountPayment;
        }

        public Contract GetContract()
        {
            return this;
        }


    }
}