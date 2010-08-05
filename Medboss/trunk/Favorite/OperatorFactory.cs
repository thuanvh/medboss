using Nammedia.Medboss.controls;
using Nammedia.Medboss.lib;
using Nammedia.Medboss.report;
using Nammedia.Medboss.report.reports.finance;
using Nammedia.Medboss.report.reports.general;
using Nammedia.Medboss.report.reports.import;
using Nammedia.Medboss.report.reports.repay;
using Nammedia.Medboss.report.reports.sale;
using Nammedia.Medboss.report.reports.verifier;
using System;

namespace Nammedia.Medboss.Favorite
{

    internal class OperatorFactory
    {
        public const string AdvanceSearcherType = "AdvanceSearcher";
        public const string BaoCaoBanThuoc = "BaoCaoBanThuoc";
        public const string BaoCaoBanThuocChiTiet = "BaoCaoBanThuocChiTiet";
        public const string BaoCaoBanThuocKhachHang = "BaoCaoBanThuocKhachHang";
        public const string BaoCaoHangTon = "BaoCaoHangTon";
        public const string BaoCaoKiemKe = "BaoCaoKiemKe";
        public const string BaoCaoNhanThuocTraLai = "BaoCaoNhanThuocTraLai";
        public const string BaoCaoNhanThuocTraLaiChiTiet = "BaoCaoNhanThuocTraLaiChiTiet";
        public const string BaoCaoNhapThuoc = "BaoCaoNhapThuoc";
        public const string BaoCaoNhapThuocChiTiet = "BaoCaoNhapThuocChiTiet";
        public const string BaoCaoNhapThuocKhachHang = "BaoCaoNhapThuocKhachHang";
        public const string BaoCaoQuy = "BaoCaoQuy";
        public const string BaoCaoQuyTongHop = "BaoCaoQuyTongHop";
        public const string BaoCaoTongHopNhapBan = "BaoCaoTongHopNhapBan";
        public const string BaoCaoTongHopNhapBanChuan = "BaoCaoTongHopNhapBanChuan";
        public const string BaoCaoTraLaiThuoc = "BaoCaoTraLaiThuoc";
        public const string BaoCaoTraLaiThuocChiTiet = "BaoCaoTraLaiThuocChiTiet";

        public static OperatorBase LoadOperator(OperatorNode operNode)
        {
            object obj2 = new object();
            switch (operNode.type)
            {
                case "AdvanceSearcher":
                {
                    AdvanceSearcher searcher = new AdvanceSearcher();
                    obj2 = searcher;
                    break;
                }
                case "BaoCaoQuyTongHop":
                {
                    ReportViewOperator @operator = new ReportViewOperator(operNode.type);
                    @operator.Report = new rptQuyTongHopModif();
                    obj2 = @operator;
                    break;
                }
                case "BaoCaoNhapThuoc":
                {
                    ReportViewOperator operator2 = new ReportViewOperator("BaoCaoNhapThuoc");
                    operator2.Report = new rptHangNhap();
                    obj2 = operator2;
                    break;
                }
                case "BaoCaoBanThuoc":
                {
                    ReportViewOperator operator3 = new ReportViewOperator("BaoCaoBanThuoc");
                    operator3.Report = new rptHangBan();
                    obj2 = operator3;
                    break;
                }
                case "BaoCaoQuy":
                {
                    ReportViewOperator operator4 = new ReportViewOperator("BaoCaoQuy");
                    operator4.Report = new rptQuy();
                    obj2 = operator4;
                    break;
                }
                case "BaoCaoTongHopNhapBan":
                {
                    ReportViewOperator operator5 = new ReportViewOperator("BaoCaoTongHopNhapBan");
                    operator5.Report = new rptTongHopNhapBan();
                    operator5.ParamManager = new TongHopParaManager();
                    obj2 = operator5;
                    break;
                }
                case "BaoCaoTongHopNhapBanChuan":
                {
                    ReportViewOperator operator6 = new ReportViewOperator("BaoCaoTongHopNhapBanChuan");
                    operator6.Report = new rptTongHopNhapBanStandard();
                    operator6.ParamManager = new TongHopParaManager();
                    obj2 = operator6;
                    break;
                }
                case "BaoCaoHangTon":
                {
                    ReportViewOperator operator7 = new ReportViewOperator("BaoCaoHangTon");
                    operator7.Report = new rptHangTon();
                    operator7.ParamManager = new TongHopParaManager();
                    obj2 = operator7;
                    break;
                }
                case "BaoCaoBanThuocChiTiet":
                {
                    ReportViewOperator operator8 = new ReportViewOperator("BaoCaoBanThuocChiTiet");
                    operator8.Report = new rptBanThuocChiTiet();
                    operator8.ParamManager = new KiemKeParaManager();
                    obj2 = operator8;
                    break;
                }
                case "BaoCaoNhapThuocChiTiet":
                {
                    ReportViewOperator operator9 = new ReportViewOperator("BaoCaoNhapThuocChiTiet");
                    operator9.Report = new rptNhapThuocChiTiet();
                    operator9.ParamManager = new KiemKeParaManager();
                    obj2 = operator9;
                    break;
                }
                case "BaoCaoKiemKe":
                {
                    ReportViewOperator operator10 = new ReportViewOperator("BaoCaoKiemKe");
                    operator10.Report = new rptKiemKe();
                    operator10.ParamManager = new KiemKeParaManager();
                    obj2 = operator10;
                    break;
                }
                case "BaoCaoTraLaiThuocChiTiet":
                {
                    ReportViewOperator operator11 = new ReportViewOperator("BaoCaoTraLaiThuocChiTiet");
                    operator11.Report = new rptTraLaiChiTiet();
                    operator11.ParamManager = new TraLaiParaManager(KhoanThuChiInfo.Thu);
                    obj2 = operator11;
                    break;
                }
                case "BaoCaoTraLaiThuoc":
                {
                    ReportViewOperator operator12 = new ReportViewOperator("BaoCaoTraLaiThuoc");
                    operator12.Report = new rptTraLai();
                    operator12.ParamManager = new TraLaiParaManager(KhoanThuChiInfo.Thu);
                    obj2 = operator12;
                    break;
                }
                case "BaoCaoNhanThuocTraLai":
                {
                    ReportViewOperator operator13 = new ReportViewOperator("BaoCaoNhanThuocTraLai");
                    operator13.Report = new rptTraLai();
                    operator13.ParamManager = new TraLaiParaManager(KhoanThuChiInfo.Chi);
                    obj2 = operator13;
                    break;
                }
                case "BaoCaoNhanThuocTraLaiChiTiet":
                {
                    ReportViewOperator operator14 = new ReportViewOperator("BaoCaoNhanThuocTraLaiChiTiet");
                    operator14.Report = new rptTraLaiChiTiet();
                    operator14.ParamManager = new TraLaiParaManager(KhoanThuChiInfo.Chi);
                    obj2 = operator14;
                    break;
                }
            }
            IFavorizable favorizable = (IFavorizable) obj2;
            ParamNode[] array = new ParamNode[operNode.paramSequence.Count];
            operNode.paramSequence.CopyTo(array);
            favorizable.setParams(array);
            return (OperatorBase) obj2;
        }
    }
}
