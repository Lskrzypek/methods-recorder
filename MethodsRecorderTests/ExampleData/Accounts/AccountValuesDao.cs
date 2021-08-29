using System;
using System.Linq;

namespace MethodsRecorderTests.ExampleData.Accounts
{
    public class AccountValuesDao : IAccountValuesDao
    {
        private readonly IAccountValuesReader AccountValuesReader;
        private readonly ICurrentTime CurrentTime;

        public AccountValuesDao(IAccountValuesReader accountValuesReader, ICurrentTime currentTime)
        {
            AccountValuesReader = accountValuesReader;
            CurrentTime = currentTime;
        }

        public AccountValue GetCurrent(string accountNum)
        {
            var now = CurrentTime.GetCurrentTime();
            return AccountValuesReader
                .ReadAllAccountValues()
                .FirstOrDefault(x => x.AccountNum == accountNum && x.Date == now);
        }

        public AccountValue Get(Account account, DateTime dateTime)
        {
            return AccountValuesReader
                .ReadAllAccountValues()
                .FirstOrDefault(x => x.AccountNum == account.AccountNumber && x.Date == dateTime);
        }

        public float GetValue(string accountNum, DateTime dateTime)
        {
            return AccountValuesReader
                .ReadAllAccountValues()
                .FirstOrDefault(x => x.AccountNum == accountNum && x.Date == dateTime)?
                .Value ?? 0f;
        }
    }
}