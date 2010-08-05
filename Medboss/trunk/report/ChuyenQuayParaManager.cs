namespace Nammedia.Medboss.report
{
    using CrystalDecisions.Windows.Forms;
    using System;

    public class ChuyenQuayParaManager
    {
        public ReportParam DenQuayParam;
        public ReportParam FromDateParam;
        public ReportParam MotChieu;
        public CrystalReportViewer ReportViewer;
        public ReportParam ToDateParam;
        public ReportParam TuQuayParam;

        public virtual void setParams()
        {
            ReportParam[] paras = new ReportParam[] { this.FromDateParam, this.ToDateParam, this.TuQuayParam, this.DenQuayParam, this.MotChieu };
            ReportViewHelper.SetParamValues(this.ReportViewer, paras);
        }
    }
}
