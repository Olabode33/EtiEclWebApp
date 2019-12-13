using EclEngine.BaseEclEngine.PdInput.Calculations;
using EclEngine.Utils;
using System;
using System.Data;

namespace EclEngine.BaseEclEngine.PdInput
{
    public class PdInputResult
    {
        CreditIndex _creditIndex;
        PdMapping _pdMapping;
        SicrInputWorkings _sicrInput;
        ScenarioLifetimePd _lifeTimePdBest;
        ScenarioLifetimePd _lifeTimePdOptimistic;
        ScenarioLifetimePd _lifeTimePdDownturn;
        ScenarioRedefaultLifetimePds _redefaultLifetimePdBest;
        ScenarioRedefaultLifetimePds _redefaultLifetimePdOptimistic;
        ScenarioRedefaultLifetimePds _redefaultLifetimePdDownturn;

        public PdInputResult()
        {
            _creditIndex = new CreditIndex();
            _pdMapping = new PdMapping();
            _sicrInput = new SicrInputWorkings();
            _lifeTimePdBest = new ScenarioLifetimePd(TempEclData.ScenarioBest);
            _lifeTimePdOptimistic = new ScenarioLifetimePd(TempEclData.ScenarioOptimistic);
            _lifeTimePdDownturn = new ScenarioLifetimePd(TempEclData.ScenarioDownturn);
            _redefaultLifetimePdBest = new ScenarioRedefaultLifetimePds(TempEclData.ScenarioBest);
            _redefaultLifetimePdOptimistic = new ScenarioRedefaultLifetimePds(TempEclData.ScenarioOptimistic); ;
            _redefaultLifetimePdDownturn = new ScenarioRedefaultLifetimePds(TempEclData.ScenarioDownturn); ;
        }

        public string GetCreditIndex()
        {
            return JsonUtil.SerializeObject(_creditIndex.ComputeCreditIndex());
        }
        public string GetPdMapping()
        {
            return JsonUtil.SerializeObject(_pdMapping.ComputePdMappingTable());
        }
        public string GetSicrInputs()
        {
            return JsonUtil.SerializeObject(_sicrInput.ComputeSicrInput());
        }
        public void RunSicrInputs()
        {
            DataTable resultDt = _sicrInput.ComputeSicrInput();
            string result = DbUtil.InsertDataTable(DbUtil._tempResulPdMappings, resultDt);
        }
        public string GetLifetimePdBest()
        {
            return JsonUtil.SerializeObject(_lifeTimePdBest.ComputeLifetimePd());
        }
        public void RunLifetimePdBest()
        {
            DataTable resultDt = _lifeTimePdBest.ComputeLifetimePd();
            string result = DbUtil.InsertDataTable(DbUtil._tempResulPdLifetimeBest, resultDt);
        }

        public string GetLifetimePdOptimistic()
        {
            return JsonUtil.SerializeObject(_lifeTimePdOptimistic.ComputeLifetimePd());
        }
        public void RunLifetimePdOptimistic()
        {
            DataTable resultDt = _lifeTimePdOptimistic.ComputeLifetimePd();
            string result = DbUtil.InsertDataTable(DbUtil._tempResulPdLifetimeOptimistics, resultDt);
        }

        public string GetLifetimePdDownturn()
        {
            return JsonUtil.SerializeObject(_lifeTimePdDownturn.ComputeLifetimePd());
        }
        public void RunLifetimePdDownturn()
        {
            DataTable resultDt = _lifeTimePdDownturn.ComputeLifetimePd();
            string result = DbUtil.InsertDataTable(DbUtil._tempResulPdLifetimeDownturn, resultDt);
        }

        public string GetRedefaultLifetimePdBest()
        {
            return JsonUtil.SerializeObject(_redefaultLifetimePdBest.ComputeRedefaultLifetimePd());
        }
        public void RunRedefaultLifetimePdBest()
        {
            DataTable resultDt = _redefaultLifetimePdBest.ComputeRedefaultLifetimePd();
            string result = DbUtil.InsertDataTable(DbUtil._tempResultPdRedefaultLifetimeBest, resultDt);
        }

        public string GetRedefaultLifetimePdOptimistic()
        {
            return JsonUtil.SerializeObject(_redefaultLifetimePdOptimistic.ComputeRedefaultLifetimePd());
        }
        public void RunRedefaultLifetimePdOptimistic()
        {
            DataTable resultDt = _redefaultLifetimePdOptimistic.ComputeRedefaultLifetimePd();
            string result = DbUtil.InsertDataTable(DbUtil._tempResultPdRedefaultLifetimeOptimistics, resultDt);
        }

        public string GetRedefaultLifetimePdDownturn()
        {
            return JsonUtil.SerializeObject(_redefaultLifetimePdDownturn.ComputeRedefaultLifetimePd());
        }
        public void RunRedefaultLifetimePdDownturn()
        {
            DataTable resultDt = _redefaultLifetimePdDownturn.ComputeRedefaultLifetimePd();
            string result = DbUtil.InsertDataTable(DbUtil._tempResultPdRedefaultLifetimeDownturn, resultDt);
        }
    }
}
