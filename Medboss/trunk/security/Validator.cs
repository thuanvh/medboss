namespace Nammedia.Medboss.security
{
    using Nammedia.Medboss;
    using Nammedia.Medboss.lib;
    using Nammedia.Medboss.Utils;
    using System;
    using System.Web.Security;

    internal class Validator : DataController
    {
        private string salt = "Medboss is the best program";

        public void delete(string userName)
        {
            userName = userName.ToLower();
            string text = "delete * from User where username='" + userName + "'";
            base.db.Command.CommandText = text;
            if (base.db.Command.ExecuteNonQuery() < 0)
            {
                throw new DeleteException();
            }
            base.db.Command.Connection.Close();
        }

        public string getHash(string input)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(this.salt + input, "MD5");
        }

        public string getPass(string userName)
        {
            userName = userName.ToLower();
            string text = "select password from User where username='" + userName + "'";
            base.db.Command.CommandText = text;
            string text2 = ConvertHelper.getString(base.db.Command.ExecuteScalar());
            base.db.Command.Connection.Close();
            return text2;
        }

        public void insert(string userName, string pass)
        {
            userName = userName.ToLower();
            int missingId = IdManager.GetMissingId("UserId", "User");
            string text = string.Concat(new object[] { "insert into User values(", missingId, ",'", userName, "','", this.getHash(pass), "')" });
            base.db.Command.CommandText = text;
            if (base.db.Command.ExecuteNonQuery() < 0)
            {
                throw new InsertException();
            }
            base.db.Command.Connection.Close();
        }

        public bool IsValidated(string userName, string pass)
        {
            string text = this.getHash(pass);
            string text2 = this.getPass(userName);
            return (text == text2);
        }

        public void update(string userName, string pass)
        {
            userName = userName.ToLower();
            string text = "update User set pass='" + this.getHash(pass) + "' where userName='" + userName + "'";
            base.db.Command.CommandText = text;
            if (base.db.Command.ExecuteNonQuery() < 0)
            {
                throw new UpdateException();
            }
            base.db.Command.Connection.Close();
        }
    }
}
