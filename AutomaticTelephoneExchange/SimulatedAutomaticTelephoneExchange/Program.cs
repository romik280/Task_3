﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AutomaticTelephoneExchange;
using AutomaticTelephoneExchange.ATE;
using AutomaticTelephoneExchange.BillingSystem;

namespace SimulatedAutomaticTelephoneExchange
{
    public class Program
    {
        static void Main(string[] args)
        {
            int randomNumberAbonent;
            int randomNumberPhoneNumber;
            string randomPhoneNumber;


            Abonent randomAbonent;
            Random random = new Random();

            Console.OutputEncoding = Encoding.UTF8;

            List<Abonent> abonents = new List<Abonent>
            {
                new Abonent("Chizhikov A.I.", "3210781M064PB6"),
                new Abonent("Petrov Y.D", "4830781P028KN3"),
                new Abonent("Serikov I.G.", "3212781G013СP8"),
                new Abonent("Ivanova L.N.", "5014602J064AB1"),
                new Abonent("Bubentsov Y.B.", "5730781R064PB0")
            };

            PhoneExchange phoneExchange = new PhoneExchange();
            phoneExchange.CreatePorts(10);
            phoneExchange.CreateTerminals(10);
            BillingSystem bilingSystem = new BillingSystem();

            for (int i = 0; i < abonents.Count - 1; i++)
            {
                var abonent = abonents[i];
                
                var freePort = phoneExchange.GetFreePort(); //Находим свободный порт
                var freeTerminal = phoneExchange.GetFreeTerminal(freePort); //Находим свободный терминал
                var freePhoneNumber = phoneExchange.GetFreePhoneNumber(); //Находим свободный номер телефона

                int randomNumberTarrid = random.Next(bilingSystem.tariffs.Count); //Рандомно возвращаем номер таррифа из коллекции
                var randomTariff = bilingSystem.tariffs[randomNumberTarrid]; //Рандомно возвращаем тариф

                int randomBalans = random.Next(0, 50); //Разновмно генерируем начальный баланс для абонента

                //Заключение договора с клиентом
                var contract = new Contract(abonent.Id, freePhoneNumber, randomTariff.Id, randomBalans, freeTerminal.Id, freePort.Id);
                phoneExchange.CreateContract(contract, freePhoneNumber, freeTerminal, freePort);

                //Выдаем абоненту терминал и порт
                freeTerminal.AssignPort(freePort);
                abonent.AssignTerminal(freeTerminal);

                Console.WriteLine("Abonent " + abonent.FIO + " concluded a contract for the tariff plan " + randomTariff.Name);
                Thread.Sleep(1000);

                abonent.ConnectTerminalToPort(); //Подключение к порту телефона абонентом
                Thread.Sleep(1000);
            }

            //Исходящий вызов абонента
            randomNumberAbonent = random.Next(0, abonents.Count - 1);
            randomAbonent = abonents[randomNumberAbonent];
            randomNumberPhoneNumber = random.Next(0, phoneExchange.phoneNumbers.Count - 1);
            randomPhoneNumber = phoneExchange.phoneNumbers[randomNumberPhoneNumber];
            randomAbonent.OutboundСall(randomPhoneNumber);

            Thread.Sleep(3000);

            //Завершение звонка
            var randomEndCal = random.Next(1, 2);
            if (randomEndCal == 1)
            {
                randomAbonent.EndCall();
            }
            else
            {
                //randomInterlocutor.EndCall();
            }
            Thread.Sleep(1000);

            //Отключение от порта телефона абонентом
            randomNumberAbonent = random.Next(0, abonents.Count - 1);
            randomAbonent = abonents[randomNumberAbonent];
            randomAbonent.DisconnectTerminalToPort();

            Console.ReadKey();
        }

    }
}
