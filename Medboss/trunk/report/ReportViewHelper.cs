namespace Nammedia.Medboss.report
{
    using CrystalDecisions.Shared;
    using CrystalDecisions.Windows.Forms;
    using System;

    internal class ReportViewHelper
    {
        public static void SetParamValues(CrystalReportViewer rpviewer, ReportParam[] paras)
        {
            ParameterFields fields = new ParameterFields();
            foreach (ReportParam param in paras)
            {
                ParameterField parameterField = new ParameterField();
                ParameterDiscreteValue value2 = new ParameterDiscreteValue();
                ParameterRangeValue value3 = new ParameterRangeValue();
                parameterField.ParameterFieldName = param.Name;
                value2.Value = param.Value;
                parameterField.CurrentValues.Add((ParameterValue) value2);
                fields.Add(parameterField);
            }
            rpviewer.ParameterFieldInfo = fields;
        }
    }
}
