using Abp.Application.Services.Dto;
using Abp.Data;
using Abp.EntityFrameworkCore;
using Abp.Events.Bus.Entities;
using Abp.Localization;
using Abp.Localization.Sources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestDemo.Auditing.Dto;
using TestDemo.Investment;
using TestDemo.InvestmentComputation;

namespace TestDemo.EntityFrameworkCore.Repositories
{
    public class EclCustomRepository : TestDemoRepositoryBase<InvestmentEcl, Guid>, IEclCustomRepository
    {
        private readonly IActiveTransactionProvider _transactionProvider;
        private readonly ILocalizationSource _localizationSource;
        private const string InvestmentPreOverrideEclProcedure = "InvSec_proc_PreOverrideEclCompution";
        private const string InvestmentPostOverrideEclProcedure = "InvSec_proc_PostOverrideEclCompution";
        private const string InvestmentCloseEclProcedure = "ECL_Investment_Archive";
        private const string InvestmentReopenEclProcedure = "ECL_Investment_Restore";
        private const string WholesaleCloseEclProcedure = "ECL_Wholesale_Archive";
        private const string WholesaleReopenEclProcedure = "ECL_Wholesale_Restore";
        private const string RetailCloseEclProcedure = "ECL_Retail_Archive";
        private const string RetailReopenEclProcedure = "ECL_Retail_Restore";
        private const string ObeCloseEclProcedure = "ECL_Obe_Archive";
        private const string ObeReopenEclProcedure = "ECL_Obe_Restore";

        public EclCustomRepository(IDbContextProvider<TestDemoDbContext> dbContextProvider, IActiveTransactionProvider transactionProvider,
            ILocalizationManager localizationManager)
            : base(dbContextProvider)
        {
            _transactionProvider = transactionProvider;
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
        }

        public async Task RunInvestmentPreOverrideEclStoredProcedure(Guid eclId)
        {
            await RunEclProcedure(InvestmentPreOverrideEclProcedure, eclId);
        }

        public async Task RunInvestmentPostOverrideEclStoredProcedure(Guid eclId)
        {
            await RunEclProcedure(InvestmentPostOverrideEclProcedure, eclId);
        }

        public async Task RunInvestmentCloseEclStoredProcedure(Guid eclId)
        {
            await RunEclProcedure(InvestmentCloseEclProcedure, eclId);
        }
        public async Task RunInvestmentReopenEclStoredProcedure(Guid eclId)
        {
            await RunEclProcedure(InvestmentReopenEclProcedure, eclId);
        }

        public async Task RunWholesaleCloseEclStoredProcedure(Guid eclId)
        {
            await RunEclProcedure_(WholesaleCloseEclProcedure, eclId);
        }
        public async Task RunWholesaleReopenEclStoredProcedure(Guid eclId)
        {
            await RunEclProcedure_(WholesaleReopenEclProcedure, eclId);
        }

        public async Task RunRetailCloseEclStoredProcedure(Guid eclId)
        {
            await RunEclProcedure_(RetailCloseEclProcedure, eclId);
        }
        public async Task RunRetailReopenEclStoredProcedure(Guid eclId)
        {
            await RunEclProcedure_(RetailReopenEclProcedure, eclId);
        }

        public async Task RunObeCloseEclStoredProcedure(Guid eclId)
        {
            await RunEclProcedure_(ObeCloseEclProcedure, eclId);
        }
        public async Task RunObeReopenEclStoredProcedure(Guid eclId)
        {
            await RunEclProcedure_(ObeReopenEclProcedure, eclId);
        }
        public async Task DeleteExistingInputRecords(string tableName, string columnName, string value)
        {
            var query = $"Delete from {tableName} where {columnName} = @Id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Id", value)
            };
            await ExecuteQuery(query, parameters);
        }
        public async Task DeleteExistingRecordsCustomInvestmentAssetBooks(string value)
        {
            var query = $"Delete from InvestmentAssetBooks where InvestmentEclUploadId in (select Id from InvestmentEclUploads where InvestmentEclId =  @Id)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("Id", value)
            };
            await ExecuteQuery(query, parameters);
        }

        private async Task RunEclProcedure(string procedureName, Guid eclId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                                                            new SqlParameter() {ParameterName = "@ECL_ID", SqlDbType = SqlDbType.UniqueIdentifier, Direction = ParameterDirection.Input, Value = eclId}
                                                          };

            await RunProcedure(procedureName, parameters);
        }

        private async Task RunEclProcedure_(string procedureName, Guid eclId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                                                            new SqlParameter() {ParameterName = "@eclId", SqlDbType = SqlDbType.NVarChar, Direction = ParameterDirection.Input, Value = eclId.ToString()}
                                                          };

            await RunProcedure(procedureName, parameters);
        }

        public async Task<List<PrintAuditLogDto>> GetAuditLogForPrint(GetAuditLogForPrintInput input)
        {
            string startDateFilter = input.StartDate != null ? " and CreationTime >= '" + input.StartDate.Value.Date.ToString("yyyy-MM-dd h:mm tt") + "' " : "";
            string endDateFilter = input.EndDate != null ? " and CreationTime <= '" + input.EndDate.Value.Date.ToString("yyyy-MM-dd h:mm tt") + "' " : "";
            string userIdFilter = input.UserNameFilter != null ? " and (UserName like '%" + input.UserNameFilter + "%' or checkerName like '%" + input.UserNameFilter + "%')" : "";

            var query = $"Select * from vw_AuditLogList " +
                        $"where 1=1 " +
                        $"{ startDateFilter } " +
                        $"{ endDateFilter } " +
                        $"{ userIdFilter } " +
                        $"order by CreationTime ";

            await EnsureConnectionOpenAsync();
            using (var command = CreateCommand(query, CommandType.Text))
            {
                using (var dataReader = await command.ExecuteReaderAsync())
                {
                    var result = new List<PrintAuditLogDto>();
                    while (dataReader.Read())
                    {
                        var log = new PrintAuditLogDto();

                        log.PropertyName = dataReader["PropertyName"] == null || dataReader["PropertyName"] == DBNull.Value ? "" : dataReader["PropertyName"].ToString(); 
                        log.PropertyTypeFullName = dataReader["PropertyTypeFullName"] == null || dataReader["PropertyTypeFullName"] == DBNull.Value ? "" : dataReader["PropertyTypeFullName"].ToString(); 
                        log.OriginalValue = dataReader["OriginalValue"] == null || dataReader["OriginalValue"] == DBNull.Value ? "" : CustomRepositoryHelper.GetEnumValue(log.PropertyTypeFullName, dataReader["OriginalValue"].ToString()); 
                        log.NewValue = dataReader["NewValue"] == null || dataReader["NewValue"] == DBNull.Value ? "" : CustomRepositoryHelper.GetEnumValue(log.PropertyTypeFullName, dataReader["NewValue"].ToString()); 
                        log.ChangeTime = dataReader["ChangeTime"] == null || dataReader["ChangeTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["ChangeTime"].ToString()); 
                        log.CreationTime = dataReader["CreationTime"] == null || dataReader["CreationTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["CreationTime"].ToString());
                        log.ChangeType = dataReader["ChangeType"] == null || dataReader["ChangeType"] == DBNull.Value ? (EntityChangeType?)null : (EntityChangeType)Convert.ToInt32(dataReader["ChangeType"].ToString());
                        log.EntityId = dataReader["EntityId"] == null || dataReader["EntityId"] == DBNull.Value ? "" : dataReader["EntityId"].ToString();
                        log.EntityTypeFullName = dataReader["EntityTypeFullName"] == null || dataReader["EntityTypeFullName"] == DBNull.Value ? "" : _localizationSource.GetString(dataReader["EntityTypeFullName"].ToString());
                        log.BrowserInfo = dataReader["BrowserInfo"] == null || dataReader["BrowserInfo"] == DBNull.Value ? "" : dataReader["BrowserInfo"].ToString();
                        log.ClientIpAddress = dataReader["ClientIpAddress"] == null || dataReader["ClientIpAddress"] == DBNull.Value ? "" : dataReader["ClientIpAddress"].ToString();
                        log.UserName = dataReader["UserName"] == null || dataReader["UserName"] == DBNull.Value ? "" : dataReader["UserName"].ToString();
                        log.ImpersonatorUser = dataReader["ImpersonatorUser"] == null || dataReader["ImpersonatorUser"] == DBNull.Value ? "" : dataReader["ImpersonatorUser"].ToString();
                        log.UserId = dataReader["UserId"] == null || dataReader["UserId"] == DBNull.Value ? (long?)null : Convert.ToInt64(dataReader["UserId"].ToString());
                        log.ImpersonatorUserId = dataReader["ImpersonatorUserId"] == null || dataReader["ImpersonatorUserId"] == DBNull.Value ? (long?)null : Convert.ToInt64(dataReader["ImpersonatorUserId"].ToString());
                        log.CheckerId = dataReader["CheckerId"] == null || dataReader["CheckerId"] == DBNull.Value ? (long?)null : Convert.ToInt64(dataReader["CheckerId"].ToString());
                        log.CheckerName = dataReader["CheckerName"] == null || dataReader["CheckerName"] == DBNull.Value ? null : dataReader["CheckerName"].ToString();
                        log.CheckerDate = dataReader["CheckerDate"] == null || dataReader["CheckerDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dataReader["CheckerDate"].ToString());
                        log.CheckerIp = dataReader["CheckerIp"] == null || dataReader["CheckerIp"] == DBNull.Value ? null : dataReader["CheckerIp"].ToString();
                        
                        result.Add(log);
                    }
                    return result;
                }
            }
        }

        private async Task RunProcedure(string procedureName, SqlParameter[] parameters)
        {
            try
            {
                await EnsureConnectionOpenAsync();

                using (var command = CreateCommand(procedureName, CommandType.StoredProcedure, parameters))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch(Exception e)
            {

            }
        }

        private async Task ExecuteQuery(string query, SqlParameter[] parameters)
        {
            try
            {
                await Context.Database.ExecuteSqlCommandAsync(query, parameters);
                await EnsureConnectionOpenAsync();

                using (var command = CreateCommand(query, CommandType.Text, parameters))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch(Exception e)
            {

            }
        }

        private DbCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = Context.Database.GetDbConnection().CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = GetActiveTransaction();
            command.CommandTimeout = 0;

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            return command;
        }

        private async Task EnsureConnectionOpenAsync()
        {
            var connection = Context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
        }

        private DbTransaction GetActiveTransaction()
        {
            return (DbTransaction)_transactionProvider.GetActiveTransaction(new ActiveTransactionProviderArgs{
                                                                                                                {"ContextType", typeof(TestDemoDbContext) },
                                                                                                                {"MultiTenancySide", MultiTenancySide }
                                                                                                            });
        }
    }
}
