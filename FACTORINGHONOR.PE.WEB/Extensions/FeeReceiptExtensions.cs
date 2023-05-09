using FACTORINGHONOR.PE.CORE.Helpers;
using FACTORINGHONOR.PE.ENTITIES.Extensions;
using FACTORINGHONOR.PE.ENTITIES.Models;
using FACTORINGHONOR.PE.WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FACTORINGHONOR.PE.WEB.Extensions
{
    public static class FeeReceiptExtensions
    {
        public static double ToCurrencyType(this double value, CurrencyType currencyType)
        {
            return value * currencyType.Value;
        }

        public static FeeReceiptViewModel ToViewModel(this FeeReceipt feeReceipt)
        {
            var model = new FeeReceiptViewModel
            {
                Id = feeReceipt.Id,
                IssuingRuc = feeReceipt.IssuingRuc,
                DebtorRuc = feeReceipt.DebtorRuc,
                IssueDate = feeReceipt.IssueDate.ToLocalDateFormat(),
                DiscountDate = feeReceipt.DiscountDate.ToLocalDateFormat(),
                PaymentDate = feeReceipt.PaymentDate.ToLocalDateFormat(),
                TotalAmmount = feeReceipt.TotalAmmount,
                Withholding = feeReceipt.Withholding,
                BankId = feeReceipt.BankId,
                Bank = feeReceipt.Bank != null ?
                    new BankViewModel
                    {
                        Name = feeReceipt.Bank.Name,
                        ShortName = feeReceipt.Bank.ShortName,
                        RateTerm = feeReceipt.Bank.RateTerm,
                        RateType = feeReceipt.Bank.RateType,
                        RateValue = feeReceipt.Bank.RateValue
                    } : null,
                DaysDiff = feeReceipt.DaysDiff,
                TermEffectiveRate = feeReceipt.TermEffectiveRate,
                TermDiscountRate = feeReceipt.TermDiscountRate,
                Discount = feeReceipt.Discount,
                NetAmmount = feeReceipt.NetAmmount,
                TotalInitialCost = feeReceipt.TotalInitialCost,
                TotalFinalCost = feeReceipt.TotalFinalCost,
                ReceivedAmmount = feeReceipt.ReceivedAmmount,
                DeliveredAmmount = feeReceipt.DeliveredAmmount,
                TotalAnualEffectiveCost = feeReceipt.TotalAnualEffectiveCost
            };
            return model;
        }

        public static FeeReceipt ToModel(this FeeReceiptViewModel model, Bank bank, ApplicationUser user)
        {
            var discountDate = model.DiscountDate.ToUtcDateTime();
            var paymentDate = model.PaymentDate.ToUtcDateTime();
            var feeReceipt = new FeeReceipt
            {
                IssuingRuc = model.IssuingRuc,
                DebtorRuc = model.DebtorRuc,
                IssueDate = model.IssueDate.ToUtcDateTime(),
                DiscountDate = discountDate,
                PaymentDate = paymentDate,
                TotalAmmount = model.TotalAmmount,
                Withholding = model.Withholding,
                BankId = model.BankId,
                Bank = bank,
                CurrencyTypeId = user.CurrencyTypeId,
                CreateDate = DateTime.UtcNow,
                TotalInitialCost = bank.BankCosts.Where(x => !x.IsFinal).Sum(x => x.Value.ToCurrencyType(user.CurrencyType)),
                TotalFinalCost = bank.BankCosts.Where(x => x.IsFinal).Sum(x => x.Value.ToCurrencyType(user.CurrencyType)),
                TermEffectiveRate = bank.RateValue.ToRate(bank.RateType, ConstantHelpers.Rate.Type.EFFECTIVE, bank.RateTerm, (paymentDate - discountDate).Days),
                UserId = user.Id
            };

            return feeReceipt;
        }
    }
}
