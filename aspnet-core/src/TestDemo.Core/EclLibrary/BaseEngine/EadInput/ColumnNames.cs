using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD_Inputs_Automation
{
    class ColumnNames
    {
        public const string currency = "CURRENCY";
        public const string contract_no = "CONTRACT_NO";
        public const string segment = "SEGMENT";
        public const string product_type = "PRODUCT_TYPE";
        public const string contract_strt_dt = "CONTRACT_START_DATE";
        public const string contract_end_dt = "CONTRACT_END_DATE";
        public const string credit_limit_lcy = "CREDIT_LIMIT_LCY";
        public const string original_bal_lcy = "ORIGINAL_BALANCE_LCY";
        public const string outstanding_bal_lcy = "OUTSTANDING_BALANCE_LCY";
        public const string debenture_omv = "DEBENTURE_OMV";
        public const string cash_omv = "CASH_OMV";
        public const string inventory_omv = "INVENTORY_OMV";
        public const string plant_and_equipment_omv = "PLANT_AND_EQUIPMENT_OMV";
        public const string residential_property_omv = "RESIDENTIAL_PROPERTY_OMV";
        public const string commercial_property_omv = "COMMERCIAL_PROPERTY_OMV";
        public const string receivables_omv = "RECEIVABLES_OMV";
        public const string shares_omv = "SHARES_OMV";
        public const string vehicle_omv = "VEHICLE_OMV";
        public const string guarantee_indicator = "GUARANTEE_INDICATOR";
        public const string restructure_indicator = "RESTRUCTURE_INDICATOR";
        public const string restructure_start_dt = "RESTRUCTURE_START_DATE";
        public const string restructure_end_dt = "RESTRUCTURE_END_DATE";
        public const string ipt_o_period = "IPT_O_PERIOD";
        public const string principal_payment_str = "PRINCIPAL_PAYMENT_STRUCTURE";
        public const string interest_payment_str = "INTEREST_PAYMENT_STRUCTURE";
        public const string base_rate = "BASE_RATE";
        public const string orig_contractual_ir = "ORIGINATION_CONTRACTUAL_INTEREST_RATE";
        public const string introductory_period = "INTRODUCTORY_PERIOD";
        public const string post_ip_contractural_ir = "POST_IP_CONTRACTUAL_INTEREST_RATE";
        public const string interest_rate_type = "INTEREST_RATE_TYPE";
        public const string current_contractual_ir = "CURRENT_CONTRACTUAL_INTEREST_RATE";
        public const string eir = "EIR";


        ///////////LIFETIME EADS///////////////
        public const string start_date = "START_DATE";
        public const string end_date = "END_DATE";
        public const string remaining_ip = "REMAINING_IP";
        public const string revised_base = "REVISED_BASE";
        public const string cir_premium = "CIR_PREMIUM";
        public const string eir_premium = "EIR_PREMIUM";
        public const string cir_base_premium = "CIR_BASE_PREMIUM";
        public const string eir_base_premium = "EIR_BASE_PREMIUM";
        public const string mths_in_force = "MONTHS_IN_FORCE";
        public const string rem_interest_moritorium = "REMAINING_INTEREST_MORITORIUM";
        public const string mths_to_expiry = "MONTHS_TO_EXPIRY";
        public const string interest_divisor = "INTEREST_DIVISOR";
        public const string first_interest_month = "FIRST_INTEREST_MONTH";
        public const string ccf_index = "CCF_INDEX";
        public const string fx_index = "FX_INDEX";
        public const string cir_index = "CIR_INDEX";
        public const string component = "COMPONENT";

        public const string key = "GROUP";

    }
}
