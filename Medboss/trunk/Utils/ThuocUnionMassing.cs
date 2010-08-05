namespace Nammedia.Medboss.Utils
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.lib;
    using Nammedia.Medboss.Log;
    using System;
    using System.Data;

    internal class ThuocUnionMassing
    {
        public static void Union()
        {
            try
            {
                DBDriver db = new DBDriver();
                db.Command.CommandText = "select MaCu,MaMoi from qryThuocTenMaCuMaMoi";
                IDataReader reader = db.Command.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        int macu = ConvertHelper.getInt(reader["MaCu"]);
                        int mamoi = ConvertHelper.getInt(reader["MaMoi"]);
                        if (macu != mamoi)
                        {
                            new MedicineController().medicineUnion(macu, mamoi);
                        }
                    }
                    reader.Close();
                }
                db.Command.Connection.Close();
            }
            catch (Exception exc)
            {
                LogManager.LogException(exc);
            }
        }
    }
}
