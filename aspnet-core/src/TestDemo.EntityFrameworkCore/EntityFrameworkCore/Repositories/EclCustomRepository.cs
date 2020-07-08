using Abp.Application.Services.Dto;
using Abp.Data;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestDemo.Investment;
using TestDemo.InvestmentComputation;

namespace TestDemo.EntityFrameworkCore.Repositories
{
    public class EclCustomRepository : TestDemoRepositoryBase<InvestmentEcl, Guid>, IEclCustomRepository
    {
        private readonly IActiveTransactionProvider _transactionProvider;
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

        public EclCustomRepository(IDbContextProvider<TestDemoDbContext> dbContextProvider, IActiveTransactionProvider transactionProvider)
            : base(dbContextProvider)
        {
            _transactionProvider = transactionProvider;
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
