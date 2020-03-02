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

        private async Task RunEclProcedure(string procedureName, Guid eclId)
        {
            SqlParameter[] parameters = new SqlParameter[]{
                                                            new SqlParameter() {ParameterName = "@ECL_ID", SqlDbType = SqlDbType.UniqueIdentifier, Direction = ParameterDirection.Input, Value = eclId}
                                                          };

            await RunProcedure(procedureName, parameters);
        }

        private async Task RunProcedure(string procedureName, SqlParameter[] parameters)
        {
            await EnsureConnectionOpenAsync();

            using (var command = CreateCommand(procedureName, CommandType.StoredProcedure, parameters))
            {
                await command.ExecuteNonQueryAsync();
            }
        }

        private DbCommand CreateCommand(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = Context.Database.GetDbConnection().CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Transaction = GetActiveTransaction();

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
