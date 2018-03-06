﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Transaction
    {
        private double _cost;
        private double _amtTendered;
        private double _rawChange;

        private List<Tuple<Currency, int>> Change;

        public Transaction(double cost, double amtTendered)
        {
            if (Math.Abs(cost) < 0)
            {
                throw new InvalidOperationException("Error - cost cannot be less than 0");
            }

            if (Math.Abs(amtTendered) < 0)
            {
                throw new InvalidOperationException("Error - amt tendered cannot be less than 0");
            }

            if (amtTendered < cost)
            {
                throw new InvalidOperationException("Error - cost cannot exceed amt tendered");
            }

            _cost = cost;
            _amtTendered = amtTendered;
            _rawChange = Math.Round(_amtTendered - _cost, 2);
            Change = new List<Tuple<Currency, int>>();
        }

        public void CalculateChange()
        {
            if (_rawChange % 3 > 0)
            {
                CalcChangeNormal();
            }
            else
            {
                CalcChangeRandom();
            }
        }

        public void DisplayChange()
        {
            if (Change == null)
            {
                Console.WriteLine("Error - Change is null");
                return;
            }

            if (Change.Count == 0)
            {
                Console.WriteLine("Change paid with exact change. No change necessary.");
                return;
            }

            if (Currencies.CurrencyList.Count == 0)
            {
                throw new InvalidOperationException("No currencies were loaded with which to create change");
            }

            var output = "";
            for (var i = 0; i < Change.Count; i++)
            {
                //Determine whether to output singular or plural denomination name
                var displayDenominationNamePluralOrSingular = Change[i].Item2 == 1 ? Change[i].Item1.Denomination : Change[i].Item1.DenominationPlural;

                //Write the line
                output += $"{Change[i].Item2} {displayDenominationNamePluralOrSingular}";

                //Write a comma if we're not done
                if (i < Change.Count - 1)
                {
                    output += ", ";
                }
            }

            Console.WriteLine(output);
        }

        private void CalcChangeRandom()
        {
            var totalCurrencies = Currencies.CurrencyList.Count;
            if (totalCurrencies == 0)
            {
                throw new InvalidOperationException("No currencies to choose from");
            }
        }

        private void CalcChangeNormal()
        {
            var currencyList = Currencies.CurrencyList.OrderByDescending(c => c.Value);
            var rollingChange = _rawChange;
            foreach (var currency in currencyList)
            {
                var totalForThisCurrency = CalculateNumberOfDenoms(rollingChange, currency);
                if (totalForThisCurrency > 0)
                {
                    Change.Add(new Tuple<Currency, int>(currency, totalForThisCurrency));
                }

                rollingChange -= Math.Round(currency.Value * totalForThisCurrency, 2, MidpointRounding.ToEven);

                if (rollingChange <= 0)
                {
                    break;
                }
            }
        }

        private static int CalculateNumberOfDenoms(double cost, Currency currency)
        {
            var dividend = Math.Floor(cost / currency.Value);
            return Convert.ToInt32(dividend);
        }

        private static int GetRandomNumber(int startRange, int endRange)
        {
            var randomGenerator = new Random();
            var randomInt = randomGenerator.Next(startRange, endRange);

            return randomInt;
        }
    }    
}
