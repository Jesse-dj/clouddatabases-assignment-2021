using DataTier.Models;
using System;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var customer = new Customer()
            {
                IncomePerYear = 35000
            };

            Assert.Equal(2916.66, customer.IncomePerMonth);
        }
    }
}
