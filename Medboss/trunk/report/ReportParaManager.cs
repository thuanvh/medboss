namespace Nammedia.Medboss.report
{
    using CrystalDecisions.Windows.Forms;
    using System;

    public class ReportParaManager
    {
        public ReportParam FromDateParam;
        public ReportParam NhomCSParam;
        public ReportParam QuayParam;
        public CrystalReportViewer ReportViewer;
        public ReportParam ToDateParam;

        public virtual void setParams()
        {
            string text = ((DateTime) this.FromDateParam.Value).ToString("MM/dd/yyyy");
            string text2 = ((DateTime) this.ToDateParam.Value).ToString("MM/dd/yyyy");
            this.FromDateParam.Value = text;
            this.ToDateParam.Value = text2;
            ReportParam[] paras = new ReportParam[] { this.FromDateParam, this.ToDateParam, this.QuayParam };
            ReportViewHelper.SetParamValues(this.ReportViewer, paras);
        }
    }
}
