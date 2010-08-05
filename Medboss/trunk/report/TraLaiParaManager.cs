namespace Nammedia.Medboss.report
{
    using System;

    internal class TraLaiParaManager : ReportParaManager
    {
        private bool _ThuHayChi;

        public TraLaiParaManager(bool thuHayChi)
        {
            this._ThuHayChi = thuHayChi;
        }

        public override void setParams()
        {
            ReportParam param = new ReportParam("TuNgay", this.FromDateParam.Value);
            ReportParam param2 = new ReportParam("DenNgay", this.ToDateParam.Value);
            ReportParam param3 = new ReportParam("ThuHayChi", this._ThuHayChi);
            ReportParam[] paras = new ReportParam[] { param, param2, base.QuayParam, param3 };
            ReportViewHelper.SetParamValues(base.ReportViewer, paras);
        }
    }
}
