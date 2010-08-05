namespace Nammedia.Medboss.Favorite
{
    using System;
    using System.Xml;

    public class XTreeLeaf : XTreeNode
    {
        private OperatorNode _oper;

        public XTreeLeaf()
        {
            base.elementNodeName = "leaf";
        }

        protected override void writeDetails(ref XmlWriter writer)
        {
            writer.WriteStartElement("operator");
            writer.WriteElementString("type", this.oper.type);
            writer.WriteStartElement("params");
            foreach (ParamNode node in this.oper.paramSequence)
            {
                writer.WriteStartElement("param");
                writer.WriteElementString("name", node.name);
                writer.WriteElementString("value", node.value);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        public OperatorNode oper
        {
            get
            {
                return this._oper;
            }
            set
            {
                this._oper = value;
            }
        }
    }
}
