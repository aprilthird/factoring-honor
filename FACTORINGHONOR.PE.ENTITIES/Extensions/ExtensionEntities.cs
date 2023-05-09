using FACTORINGHONOR.PE.CORE.Helpers;
using FACTORINGHONOR.PE.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FACTORINGHONOR.PE.ENTITIES.Extensions
{
    public static class ExtensionEntities
    {
        public static FeeReceipt ToCurrencyType(this FeeReceipt feeReceipt, CurrencyType currencyType)
        {
            if (feeReceipt.CurrencyTypeId == currencyType.Id)
                return feeReceipt;
            feeReceipt.TotalAmmount = feeReceipt.TotalAmmount.ToCurrencyType(feeReceipt.CurrencyType, currencyType);
            feeReceipt.Withholding = feeReceipt.Withholding.ToCurrencyType(feeReceipt.CurrencyType, currencyType);
            feeReceipt.TotalInitialCost = feeReceipt.TotalInitialCost.ToCurrencyType(feeReceipt.CurrencyType, currencyType);
            feeReceipt.TotalFinalCost = feeReceipt.TotalFinalCost.ToCurrencyType(feeReceipt.CurrencyType, currencyType);
            return feeReceipt;
        }

        public static double ToCurrencyType(this double number, CurrencyType currentCurrencyType, CurrencyType currencyType)
        {
            return number * currentCurrencyType.Value / currencyType.Value;
        }

        public static double ToRate(this double rate, int currentRateType, int rateType, int currentTermDays, int termDays, int cap = 1)
        {
            if(currentRateType == rateType)
            {
                if(currentRateType == ConstantHelpers.Rate.Type.EFFECTIVE)
                {
                    return Math.Pow(1 + rate, (double)termDays / currentTermDays) - 1;
                }
            }
            if(currentRateType == ConstantHelpers.Rate.Type.EFFECTIVE
                && rateType == ConstantHelpers.Rate.Type.NOMINAL)
            {
                var m = (double)termDays / cap;
                var n = (double)currentTermDays / cap;
                return (Math.Pow(rate + 1, 1 / n) - 1) * m;
            }
            if (currentRateType == ConstantHelpers.Rate.Type.NOMINAL
                && rateType == ConstantHelpers.Rate.Type.EFFECTIVE)
            {
                var m = (double)currentTermDays / cap;
                var n = (double)termDays / cap;
                return Math.Pow(1 + rate / m, n) - 1;
            }
            return rate;
        }

        public static double FromAnualNominalToTermEffective(this double nominalRate, int termDays)
        {
            return nominalRate.ToRate(ConstantHelpers.Rate.Type.NOMINAL, ConstantHelpers.Rate.Type.EFFECTIVE, ConstantHelpers.Bank.COMMERCIAL_YEAR, termDays);
        }

        public static double FromAnualEffectiveToTermEffective(this double effectiveRate, int termDays)
        {
            return effectiveRate.ToRate(ConstantHelpers.Rate.Type.EFFECTIVE, ConstantHelpers.Rate.Type.EFFECTIVE, ConstantHelpers.Bank.COMMERCIAL_YEAR, termDays);
        }

        public static double FromAnualEffectiveToTermNominal(this double effectiveRate, int termDays)
        {
            return effectiveRate.ToRate(ConstantHelpers.Rate.Type.EFFECTIVE, ConstantHelpers.Rate.Type.NOMINAL, ConstantHelpers.Bank.COMMERCIAL_YEAR, termDays);
        }

        //public static double ToAnualEffectiveRate(this double rate, int currentRateType)
        //{
        //    if (currentRateType == ConstantHelpers.Rate.Type.EFFECTIVE)
        //        return rate;
        //    if (currentRateType == ConstantHelpers.Rate.Type.NOMINAL)
        //        return rate.FromAnualNominalToTermEffective(ConstantHelpers.Bank.COMMERCIAL_YEAR);
        //    return rate;
        //}

        //public static double ToEffectiveRate(this double rate, int currentRateType, int termDays)
        //{
        //    if (currentRateType == ConstantHelpers.Rate.Type.EFFECTIVE)
        //        return rate;
        //    if (currentRateType == ConstantHelpers.Rate.Type.NOMINAL)
        //        return rate.FromAnualNominalToTermEffective(termDays);
        //    return rate;
        //}
    }
}
