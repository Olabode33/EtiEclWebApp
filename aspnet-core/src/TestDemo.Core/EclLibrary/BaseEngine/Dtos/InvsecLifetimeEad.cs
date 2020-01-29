using EclEngine.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.InvestmentInputs;

namespace TestDemo.EclLibrary.Investment.Dtos
{
    public class InvsecLifetimeEad: InvestmentAssetBook
    {
        public InvsecLifetimeEad(InvestmentAssetBook assetBook)
        {
            Id = assetBook.Id;
            AssetDescription = assetBook.AssetDescription;
            AssetType = assetBook.AssetType;
            CounterParty = assetBook.CounterParty;
            SovereignDebt = assetBook.SovereignDebt;
            RatingAgency = assetBook.RatingAgency;
            CreditRatingAtPurchaseDate = assetBook.CreditRatingAtPurchaseDate;
            CurrentCreditRating = assetBook.CurrentCreditRating;
            NominalAmount = assetBook.NominalAmount;
            PrincipalAmortisation = assetBook.PrincipalAmortisation;
            RepaymentTerms = assetBook.RepaymentTerms;
            CarryAmountNGAAP = assetBook.CarryAmountNGAAP;
            CarryingAmountIFRS = assetBook.CarryingAmountIFRS;
            Coupon = assetBook.Coupon;
            Eir = assetBook.Eir;
            PurchaseDate = assetBook.PurchaseDate;
            IssueDate = assetBook.IssueDate;
            PurchasePrice = assetBook.PurchasePrice;
            MaturityDate = assetBook.MaturityDate;
            RedemptionPrice = assetBook.RedemptionPrice;
            BusinessModelClassification = assetBook.BusinessModelClassification;
            Ias39Impairment = assetBook.Ias39Impairment;
            PrudentialClassification = assetBook.PrudentialClassification;
            ForebearanceFlag = assetBook.ForebearanceFlag;
        }

        public double? MonthlyInt()
        {
            return Eir == null ? (double?)null : Math.Pow(1 + ((double)Eir / 100), 1.0 / 12.0) - 1;
        }

        public double? TermInForceMonth(DateTime reportDate)
        {
            return PurchaseDate == null ? (double?)null : ExcelFormulaUtil.YearFrac((DateTime)PurchaseDate, reportDate);
        }

        public double? ContractualTermMonths(string assumpedCreditlifetime)
        {
            if (BusinessModelClassification == "Hold to collect")
            {
                var yearFrac = ExcelFormulaUtil.YearFrac((DateTime)PurchaseDate, (DateTime)MaturityDate);
                return yearFrac == 0 ? 1 : yearFrac;
            }
            else
            {
                double o;
                if (double.TryParse(assumpedCreditlifetime, out o))
                {
                    return o;
                } else
                {
                    return null;
                }
            }
        }

        public double? PaymentStartMonth()
        {
            var month = MaturityDate.Value.Month;
            return month - 6 >= 0 ? month - 6 : month;
        }

        public double? PrincipalAmortisationComputed(DateTime reportDate)
        {
            var rate = MonthlyInt();
            var nper = ExcelFormulaUtil.YearFrac((DateTime)MaturityDate, reportDate);
            switch (RepaymentTerms)
            {
                case "Monthly":
                    return -1 * ExcelFormulaUtil.PMT((double)rate, nper * 12, (double)CarryingAmountIFRS, 0);
                case "Quarterly":
                    return -1 * ExcelFormulaUtil.PMT((double)rate * 3, nper * 3, (double)CarryingAmountIFRS, 0);
                case "Half-Yearly":
                    return -1 * ExcelFormulaUtil.PMT((double)rate * 6, nper * 6, (double)CarryingAmountIFRS, 0);
                case "Yearly":
                    return -1 * ExcelFormulaUtil.PMT((double)rate * 12, nper, (double)CarryingAmountIFRS, 0);
                default:
                    return 0;
            }
        }

        public double CouponComputed()
        {
            switch(PrincipalAmortisation)
            {
                case "Amortisation":
                    return 0;
                default:
                    switch (RepaymentTerms)
                    {
                        case "Monthly":
                            return (double)Coupon * (double)NominalAmount / 12;
                        case "Quarterly":
                            return (double)Coupon * (double)NominalAmount / 4;
                        case "Half-Yearly":
                            return (double)Coupon * (double)NominalAmount / 2;
                        case "Yearly":
                            return 1;
                        default:
                            return (double)Coupon * (double)NominalAmount / 1;
                    }
            }
        }

        public double? Month0()
        {
            return CarryingAmountIFRS;
        }
    }
}
