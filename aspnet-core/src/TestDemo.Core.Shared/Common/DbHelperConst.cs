using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Common
{
    public class DbHelperConst
    {
        //Table Names
        public const string TB_CalibrationInputPdCrDr = "CalibrationInput_PD_CR_DR";
        public const string TB_CalibrationInputBehaviouralTerm = "CalibrationInput_EAD_Behavioural_Terms";
        public const string TB_CalibrationInputCcfSummary = "CalibrationInput_EAD_CCF_Summary";
        public const string TB_CalibrationInputHaircut = "CalibrationInput_LGD_HairCut";
        public const string TB_CalibrationInputRecoveryRate = "CalibrationInput_LGD_RecoveryRate";
        public const string TB_CalibrationInputMacroeconomicData = "MacroenonomicData";
        public const string TB_EclLoanBookWholesale = "WholesaleEclDataLoanBooks";
        public const string TB_EclLoanBookRetail = "RetailEclDataLoanBooks";
        public const string TB_EclLoanBookObe = "ObeEclDataLoanBooks";
        public const string TB_EclPaymentScheduleWholesale = "WholesaleEclDataPaymentSchedules";
        public const string TB_EclPaymentScheduleRetail = "RetailEclDataPaymentSchedules";
        public const string TB_EclPaymentScheduleObe = "ObeEclDataPaymentSchedules";
        public const string TB_EclAssetbookInvestment = "InvestmentAssetBooks";
        public const string TB_TrackEclDataLoanBookException = "TrackEclDataLoanBookException";
        public const string TB_TrackCalibrationPdCrDrException = "TrackCalibrationPdCrDrException";
        public const string TB_TrackCalibrationBehaviouralTermException = "TrackCalibrationBehaviouralTermException";


        //Column Names
        public const string COL_CalibrationId = "CalibrationId";
        public const string COL_MacroId = "MacroId";
        public const string COL_WholesaleEclUploadId = "WholesaleEclUploadId";
        public const string COL_RetailEclUploadId = "RetailEclUploadId";
        public const string COL_ObeEclUploadId = "ObeEclUploadId";
        public const string COL_InvestmentEclUploadId = "InvestmentEclUploadId";
        public const string COL_EclId = "EclId";
    }
}
