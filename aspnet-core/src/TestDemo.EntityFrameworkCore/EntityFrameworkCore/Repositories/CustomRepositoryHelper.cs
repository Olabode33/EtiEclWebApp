using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.EntityFrameworkCore.Repositories
{
    public static class CustomRepositoryHelper
    {
        public static string GetEnumValue(string propertyType, string value)
        {
            string convertdValue = value;
            int enumIntValue = -1;
            switch (propertyType)
            {
                case "TestDemo.EclShared.PdInputAssumptionGroupEnum":
                    enumIntValue = Convert.ToInt32(value);
                    convertdValue = ((PdInputAssumptionGroupEnum)enumIntValue).ToString();
                    break;

                case "TestDemo.EclShared.AssumptionGroupEnum":
                    enumIntValue = Convert.ToInt32(value);
                    convertdValue = ((AssumptionGroupEnum)enumIntValue).ToString();
                    break;

                case "TestDemo.EclShared.CalibrationStatusEnum":
                    enumIntValue = Convert.ToInt32(value);
                    convertdValue = ((CalibrationStatusEnum)enumIntValue).ToString();
                    break;

                case "TestDemo.EclShared.LdgInputAssumptionGroupEnum":
                    enumIntValue = Convert.ToInt32(value);
                    convertdValue = ((LdgInputAssumptionGroupEnum)enumIntValue).ToString();
                    break;

                case "TestDemo.EclShared.UploadDocTypeEnum":
                    enumIntValue = Convert.ToInt32(value);
                    convertdValue = ((UploadDocTypeEnum)enumIntValue).ToString();
                    break;

                case "TestDemo.EclShared.GeneralStatusEnum":
                    enumIntValue = Convert.ToInt32(value);
                    convertdValue = ((GeneralStatusEnum)enumIntValue).ToString();
                    break;

                case "TestDemo.EclShared.EadInputAssumptionGroupEnum":
                    enumIntValue = Convert.ToInt32(value);
                    convertdValue = ((EadInputAssumptionGroupEnum)enumIntValue).ToString();
                    break;

                case "TestDemo.EclShared.AssumptionTypeEnum":
                    enumIntValue = Convert.ToInt32(value);
                    convertdValue = ((AssumptionTypeEnum)enumIntValue).ToString();
                    break;

                case "TestDemo.EclShared.EclStatusEnum":
                    enumIntValue = Convert.ToInt32(value);
                    convertdValue = ((EclStatusEnum)enumIntValue).ToString();
                    break;

                case "TestDemo.EclShared.FrameworkEnum":
                    enumIntValue = Convert.ToInt32(value);
                    convertdValue = ((FrameworkEnum)enumIntValue).ToString();
                    break;

                case "TestDemo.EclShared.DataTypeEnum":
                    enumIntValue = Convert.ToInt32(value);
                    convertdValue = ((DataTypeEnum)enumIntValue).ToString();
                    break;

                default:
                    break;
            }

            return convertdValue;
        }
    }
}
