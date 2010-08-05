namespace Nammedia.Medboss.Favorite
{
    using System;
    using System.Collections;
    using System.Xml;

    public class XTreeFolder : XTreeNode
    {
        private ArrayList _childFolders;
        private ArrayList _childLeaves;

        public XTreeFolder()
        {
            base.elementNodeName = "folder";
            this._childLeaves = new ArrayList();
            this._childFolders = new ArrayList();
        }

        public int addFolder(XTreeFolder folder)
        {
            return this._childFolders.Add(folder);
        }

        public int addLeaf(XTreeLeaf leaf)
        {
            return this._childLeaves.Add(leaf);
        }

        public bool addXTreeNode(string parentName, XTreeLeaf node)
        {
            XTreeFolder folder = this.findFolder(parentName);
            if (folder != null)
            {
                folder.ChildLeaves.Add(node);
                return true;
            }
            return false;
        }

        public XTreeFolder findFolder(string name)
        {
            if (base.Name == name)
            {
                return this;
            }
            foreach (XTreeFolder folder in this._childFolders)
            {
                XTreeFolder folder2 = folder.findFolder(name);
                if (folder2 != null)
                {
                    return folder2;
                }
            }
            return null;
        }

        protected override void writeDetails(ref XmlWriter writer)
        {
            foreach (XTreeFolder folder in this._childFolders)
            {
                folder.writeXML(ref writer);
            }
            foreach (XTreeLeaf leaf in this._childLeaves)
            {
                leaf.writeXML(ref writer);
            }
        }

        public ArrayList ChildFolders
        {
            get
            {
                return this._childFolders;
            }
            set
            {
                this._childFolders = value;
            }
        }

        public ArrayList ChildLeaves
        {
            get
            {
                return this._childLeaves;
            }
            set
            {
                this._childLeaves = value;
            }
        }
    }
}
