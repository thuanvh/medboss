namespace Nammedia.Medboss.report
{
    using System;

    internal class BanHangChuanParaManager : ReportParaManager
    {
        public override void setParams()
        {
            DateTime dtTuNgay = (DateTime) this.FromDateParam.Value;
            string strTuNgay = ((DateTime) this.FromDateParam.Value).ToString("MM/dd/yyyy");
            string strDenNgay = ((DateTime) this.ToDateParam.Value).ToString("MM/dd/yyyy");
            ReportParam param = new ReportParam("[qryNgayNhapGanNhat_NgayEnd]", strDenNgay);
            ReportParam TuNgay = new ReportParam("[FromDate]", strTuNgay);
            ReportParam DenNgay = new ReportParam("[ToDate]", strDenNgay);
            ReportParam[] paras = new ReportParam[] { base.QuayParam, TuNgay, DenNgay, param };
            ReportViewHelper.SetParamValues(base.ReportViewer, paras);
        }
    }
}
