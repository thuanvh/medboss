namespace Nammedia.Medboss.report
{
    using System;

    internal class TongHopParaManager : ReportParaManager
    {
        public override void setParams()
        {
            DateTime dtTuNgay = (DateTime) this.FromDateParam.Value;
            string strNgayTonDauKy = dtTuNgay.AddDays(-1.0).ToString("MM/dd/yyyy");
            string strTuNgay = ((DateTime) this.FromDateParam.Value).ToString("MM/dd/yyyy");
            string strDenNgay = ((DateTime) this.ToDateParam.Value).ToString("MM/dd/yyyy");
            ReportParam param = new ReportParam("[prNgay]", strNgayTonDauKy);
            ReportParam param2 = new ReportParam("[qryHoaDon_SauKiemKe_NgayCuoi]", strNgayTonDauKy);
            ReportParam param3 = new ReportParam("[qryHoaDonChiTiet_TuNgay]", strTuNgay);
            ReportParam param4 = new ReportParam("[qryHoaDonChiTiet_ToiNgay]", strDenNgay);
            ReportParam param5 = new ReportParam("[qryNgayNhapGanNhat_NgayEnd]", strNgayTonDauKy);
            ReportParam param6 = new ReportParam("[qryNgayNhapGanNhat2_NgayEnd]", strDenNgay);
            ReportParam[] paras = new ReportParam[] { base.QuayParam, param, param3, param4, param6, param5, param2 };
            ReportViewHelper.SetParamValues(base.ReportViewer, paras);
        }
    }
}
