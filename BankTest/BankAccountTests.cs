using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;
using System;

namespace BankTest
{
    [TestClass]
    public class BankAccountTests
    {
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            account.Debit(debitAmount);

            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act and assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Debit(debitAmount));
        }

        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        // Далее идут методы, написанные мной

        // Тест на успешное зачисление положительной суммы
        [TestMethod]
        public void Credit_WithValidAmount_IncreasesBalance()
        {
            // Arrange
            double beginningBalance = 10.0;
            double creditAmount = 5.0;
            double expected = 15.0;
            BankAccount account = new BankAccount("Test User", beginningBalance);

            // Act
            account.Credit(creditAmount);

            // Assert
            Assert.AreEqual(expected, account.Balance, 0.001, "Balance should increase by credited amount");
        }
        // Тест на исключение при попытке зачислить отрицательную сумму
        [TestMethod]
        public void Credit_WhenAmountIsNegative_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            BankAccount account = new BankAccount("Test User", 10.0);
            double creditAmount = -5.0;

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Credit(creditAmount));
        }

        // Тест на исключение при попытке снять сумму больше баланса
        [TestMethod]
        public void Debit_WhenAmountExceedsBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            BankAccount account = new BankAccount("Test User", 10.0);
            double debitAmount = 20.0;

            // Act & Assert
            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                StringAssert.Contains(ex.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }
            Assert.Fail("Expected exception was not thrown.");
        }
    }
}
