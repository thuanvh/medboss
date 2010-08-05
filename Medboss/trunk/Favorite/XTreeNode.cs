namespace Nammedia.Medboss.Favorite
{
    using System;
    using System.Xml;

    public class XTreeNode
    {
        private const string _idNode = "id";
        private string _name;
        private const string _nameNode = "name";
        private string _title;
        private const string _titleNode = "title";
        protected string elementNodeName;
        public const string Folder = "folder";
        public string Id;
        public const string Leaf = "leaf";

        protected virtual void writeDetails(ref XmlWriter writer)
        {
        }

        public void writeXML(ref XmlWriter writer)
        {
            writer.WriteStartElement(this.elementNodeName);
            writer.WriteAttributeString("name", this._name);
            writer.WriteAttributeString("title", this._title);
            writer.WriteAttributeString("id", this.Id);
            this.writeDetails(ref writer);
            writer.WriteEndElement();
        }

        public string ElementNodeName
        {
            get
            {
                return this.elementNodeName;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }
    }
}
