namespace Nammedia.Medboss.Favorite
{
    using System;
    using System.Collections;
    using System.Windows.Forms;
    using System.Xml;

    public class FavoriteTree
    {
        public Hashtable NodeMap;
        public Hashtable OperatorMap;
        public XTreeFolder Tree;

        public FavoriteTree()
        {
            this.OperatorMap = new Hashtable();
            this.NodeMap = new Hashtable();
            this.Tree = new XTreeFolder();
            this.Tree.Name = "Favorite";
            this.Tree.Title = "Favorite";
            this.Tree.Id = "Favorite";
        }

        public FavoriteTree(string fileXML)
        {
            this.OperatorMap = new Hashtable();
            this.NodeMap = new Hashtable();
            this.Tree = this.getTree(fileXML);
        }

        private OperatorNode getOperator(XmlNode xmlnodeOperator)
        {
            OperatorNode node = new OperatorNode();
            if (xmlnodeOperator.Name == "operator")
            {
                node.paramSequence = new ArrayList();
                XmlNodeList childNodes = xmlnodeOperator.ChildNodes;
                foreach (XmlNode node2 in childNodes)
                {
                    if (node2.Name == "type")
                    {
                        node.type = node2.InnerText;
                    }
                    if (node2.Name == "params")
                    {
                        foreach (XmlNode node3 in node2.ChildNodes)
                        {
                            ParamNode node4 = new ParamNode();
                            foreach (XmlNode node5 in node3)
                            {
                                if (node5.Name == "name")
                                {
                                    node4.name = node5.InnerText;
                                }
                                else if (node5.Name == "value")
                                {
                                    node4.value = node5.InnerText;
                                }
                            }
                            node.paramSequence.Add(node4);
                        }
                    }
                }
            }
            return node;
        }

        private XTreeFolder getTree(string fileXML)
        {
            XTreeFolder folder = new XTreeFolder();
            XmlDocument document = new XmlDocument();
            document.Load(fileXML);
            XmlElement folderNode = document.DocumentElement;
            this.getXTreeFolder(folderNode, ref folder);
            return folder;
        }

        public TreeNode getTreeNode()
        {
            return this.getTreeNode(this.Tree);
        }

        private TreeNode getTreeNode(XTreeFolder folder)
        {
            TreeNode node2;
            TreeNode node = new TreeNode();
            node.Text = folder.Title;
            node.Name = folder.Name;
            foreach (XTreeLeaf leaf in folder.ChildLeaves)
            {
                node2 = new TreeNode();
                node2.Name = leaf.Name;
                node2.Text = leaf.Title;
                node.Nodes.Add(node2);
            }
            foreach (XTreeFolder folder2 in folder.ChildFolders)
            {
                node2 = this.getTreeNode(folder2);
                node.Nodes.Add(node2);
            }
            return node;
        }

        public TreeNode getTreeNode(int folderImageIndex, int leafImageIndex)
        {
            return this.getTreeNode(this.Tree, folderImageIndex, leafImageIndex);
        }

        private TreeNode getTreeNode(XTreeFolder folder, int folderImageIndex, int leafImageIndex)
        {
            TreeNode node2;
            TreeNode node = new TreeNode();
            node.ImageIndex = folderImageIndex;
            node.SelectedImageIndex = folderImageIndex;
            node.Text = folder.Title;
            node.Name = folder.Name;
            foreach (XTreeLeaf leaf in folder.ChildLeaves)
            {
                node2 = new TreeNode();
                node2.Name = leaf.Name;
                node2.Text = leaf.Title;
                node2.ImageIndex = leafImageIndex;
                node2.SelectedImageIndex = leafImageIndex;
                node.Nodes.Add(node2);
            }
            foreach (XTreeFolder folder2 in folder.ChildFolders)
            {
                node2 = this.getTreeNode(folder2, folderImageIndex, leafImageIndex);
                node.Nodes.Add(node2);
            }
            return node;
        }

        private void getXTreeFolder(XmlNode folderNode, ref XTreeFolder folder)
        {
            XmlNodeList childNodes = folderNode.ChildNodes;
            folder.Id = folderNode.Attributes["id"].Value;
            folder.Name = folderNode.Attributes["name"].Value;
            folder.Title = folderNode.Attributes["title"].Value;
            this.NodeMap.Add(folder.Name, folder);
            foreach (XmlNode node in childNodes)
            {
                if (node.Name == "leaf")
                {
                    XTreeLeaf leaf = new XTreeLeaf();
                    leaf.Id = node.Attributes["id"].Value;
                    leaf.Name = node.Attributes["name"].Value;
                    leaf.Title = node.Attributes["title"].Value;
                    leaf.oper = this.getOperator(node.FirstChild);
                    this.OperatorMap.Add(leaf.Name, leaf.oper);
                    this.NodeMap.Add(leaf.Name, leaf);
                    folder.ChildLeaves.Add(leaf);
                }
                if (node.Name == "folder")
                {
                    XTreeFolder folder2 = new XTreeFolder();
                    this.getXTreeFolder(node, ref folder2);
                    folder.ChildFolders.Add(folder2);
                }
            }
        }

        public void WriteXML(string fileXML)
        {
            XmlTextWriter writer = new XmlTextWriter(fileXML, null);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            XmlWriter writer2 = writer;
            this.Tree.writeXML(ref writer2);
            writer.Flush();
            writer.Close();
        }
    }
}
