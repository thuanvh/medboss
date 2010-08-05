namespace Nammedia.Medboss.report
{
    using System;

    internal class ThanhToanCXParaManager : ReportParaManager
    {
        public override void setParams()
        {
            ReportParam[] paras = new ReportParam[] { base.QuayParam, base.NhomCSParam };
            ReportViewHelper.SetParamValues(base.ReportViewer, paras);
        }
    }
}
