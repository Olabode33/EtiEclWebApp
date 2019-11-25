using Excel.FinancialFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD_Inputs_Automation
{
    public class EAD_Inputs_Calculation
    {
        public String virProjections = "0.14"; //GOTTEN FROM DB
        public string tempString = String.Empty; //for holding temporary values
        public String Non_Expired = "31";   ///this is called OD_PERFORMACE_PAST_EXPIRY on the excel and it is obtained from the EAD calibration. It will be obtained from the DB
        public String Expired = "22";///this is called EXP_OD_PERFORMACE_PAST_EXPIRY on the excel and it is obtained from the EAD calibration. It will be obtained from the DB
        public double Corporate = 1;///It will be obtained from the DB
        public double Commercial = 1;///It will be obtained from the DB
        public double Consumer = 1;///It will be obtained from the DB
        public double Conversion_Factor_OBE = 1;///It will be obtained from the DB this is in percentage
        public DateTime reportingDate = new DateTime(2016, 12, 31);
        public void EAD_Inputs_START(DataTable oldDt)
        {
            DataTable refinedRawData = Tables.RevisedTable();
            ///Get contract IDs
            ///
            //mainDT = Get_Contract_IDs(mainDT, oldDt);
            refinedRawData = PopulateMain(Get_Contract_IDs(refinedRawData, oldDt), oldDt);
            DataTable lifeTimeEAD_w = LifeTimeEADs_W(refinedRawData);

            /*DataTable distinctValues = new DataView(lifeTimeEAD_w).ToTable(true, ColumnNames.cir_base_premium);
            distinctValues.DefaultView.Sort = ColumnNames.cir_base_premium;
            distinctValues = distinctValues.DefaultView.ToTable();

            DataTable distinctValues2 = new DataView(lifeTimeEAD_w).ToTable(true, ColumnNames.eir_base_premium);
            distinctValues2.DefaultView.Sort = ColumnNames.eir_base_premium;
            distinctValues2 = distinctValues2.DefaultView.ToTable();*/

            DataTable distinctContractID = new DataView(refinedRawData).ToTable(true, ColumnNames.contract_no);
            distinctContractID.DefaultView.Sort = ColumnNames.contract_no;
            distinctContractID = distinctContractID.DefaultView.ToTable();

            //Perform Projections
            var maximumDate = refinedRawData.AsEnumerable().Select(x => Convert.ToDateTime(x.Field<string>(ColumnNames.contract_end_dt))).Max();
            double noOfDays = (maximumDate - reportingDate).Days;
            double noOfMonths = Math.Ceiling(noOfDays * 12 / 365);

            //work on payment schedule
            //let us imagine we have a table with contract ids and their components

            //populate for EIR projections
            string filterValue = VariableNames.filterValue_eir;
            DataTable EIR_Projection = Projections(lifeTimeEAD_w, distinctContractID, noOfMonths, filterValue);

            //populate for CIR projections
            filterValue = VariableNames.filterValue_cir;
            DataTable CIR_Projection = Projections(lifeTimeEAD_w, distinctContractID, noOfMonths, filterValue);

            //populate for Lifetime EAD projections

            filterValue = VariableNames.filterValue_lifetime;
            NewMethod(refinedRawData, lifeTimeEAD_w, distinctContractID, noOfMonths, filterValue, CIR_Projection);

        }

        private void NewMethod(DataTable refinedRawData, DataTable lifeTimeEAD_w, DataTable distinctContractID, double noOfMonths, string filterValue, DataTable CIR_Projection)
        {
            DataTable lifetimeEadInputs = Tables.ProjectionTable(filterValue);
            for (int contractIndex = 0; contractIndex < distinctContractID.Rows.Count; contractIndex++)
            {
                EAD_Inputs obj = new EAD_Inputs()
                {
                    outstanding_balance_lcy = Convert.ToDouble(refinedRawData.Rows[contractIndex][ColumnNames.outstanding_bal_lcy]),
                    product_type = refinedRawData.Rows[contractIndex][ColumnNames.product_type].ToString(),
                    months_to_expiry = Convert.ToDouble(refinedRawData.Rows[contractIndex][ColumnNames.mths_to_expiry]),
                    segment = refinedRawData.Rows[contractIndex][ColumnNames.segment].ToString(),
                    credit_limit_lcy = Convert.ToDouble(refinedRawData.Rows[contractIndex][ColumnNames.credit_limit_lcy]),
                    rem_interest_moritorium = Convert.ToDouble(refinedRawData.Rows[contractIndex][ColumnNames.rem_interest_moritorium]),
                    interest_divisor = refinedRawData.Rows[contractIndex][ColumnNames.interest_divisor].ToString(),
                };

                string contract = distinctContractID.Rows[contractIndex][ColumnNames.contract_no].ToString();
                string eir_group_value = lifeTimeEAD_w.AsEnumerable().Where(x => x.Field<string>(ColumnNames.contract_no) == contract).Select(x => x.Field<string>(ColumnNames.eir_base_premium)).First();
                string cir_group_value = lifeTimeEAD_w.AsEnumerable().Where(x => x.Field<string>(ColumnNames.contract_no) == contract).Select(x => x.Field<string>(ColumnNames.cir_base_premium)).First();

                for (double monthIndex = 0; monthIndex <= noOfMonths; monthIndex++)
                {
                    if (monthIndex == 0)
                    {
                        double value = projection_Calulcation_lifetimeEAD_0(obj.outstanding_balance_lcy, obj.product_type);

                        lifetimeEadInputs.Rows.Add(new object[]
                        {
                            contract,
                            eir_group_value,
                            cir_group_value,
                            monthIndex,
                            value
                        });
                    }
                    else
                    {
                        double overallvalue = 0, value1, value2;
                        double month_0 = lifetimeEadInputs.AsEnumerable().Where(x => x.Field<string>(ColumnNames.months) == VariableNames._month0 && x.Field<string>(ColumnNames.contract_no) == contract)
                                        .Select(x => x.Field<double>(ColumnNames.value)).First();

                        if (obj.product_type != VariableNames._productType_loan && obj.product_type != VariableNames._productType_lease & obj.product_type != VariableNames._productType_mortgage)
                        {
                            if (monthIndex <= obj.months_to_expiry)
                            {
                                if (obj.segment == VariableNames._corporate)
                                {
                                    value1 = obj.outstanding_balance_lcy + Math.Max((obj.credit_limit_lcy - obj.outstanding_balance_lcy) * Corporate, 0);
                                }

                                else if (obj.segment == VariableNames._consumer)
                                {
                                    value1 = obj.outstanding_balance_lcy + Math.Max((obj.credit_limit_lcy - obj.outstanding_balance_lcy) * Convert.ToDouble(Consumer), 0);
                                }
                                else if (obj.segment == VariableNames._commercial)
                                {
                                    value1 = obj.outstanding_balance_lcy + Math.Max((obj.credit_limit_lcy - obj.outstanding_balance_lcy) * Convert.ToDouble(Commercial), 0);
                                }
                                else //OBE
                                {
                                    value1 = obj.outstanding_balance_lcy + Math.Max((obj.credit_limit_lcy - obj.outstanding_balance_lcy) * Convert.ToDouble(Corporate), 0);
                                }

                                if (obj.product_type != VariableNames._productType_od && obj.product_type != VariableNames._productType_card)
                                {
                                    value2 = Convert.ToDouble(Conversion_Factor_OBE);
                                }
                                else
                                {
                                    value2 = 1;
                                }

                                overallvalue = value1 * value2;
                            }
                        }
                        else
                        {
                            string component = ""; //this should be obtained from the payment schedule
                            double d_value;
                            if (monthIndex <= obj.months_to_expiry)
                            {
                                double c_value = CIR_Projection.AsEnumerable().Where(x => x.Field<string>(ColumnNames.cir_group) == cir_group_value && x.Field<string>(ColumnNames.months) == (monthIndex + 1).ToString()).
                                                Select(x => x.Field<double>(ColumnNames.cir_effective)).First();
                                if (component != VariableNames._amortise)
                                {
                                    int a_value = (monthIndex > obj.rem_interest_moritorium || obj.rem_interest_moritorium == 0) ? 1 : 0;
                                    int b_value = (obj.interest_divisor != "1") ? 1 : 0;

                                    d_value = a_value * b_value * c_value * month_0;
                                }
                                else
                                {
                                    d_value = c_value * month_0;
                                }

                                double e_value = month_0 + d_value; // - ('Payment Schedule'!D4*INDEX(FX_PROJECTIONS, $L4, T$3+1)

                                if (obj.interest_divisor == VariableNames._interestDivisior)
                                {
                                    //x = ($H4=T$3)*SUMPRODUCT(OFFSET(T4, 0, -1, 1, -T$3), OFFSET(CIR_EFF_MONTHLY_RANGE, $M4-1, T$3, 1, -T$3))*($H4+$G4)/T$3

                                }
                                else
                                {

                                }
                            }
                            else
                            {

                            }
                        }

                        lifetimeEadInputs.Rows.Add(new object[]
                        {
                            contract,
                            eir_group_value,
                            cir_group_value,
                            monthIndex,
                            overallvalue
                        });
                    }
                }

            }
        }

        private DataTable Projections (DataTable lifeTimeEAD_w, DataTable distinctContractID, double noOfMonths, string filterValue)
        {
            DataTable projectionTable = Tables.ProjectionTable(filterValue);
            for (int contractIndex = 0; contractIndex < distinctContractID.Rows.Count; contractIndex++)
            {
                string contract = distinctContractID.Rows[contractIndex][ColumnNames.contract_no].ToString();
                string group_value;
                if (filterValue == VariableNames.filterValue_eir)
                {
                    group_value = lifeTimeEAD_w.AsEnumerable().Where(x => x.Field<string>(ColumnNames.contract_no) == contract).Select(x => x.Field<string>(ColumnNames.eir_base_premium)).First();
                }
                else
                {
                    group_value = lifeTimeEAD_w.AsEnumerable().Where(x => x.Field<string>(ColumnNames.contract_no) == contract).Select(x => x.Field<string>(ColumnNames.cir_base_premium)).First();
                }

                for (double monthIndex = 0; monthIndex < noOfMonths; monthIndex++)
                {
                    double value;
                    if (group_value == VariableNames.expired)
                    {
                        value = 0;
                    }
                    else
                    {
                        var temp = group_value.Split(VariableNames._splitValue);
                        if (temp[1] != VariableNames._fixed)
                        {
                            value = Math.Round((Convert.ToDouble(virProjections) * 100) + Convert.ToDouble(temp[2].Substring(0, temp[2].Length - 1)), 1) / 100;
                        }
                        else
                        {
                            value = Convert.ToDouble(virProjections)/100;
                        }
                    }

                    if (filterValue == VariableNames.filterValue_cir) //calculate the cir effective
                    {
                        double effectiveValue, power = Convert.ToDouble(1m/12m);
                        //(1 + C$) ^ (1/12)-1
                        effectiveValue = Math.Pow(1 + value, power) - 1;
                        projectionTable.Rows.Add(new object[]
                        {
                            group_value,
                            monthIndex + 1,
                            value,
                            effectiveValue
                        });
                    }
                    else
                    {
                        projectionTable.Rows.Add(new object[]
                        {
                            group_value,
                            monthIndex + 1,
                            value
                        });
                    }
                }
            }
            return projectionTable;
        }

        private double projection_Calulcation_lifetimeEAD_0(double outstanding_bal_lcy, string product_type)
        {
            double value;
            if (product_type != VariableNames._productType_loan && product_type != VariableNames._productType_lease && product_type != VariableNames._productType_mortgage && product_type != VariableNames._productType_od && product_type != VariableNames._productType_card)
            {
                value = Conversion_Factor_OBE;
            }
            else
            {
                value = 1;
            }
            value = outstanding_bal_lcy * value;

            return value;
        }

       /* private static double projection_Calulcation_lifetimeEAD_1(double interest_divisior, double remaining_ir_mom, string component, double month_0 ,string segment, double outstanding_bal_lcy, string product_type, string columnNo, double mths_2_expiry, double credit_limit_lcy)
        {///for compoment please do not forget
            //= IF(AND($C4 <> "LOAN", $C4 <> "LEASE", $C4 <> "MORTGAGE"), IF(T$3 <=$H4, ($E4 + MAX(($D4 -$E4) * INDEX(CCF, $K4, T$3), 0)) * IF(AND($C4 <> "OD", $C4 <> "CARD"), CONVERSION_FACTOR_OBE, 1), 0),
            //IF(T$3 <=$H4, MAX(S4 + IF($N4 <> "AMORTISE", OR(T$3 >$F4, $F4 = "") * ($I4 <> 1) * INDEX(CIR_EFF_MONTHLY_RANGE, $M4, 1 + T$3) * S4, INDEX(CIR_EFF_MONTHLY_RANGE, $M4, 1 + T$3) * S4)
            //- ('Payment Schedule'!D4 * INDEX(FX_PROJECTIONS, $L4, T$3 + 1) + IF($I4 = "B", ($H4 = T$3) * SUMPRODUCT(OFFSET(T4, 0, -1, 1, -T$3), OFFSET(CIR_EFF_MONTHLY_RANGE, $M4 - 1, T$3, 1, -T$3)) * ($H4 +$G4) / T$3,($N4 <> "AMORTISE")*
            //(MOD((T$3 -$J4),$I4)= 0)*(T$3 >$F4)*IF(T$3 <$I4, SUMPRODUCT(OFFSET(T4, 0, -1, 1, -T$3), OFFSET(CIR_EFF_MONTHLY_RANGE, $M4 - 1, T$3, 1, -T$3)) *$I4 / T$3, SUMPRODUCT(OFFSET(T4, 0, -1, 1, -$I4), OFFSET(CIR_EFF_MONTHLY_RANGE, $M4 - 1, T$3, 1, -$I4))))), 0) *(1 - PPF),0))
            
            double value, value1, value2;
            double mths_to_expiry = Convert.ToDouble(mths_2_expiry);
            if (product_type != "LOAN" && product_type != "LEASE" && product_type != "MORTGAGE")
            {
                if (Convert.ToDouble(columnNo) <= mths_to_expiry)
                {
                    if (segment == "CORPORATE")
                    {
                        value1 = outstanding_bal_lcy + Math.Max((credit_limit_lcy - outstanding_bal_lcy) * Convert.ToDouble(Corporate), 0);
                    }

                    else if (segment == "CONSUMER")
                    {
                        value1 = outstanding_bal_lcy + Math.Max((credit_limit_lcy - outstanding_bal_lcy) * Convert.ToDouble(Consumer), 0);
                    }
                    else if (segment == "COMMERCIAL")
                    {
                        value1 = outstanding_bal_lcy + Math.Max((credit_limit_lcy - outstanding_bal_lcy) * Convert.ToDouble(Commercial), 0);
                    }
                    else //OBE
                    {
                        value1 = outstanding_bal_lcy + Math.Max((credit_limit_lcy - outstanding_bal_lcy) * Convert.ToDouble(Corporate), 0);
                    }

                    if (product_type != "OD" && product_type != "CARD")
                    {
                        value2 = Convert.ToDouble(Conversion_Factor_OBE);
                    }
                    else
                    {
                        value2 = 1;
                    }

                    value = value1 * value2;
                }
                else
                {
                    value = 0;
                }
            }
            else
            {
                if (Convert.ToDouble(columnNo) <= mths_to_expiry)
                {
                    if (component != "AMORTISE")
                    {
                        int a_value = (Convert.ToDouble(columnNo) > remaining_ir_mom || remaining_ir_mom == 0) ? 1 : 0;
                        int b_value = (interest_divisior != 1) ? 1 : 0;
                    }
                    value = Convert.ToDouble(month_0);
                }
            }
            value = outstanding_bal_lcy * value;

            return value;
        }
        */
        public virtual DataTable Get_Contract_IDs(DataTable maindt, DataTable oldDT)
        {
            for (int i =0; i < oldDT.Rows.Count; i++)
            {
                maindt.Rows.Add();
                string contract_start_dt = oldDT.Rows[i][ColumnNames.contract_strt_dt].ToString();
                string contract_end_dt = oldDT.Rows[i][ColumnNames.contract_end_dt].ToString();
                string credit_limit_lcy = oldDT.Rows[i][ColumnNames.credit_limit_lcy].ToString();
                string original_bal_lcy = oldDT.Rows[i][ColumnNames.original_bal_lcy].ToString();

                if (String.IsNullOrEmpty(contract_start_dt) && String.IsNullOrEmpty(contract_end_dt) && credit_limit_lcy == "0" && original_bal_lcy == "0")
                {
                    //add value within a column
                    double columnSum = Convert.ToDouble(oldDT.Rows[i][ColumnNames.debenture_omv].ToString()) + Convert.ToDouble(oldDT.Rows[i][ColumnNames.cash_omv].ToString()) + Convert.ToDouble(oldDT.Rows[i][ColumnNames.inventory_omv].ToString())
                                        + Convert.ToDouble(oldDT.Rows[i][ColumnNames.plant_and_equipment_omv].ToString()) + Convert.ToDouble(oldDT.Rows[i][ColumnNames.residential_property_omv].ToString()) + Convert.ToDouble(oldDT.Rows[i][ColumnNames.commercial_property_omv].ToString())
                                        + Convert.ToDouble(oldDT.Rows[i][ColumnNames.receivables_omv].ToString()) + Convert.ToDouble(oldDT.Rows[i][ColumnNames.shares_omv].ToString()) + Convert.ToDouble(oldDT.Rows[i][ColumnNames.vehicle_omv].ToString())
                                        + Convert.ToDouble(oldDT.Rows[i][ColumnNames.guarantee_indicator].ToString());
                    /*  for (int j = 28; j < oldDT.Columns.Count; j++)
                      {
                          columnSum += Convert.ToDouble(oldDT.Rows[i][j]);
                      }
                      */
                    if (columnSum == 0)
                    {
                        maindt.Rows[i][ColumnNames.contract_no] = "EXP " + oldDT.Rows[i][ColumnNames.product_type].ToString() + "|" + oldDT.Rows[i][ColumnNames.segment].ToString();
                    }
                    else
                    {
                        maindt.Rows[i][ColumnNames.contract_no] = "EXP " + oldDT.Rows[i][ColumnNames.product_type].ToString() + "|" + oldDT.Rows[i][ColumnNames.contract_no].ToString();
                    }
                }   
                else
                {
                    maindt.Rows[i][ColumnNames.contract_no] = oldDT.Rows[i][ColumnNames.contract_no];
                }

            }
            return maindt;
        }

        public virtual DataTable PopulateMain(DataTable mainDT, DataTable oldDT)
        {
            ////
            //populate the mainDT (this is the revised table)
            for (int i = 0; i < oldDT.Rows.Count; i++)
            {
                string Contract_No = mainDT.Rows[i][ColumnNames.contract_no].ToString();
                var getValuefromContractNo = Contract_No.Substring(Contract_No.IndexOf('|') + 1, Contract_No.Length - Contract_No.IndexOf('|') - 1);
                var checkNumber = int.TryParse(getValuefromContractNo, out int n);
                
                var query = oldDT.AsEnumerable().Where(x => x.Field<string>(ColumnNames.contract_no) == mainDT.Rows[i][ColumnNames.contract_no].ToString());
                if (Contract_No.Substring(0,3) == VariableNames.expired && !checkNumber)
                {
                    
                    /*mainDT.Rows[i][ColumnNames.segment] = String.Empty;
                    mainDT.Rows[i][ColumnNames.currency] = String.Empty;
                    mainDT.Rows[i][ColumnNames.product_type] = String.Empty;
                    mainDT.Rows[i][ColumnNames.contract_strt_dt] = String.Empty;
                    mainDT.Rows[i][ColumnNames.contract_end_dt] = String.Empty;
                    mainDT.Rows[i][ColumnNames.restructure_indicator] = String.Empty;
                    mainDT.Rows[i][ColumnNames.restructure_start_dt] = String.Empty;
                    mainDT.Rows[i][ColumnNames.restructure_end_dt] = String.Empty;
                    mainDT.Rows[i][ColumnNames.ipt_o_period] = String.Empty;
                    mainDT.Rows[i][ColumnNames.principal_payment_str] = String.Empty;
                    mainDT.Rows[i][ColumnNames.ipt_o_period] = String.Empty;
                    mainDT.Rows[i][ColumnNames.ipt_o_period] = String.Empty;*/
                    //////////
                    /////this is speaking to the column Product type. Provided the loan is expired, we attempt to extract the type of loan from the value within the cell
                    ///////
                    ///What i have done is get the value from cell using linq query, then i obtain the index of space and pipe within the value. I use this position to substring the exact value i need
                    ///
                  /*  string temp = oldDT.AsEnumerable().Where(x => x.Field<string>(ColumnNames.contract_no) == oldDT.Rows[i][ColumnNames.contract_no].ToString())
                                                   .Select(x => x.Field<string>(ColumnNames.product_type))
                                                   .FirstOrDefault();*/
                    int position1 = Contract_No.IndexOf(' ');
                    int position2 = Contract_No.IndexOf('|');
                    mainDT.Rows[i][ColumnNames.product_type] = Contract_No.Substring(position1 + 1, position2 - position1 - 1);

                    mainDT.Rows[i][ColumnNames.credit_limit_lcy] = query.Select(x => Convert.ToDouble(x.Field<string>(ColumnNames.credit_limit_lcy))).Sum();
                    mainDT.Rows[i][ColumnNames.original_bal_lcy] = query.Select(x => Convert.ToDouble(x.Field<string>(ColumnNames.original_bal_lcy))).Sum();
                    mainDT.Rows[i][ColumnNames.outstanding_bal_lcy] = query.Select(x => Convert.ToDouble(x.Field<string>(ColumnNames.outstanding_bal_lcy))).Sum();

                }
                else
                {

                    mainDT.Rows[i][ColumnNames.segment] = query.Select(x => x.Field<string>(ColumnNames.segment)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.currency] = query.Select(x => x.Field<string>(ColumnNames.currency)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.product_type] = query.Select(x => x.Field<string>(ColumnNames.product_type)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.credit_limit_lcy] = query.Select(x => x.Field<string>(ColumnNames.credit_limit_lcy)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.original_bal_lcy] = query.Select(x => x.Field<string>(ColumnNames.original_bal_lcy)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.outstanding_bal_lcy] = query.Select(x => x.Field<string>(ColumnNames.outstanding_bal_lcy)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.contract_strt_dt] = query.Select(x => x.Field<string>(ColumnNames.contract_strt_dt)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.contract_end_dt] = query.Select(x => x.Field<string>(ColumnNames.contract_end_dt)).FirstOrDefault(); 
                    mainDT.Rows[i][ColumnNames.restructure_indicator] = query.Select(x => x.Field<string>(ColumnNames.restructure_indicator)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.restructure_start_dt] = query.Select(x => x.Field<string>(ColumnNames.restructure_start_dt)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.restructure_end_dt] = query.Select(x => x.Field<string>(ColumnNames.restructure_end_dt)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.ipt_o_period] = query.Select(x => x.Field<string>(ColumnNames.ipt_o_period)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.principal_payment_str] = query.Select(x => x.Field<string>(ColumnNames.principal_payment_str)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.interest_payment_str] = query.Select(x => x.Field<string>(ColumnNames.interest_payment_str)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.base_rate] = query.Select(x => x.Field<string>(ColumnNames.base_rate)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.orig_contractual_ir] = query.Select(x => x.Field<string>(ColumnNames.orig_contractual_ir)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.introductory_period] = query.Select(x => x.Field<string>(ColumnNames.introductory_period)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.post_ip_contractural_ir] = query.Select(x => x.Field<string>(ColumnNames.post_ip_contractural_ir)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.interest_rate_type] = query.Select(x => x.Field<string>(ColumnNames.interest_rate_type)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.current_contractual_ir] = query.Select(x => x.Field<string>(ColumnNames.current_contractual_ir)).FirstOrDefault();
                    mainDT.Rows[i][ColumnNames.eir] = query.Select(x => x.Field<string>(ColumnNames.eir)).FirstOrDefault();
                }
               
            }
            return mainDT;
        }

        public virtual DataTable LifeTimeEADs_W(DataTable mainDT)
        {
            ///end DB
            DataTable lifetimeEAD_w = Tables.LifeTimeEADs();
            for (int i = 0; i < mainDT.Rows.Count; i++)
            {
                lifetimeEAD_w.Rows.Add();
                ///start date
                EAD_Inputs obj = new EAD_Inputs()
                {
                    restructure_start_dt = mainDT.Rows[i][ColumnNames.restructure_start_dt].ToString(),
                    restructure_end_dt = mainDT.Rows[i][ColumnNames.restructure_end_dt].ToString(),
                    restructure_indicator = mainDT.Rows[i][ColumnNames.restructure_indicator].ToString(),
                    contract_start_dt = mainDT.Rows[i][ColumnNames.contract_strt_dt].ToString(),
                    contract_end_dt = mainDT.Rows[i][ColumnNames.contract_end_dt].ToString(),
                    introductory_period = mainDT.Rows[i][ColumnNames.introductory_period].ToString(),
                    //start_date = mainDT.Rows[i][ColumnNames.start_date].ToString(),
                    contract_no = mainDT.Rows[i][ColumnNames.contract_no].ToString(),
                    interest_rate_type = mainDT.Rows[i][ColumnNames.interest_rate_type].ToString(),
                    base_rate = mainDT.Rows[i][ColumnNames.base_rate].ToString(),
                    current_contractual_ir = mainDT.Rows[i][ColumnNames.current_contractual_ir].ToString(),
                    post_ip_contractural_ir = mainDT.Rows[i][ColumnNames.post_ip_contractural_ir].ToString(),
                    segment = mainDT.Rows[i][ColumnNames.segment].ToString(),
                    credit_limit_lcy = Convert.ToDouble(mainDT.Rows[i][ColumnNames.credit_limit_lcy])
                };

                lifetimeEAD_w.Rows[i][ColumnNames.contract_no] = obj.contract_no;
                lifetimeEAD_w.Rows[i][ColumnNames.segment] = obj.segment;
                lifetimeEAD_w.Rows[i][ColumnNames.credit_limit_lcy] = obj.credit_limit_lcy;

                lifetimeEAD_w.Rows[i][ColumnNames.start_date] = S_E_Date(obj.restructure_start_dt, obj.restructure_indicator, obj.contract_start_dt);

                ///end date
                lifetimeEAD_w.Rows[i][ColumnNames.end_date] = S_E_Date(obj.restructure_end_dt, obj.restructure_indicator, obj.contract_end_dt);

                //remaining IP
                lifetimeEAD_w.Rows[i][ColumnNames.remaining_ip] = Remaining_IP(obj, reportingDate);
                
                if (obj.contract_no.Substring(0, 3) == VariableNames.expired)
                {
                    lifetimeEAD_w.Rows[i][ColumnNames.revised_base] = VariableNames.expired;
                    lifetimeEAD_w.Rows[i][ColumnNames.cir_premium] = String.Empty;
                    lifetimeEAD_w.Rows[i][ColumnNames.cir_base_premium] = VariableNames.expired;
                    lifetimeEAD_w.Rows[i][ColumnNames.eir_base_premium] = VariableNames.expired;
                    lifetimeEAD_w.Rows[i][ColumnNames.mths_in_force] = String.Empty;
                    lifetimeEAD_w.Rows[i][ColumnNames.mths_to_expiry] = "0";
                }
                else
                {
                    ////populate revised base 
                    lifetimeEAD_w.Rows[i][ColumnNames.revised_base] = Revised_Base(obj.interest_rate_type, obj.base_rate);

                    ///start CIR/EIRpremium
                    var AA_Value = (lifetimeEAD_w.Rows[i][ColumnNames.remaining_ip].ToString() == "0") ? obj.current_contractual_ir : obj.post_ip_contractural_ir;
                    lifetimeEAD_w.Rows[i][ColumnNames.cir_premium] = CIR_EIR_Premium(lifetimeEAD_w.Rows[i][ColumnNames.revised_base].ToString(), AA_Value.ToString());
                    lifetimeEAD_w.Rows[i][ColumnNames.eir_premium] = CIR_EIR_Premium(lifetimeEAD_w.Rows[i][ColumnNames.revised_base].ToString(), mainDT.Rows[i][ColumnNames.eir].ToString());
                    //end CIR/EIR premium
                       

                    ///start CIR Base Premium 
                    ///=IF(LEFT($B5, 3) = VariableNames.expired, VariableNames.expired, "CIR"& IF(AA5<>"", "(" & $AA5 & "RIP@" & ROUND(((1+$R5/12)^12-1)*100, 1) & "%)_", "_")& 
                    ///$AB5 & "_" & IF(AC5<0, ROUND($AC5*100, 1) & "%", "+" & ROUND($AC5*100, 1) & "%"))
                    ///
                  /*  double value1, value2 = 0;
                    string concatenateValue1 = "CIR";
                    string concatenateValue2 = String.Empty;
                    string concatenateValue3 = String.Empty;
                    if (lifetimeEAD_w.Rows[i][ColumnNames.remaining_ip].ToString() != "0")
                    {
                        string temp = mainDT.Rows[i][ColumnNames.orig_contractual_ir].ToString();
                        value1 = (String.IsNullOrEmpty(temp)) ? 0 : Math.Round((Math.Pow((Convert.ToDouble(temp)/1200) + 1, 12) - 1) * 100, 1);
                        concatenateValue2 = "(" + lifetimeEAD_w.Rows[i][ColumnNames.remaining_ip].ToString() + "RIP@" + value1 + "%)_";
                    }
                    else
                    {
                        concatenateValue2 = "_";
                    }

                    concatenateValue3 = lifetimeEAD_w.Rows[i][ColumnNames.revised_base].ToString() + "_";
                    if (Convert.ToDouble(lifetimeEAD_w.Rows[i][ColumnNames.eir_premium].ToString()) < 0)
                    {
                        value2 = Math.Round(Convert.ToDouble(lifetimeEAD_w.Rows[i][ColumnNames.eir_premium].ToString()) * 100, 1);
                        concatenateValue3 = concatenateValue3 + value2 + "%";
                    }
                    else
                    {
                        value2 = Math.Round(Convert.ToDouble(lifetimeEAD_w.Rows[i][ColumnNames.eir_premium].ToString()) * 100, 1);
                        concatenateValue3 = concatenateValue3 + "+" + value2 + "%";
                    }
                    */
                    lifetimeEAD_w.Rows[i][ColumnNames.cir_base_premium] = CIR_Base_Premium(lifetimeEAD_w.Rows[i][ColumnNames.remaining_ip].ToString(), mainDT.Rows[i][ColumnNames.orig_contractual_ir].ToString(),
                                                                        lifetimeEAD_w.Rows[i][ColumnNames.revised_base].ToString(), lifetimeEAD_w.Rows[i][ColumnNames.eir_premium].ToString());
                    ///end CIR 

                    //start EIR base premium
                    lifetimeEAD_w.Rows[i][ColumnNames.eir_base_premium] = EIR_Base_Premium(lifetimeEAD_w.Rows[i][ColumnNames.revised_base].ToString(), lifetimeEAD_w.Rows[i][ColumnNames.eir_premium].ToString());
                    //end EIR base premium

                    //start Months in Force
                    //=IF(LEFT($B5, 3) = VariableNames.expired, "",ROUND(YEARFRAC($Y5, REPORT_DATE, 1) * 12, 0))
                    //THIS NEEDS TO BE UPDATED///
                    /*var wer = DateTime.ParseExact("07/08/2019", "MM/dd/yyyy", null);
                    var real = lifetimeEAD_w.Rows[i][ColumnNames.start_date].ToString();
                    var vale9 = Convert.ToDateTime(real);
                    var dsfh = new DateTime(2013, 7, 8);
                    var valu11 = Financial.YearFrac(vale9, reportingDate, DayCountBasis.ActualActual);
                    var valu12 = valu11 * 12;
                    var rounds = Math.Round(valu12);*/
                    lifetimeEAD_w.Rows[i][ColumnNames.mths_in_force] = Math.Round(Financial.YearFrac(Convert.ToDateTime(lifetimeEAD_w.Rows[i][ColumnNames.start_date].ToString()), reportingDate, DayCountBasis.ActualActual) * 12, 0);
                    //end Months in Force

                    //REMAINING INTEREST MORITORIUM
                    lifetimeEAD_w.Rows[i][ColumnNames.rem_interest_moritorium] = Remaining_IR(mainDT.Rows[i][ColumnNames.ipt_o_period].ToString(), lifetimeEAD_w.Rows[i][ColumnNames.mths_in_force].ToString());
                    //END

                    //MONTHS TO EXPIRY
                    lifetimeEAD_w.Rows[i][ColumnNames.mths_to_expiry] = Months_To_Expiry(reportingDate, Convert.ToDateTime(lifetimeEAD_w.Rows[i][ColumnNames.end_date].ToString()), mainDT.Rows[i][ColumnNames.product_type].ToString());
                    //END

                    //INTEREST_DIVISOR
                    lifetimeEAD_w.Rows[i][ColumnNames.interest_divisor] = Interest_Divisor(mainDT.Rows[i][ColumnNames.interest_payment_str].ToString());
                    //END

                    //FIRST INTEREST MONTH
                    string interest_divisor = lifetimeEAD_w.Rows[i][ColumnNames.interest_divisor].ToString();
                    double mths_to_expiry = Convert.ToDouble(lifetimeEAD_w.Rows[i][ColumnNames.mths_to_expiry].ToString());
                    double rem_interest_moritorium = Convert.ToDouble(lifetimeEAD_w.Rows[i][ColumnNames.rem_interest_moritorium].ToString());
                    double mths_in_force = Convert.ToDouble(lifetimeEAD_w.Rows[i][ColumnNames.mths_in_force].ToString());
                    double ipt_o_period = Convert.ToDouble(mainDT.Rows[i][ColumnNames.ipt_o_period].ToString());
                    lifetimeEAD_w.Rows[i][ColumnNames.first_interest_month] = First_Interest_Month(interest_divisor, mths_to_expiry, rem_interest_moritorium, mths_in_force, ipt_o_period);
                    //END
                }
            }
            return lifetimeEAD_w; 
        }

        private string S_E_Date(string restructure_dt, string restructure_indicator, string contract_dt)
        {
            string value = String.Empty;
            if (!String.IsNullOrEmpty(restructure_dt) && restructure_indicator == "1")
            {
                value = restructure_dt;
            }
            else
            {
                if (!String.IsNullOrEmpty(contract_dt))
                {
                    value = contract_dt;
                }
            }
            return value;
        }

        private static double Remaining_IP(EAD_Inputs input, DateTime reportingDate)
        {
            //=IF($S5="", "",IF(YEARFRAC($Y5, REPORT_DATE, 0)*12>=$S5,  "",MAX(ROUND($S5-YEARFRAC($Y5, REPORT_DATE, 0)*12, 0), 1)))
            
            double value = 0;
            if (!String.IsNullOrEmpty(input.start_date))
               {
                if (!String.IsNullOrEmpty(input.introductory_period))
                {
                    //calculate yearfrac
                    var yearFrac = Math.Round((Financial.YearFrac(Convert.ToDateTime(input.start_date), reportingDate, 0)) * 12, 5);
                    if (yearFrac < Convert.ToDouble(input.introductory_period))
                    {
                        //MAX(ROUND($S5-YEARFRAC($Y5, REPORT_DATE, 0)*12, 0), 1)
                        value = Math.Max(Math.Round(Convert.ToDouble(input.introductory_period) - (Financial.YearFrac(Convert.ToDateTime(input.start_date), reportingDate, 0) * 12)), 1);
                    }
                }
            }

            return value;
        }

        private static string Revised_Base(string interest_rate_type, string base_rate)
        {
            ///=IF(LEFT($B5, 3) = VariableNames.expired, VariableNames.expired, IF($U5 <> "FLOATING", VariableNames._fixed,  IF($Q5 <> "", $Q5, "MPR")))
            string value = String.Empty;
            if (interest_rate_type != "FLOATING")
            {
                value = VariableNames._fixed;
            }
            else
            {
                if (!String.IsNullOrEmpty(base_rate))
                {
                    value = base_rate;
                }
                else
                {
                    value = "MPR";
                }
            }
            return value;
        }

        private double CIR_EIR_Premium(string L_revisedBase, string AA_Value)
        {
            //// = IF(LEFT($B5, 3) = VariableNames.expired, "",ROUND((((1 + IF($AA5 = "", $V5, $T5) / 12) ^ 12) - 1) - IF($AB5 <> VariableNames._fixed, INDEX(VIR_PROJECTIONS, MATCH($AB5, INDEX(VIR_PROJECTIONS, 0, 1), 1), 2), 0), 3))
            double value1 = (String.IsNullOrEmpty(AA_Value)) ? 0 : Math.Pow((Convert.ToDouble(AA_Value) / 1200) + 1, 12) - 1;
            double value2;

            if (L_revisedBase != VariableNames._fixed)
            {
                value2 = Convert.ToDouble(virProjections);
            }
            else
            {
                value2 = 0;
            }

            return Math.Round(value1 - value2, 3);
        }

        private static string CIR_Base_Premium (string remaining_ip, string orig_contractual_ir, string revised_base, string cir_premium)
        {
            string value;
            double value1, value2 = 0;
            string concatenateValue1 = "CIR";
            string concatenateValue2;
            string concatenateValue3;
            if (remaining_ip != "0") //AA5 <> ""
            {
                value1 = (String.IsNullOrEmpty(orig_contractual_ir)) ? 0 : Math.Round((Math.Pow((Convert.ToDouble(orig_contractual_ir) / 1200) + 1, 12) - 1) * 100, 1);
                concatenateValue2 = (value1 == 0) ? "(" + remaining_ip + "RIP@" + "%)_" : "(" + remaining_ip + "RIP@" + value1 + "%)_";
            }
            else
            {
                concatenateValue2 = "_";
            }

            concatenateValue3 = revised_base + "_";
            if (Convert.ToDouble(cir_premium) < 0)
            {
                value2 = Math.Round(Convert.ToDouble(cir_premium) * 100, 1);
                concatenateValue3 = concatenateValue3 + value2 + "%";
            }
            else
            {
                value2 = Math.Round(Convert.ToDouble(cir_premium) * 100, 1);
                concatenateValue3 = concatenateValue3 + "+" + value2 + "%";
            }

            value = concatenateValue1 + concatenateValue2 + concatenateValue3;

            return value;
        }

        private static string EIR_Base_Premium(string revised_base, string eir_premium)
        {
            //=IF(LEFT($B5, 3) = VariableNames.expired, VariableNames.expired, "EIR_" & $AB5 & "_" & IF(AD5<0, ROUND($AD5*100, 1) & "%", "+" & ROUND($AD5*100, 1) & "%"))
            string value;
            string concatenateValue1 = "EIR_" + revised_base + "_";

            string concatenateValue2;
            if (Convert.ToDouble(eir_premium) < 0)
            {
                concatenateValue2 = Math.Round(Convert.ToDouble(eir_premium) * 100, 1) + "%";
            }
            else
            {
                concatenateValue2 = "+" + Math.Round(Convert.ToDouble(eir_premium) * 100, 1) + "%";
            }

            value = concatenateValue1 + concatenateValue2;

            return value;
        }

        private static double Remaining_IR(string ipt_o_period, string mths_in_force)
        {
            //=IF(AND($N5<>"", $N5<>0), MAX($N5-$AG5, 0), "")
            double value = 0;
            if (!String.IsNullOrEmpty(ipt_o_period) && ipt_o_period != "0")
            {
                value = Math.Max(Convert.ToDouble(ipt_o_period) - Convert.ToDouble(mths_in_force), 0);
            }
            return value;
        }

        private string Interest_Divisor(string ir_payment_struct)
        {
            //=IF($P5="B","B", IF($P5 = "H", 6, IF($P5 = "Y", 12, IF($P5 = "M", 1, IF($P5 = "Q", 3, IF($P5 = "S", "Error",IF($P5 = "", "",)))))))
            string value = String.Empty;
            if (ir_payment_struct == "B")
            {
                value = "B";
            }
            else if (ir_payment_struct == "H")
            {
                value = "6";
            }
            else if (ir_payment_struct == "Y")
            {
                value = "12";
            }
            else if (ir_payment_struct == "M")
            {
                value = "1";
            }
            else if (ir_payment_struct == "Q")
            {
                value = "3";
            }
            else if (ir_payment_struct == "S")
            {
                value = "Error";
            }
            return value;
        }

        private double Months_To_Expiry (DateTime reportingDate, DateTime endDate, string productType)
        {
            //=IF(LEFT($B5, 3) = VariableNames.expired, 0,
            //IF(AND($Z5 < REPORT_DATE, OR($E5 = "OD", $E5 = "CARD")), IF(REPORT_DATE > EOMONTH($Z5, EXP_OD_PERFORMANCE_PAST_EXPIRY), 0, ROUND(YEARFRAC(REPORT_DATE, EOMONTH($Z5, EXP_OD_PERFORMANCE_PAST_EXPIRY), 1) * 12, 0)),
            //MAX(IF(OR($E5 = "OD", $E5 = "CARD"), ROUND(YEARFRAC(REPORT_DATE, EOMONTH($Z5, OD_PERFORMANCE_PAST_EXPIRY), 1) * 12, 0),
            //ROUND(YEARFRAC(REPORT_DATE, EOMONTH($Z5, 0), 1) * 12, 0)),
            //1)))
            double value = 0;
            if (endDate < reportingDate || productType == "OD" || productType == "CARD")
            {
                DateTime EOM = EndOfMonth(endDate, Convert.ToInt32(Expired));
                if (reportingDate > EOM)
                {
                    value = 0;
                }
                else
                {
                    value = Math.Max(Math.Round(Financial.YearFrac(reportingDate, EOM, DayCountBasis.ActualActual) * 12), 0);
                }
            }
            else
            {
                DateTime EOM = EndOfMonth(endDate, Convert.ToInt32(Expired));
            if (productType == "OD" || productType == "CARD")
                {
                    value = Math.Max(Math.Round(Financial.YearFrac(reportingDate, EOM, DayCountBasis.ActualActual) * 12), 0);
                }
            }
            return value;
        }

        private double First_Interest_Month(string interest_divisor, double mths_to_expiry, double rem_interest_moritorium, double mths_in_force, double ipt_o_period)
        {
            double value = 0;
            // = IF(interest_divisor <> "", IF(interest_divisor = "B", mths_to_expiry, iF(rem_interest_moritorium <> "", IF(rem_interest_moritorium > 0, rem_interest_moritorium + interest_divisor,
            //(ROUNDUP((mths_in_force -$N5) /interest_divisor, 0) - (mths_in_force -$N5) /interest_divisor) *interest_divisor),(ROUNDUP((mths_in_force) /interest_divisor, 0)-(mths_in_force)/interest_divisor)*interest_divisor)),"")

            if (!String.IsNullOrEmpty(interest_divisor))
            {
                if (interest_divisor == "B")
                {
                    value = mths_to_expiry;
                }
                else
                {
                    if (rem_interest_moritorium != 0)
                    {
                        if (rem_interest_moritorium > 0)
                        {
                            value = rem_interest_moritorium + Convert.ToDouble(interest_divisor);
                        }
                        else
                        {
                            value = (Math.Ceiling(mths_in_force - ipt_o_period / Convert.ToDouble(interest_divisor)) - (mths_in_force - ipt_o_period) / Convert.ToDouble(interest_divisor)) / Convert.ToDouble(interest_divisor);
                        }
                    }
                    else
                    {
                        value = (Math.Ceiling(mths_in_force / Convert.ToDouble(interest_divisor)) - (mths_in_force) / Convert.ToDouble(interest_divisor)) / Convert.ToDouble(interest_divisor);
                    }
                }
            }
                    
                return value;
        }
                
        private DateTime EndOfMonth(DateTime myDate, int numberOfMonths)
        {
            DateTime startOfMonth = new DateTime(myDate.Year, myDate.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(numberOfMonths).AddDays(-1);
            return endOfMonth;
        }

        private double Offset(DataTable dt, int index, string columnName, double reference, int row, int column, int height = 0, int width = 0)
        {
            double value = 0;
            var q = dt.Rows[index][columnName].ToString(); 
            row = row + index;
            return value;
        }

    }
}
