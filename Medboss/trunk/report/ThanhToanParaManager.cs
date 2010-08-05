namespace Nammedia.Medboss.report
{
    using System;

    internal class ThanhToanParaManager : ReportParaManager
    {
        public override void setParams()
        {
            DateTime tungay = (DateTime) this.FromDateParam.Value;
            ReportParam param = new ReportParam("TuNgay", tungay.ToString("MM/dd/yyyy"));
            DateTime denngay = (DateTime) this.ToDateParam.Value;
            ReportParam param2 = new ReportParam("DenNgay", denngay.ToString("MM/dd/yyyy"));
            ReportParam[] paras = new ReportParam[] { param, param2, base.QuayParam, base.NhomCSParam };
            ReportViewHelper.SetParamValues(base.ReportViewer, paras);
        }
    }
}
