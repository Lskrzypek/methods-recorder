using System;
using System.Collections.Generic;

namespace MethodsRecorderTests.ExampleData.Accounts
{
    public class AccountValuesReader : IAccountValuesReader
    {
        private readonly IEnumerable<AccountValue> AccountValues = new[]
        {
            new AccountValue()
            {
                AccountNum = "1-1234-5678-9012",
                Date = new DateTime(2021, 7, 1, 12, 0, 0),
                Value = 1000
            },
            new AccountValue()
            {
                AccountNum = "2-0000-1111-2222",
                Date = new DateTime(2021, 7, 1, 12, 0, 0),
                Value = 50000
            },
            new AccountValue()
            {
                AccountNum = "1-1234-5678-9012",
                Date = new DateTime(2021, 7, 1, 13, 0, 0),
                Value = 1100
            },
            new AccountValue()
            {
                AccountNum = "1-1234-5678-9012",
                Date = new DateTime(2021, 7, 1, 14, 0, 0),
                Value = 1500
            },
        };

        public IEnumerable<AccountValue> ReadAllAccountValues()
        {
            return AccountValues;
        }
    }
}