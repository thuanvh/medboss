namespace Nammedia.Medboss.report
{
    using System;

    internal class KiemKeParaManager : ReportParaManager
    {
        public override void setParams()
        {
            ReportParam param = new ReportParam("TuNgay", this.FromDateParam.Value);
            ReportParam param2 = new ReportParam("DenNgay", this.ToDateParam.Value);
            ReportParam[] paras = new ReportParam[] { param, param2, base.QuayParam };
            ReportViewHelper.SetParamValues(base.ReportViewer, paras);
        }
    }
}
