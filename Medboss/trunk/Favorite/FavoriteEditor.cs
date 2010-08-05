namespace Nammedia.Medboss.Favorite
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FavoriteEditor : Form
    {
        private int _folderImageIndex;
        private ImageList _imgList;
        private XTreeNode _insertedNode;
        private int _leafImageIndex;
        private FavoriteTree _tree;
        private Button butClose;
        private Button butNewFolder;
        private Button butSave;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private TreeView treeFav;
        private TextBox txtTitle;

        public FavoriteEditor(ref FavoriteTree tree, XTreeNode node)
        {
            this.InitializeComponent();
            this.txtTitle.Text = node.Title;
            this._tree = tree;
            this._insertedNode = node;
            this.treeFav.Nodes.Add(this._tree.getTreeNode(this._folderImageIndex, this._leafImageIndex));
        }

        private void butClose_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void butNewFolder_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeFav.SelectedNode;
            if (selectedNode != null)
            {
                NewFolderForm form = new NewFolderForm();
                string text = "";
                DialogResult result = form.ShowDialog();
                text = form.getFolderName();
                if (result == DialogResult.OK)
                {
                    XTreeNode node2 = (XTreeNode) this._tree.NodeMap[selectedNode.Name];
                    if (node2.ElementNodeName == "folder")
                    {
                        XTreeFolder folder = new XTreeFolder();
                        folder.Name = text;
                        folder.Title = text;
                        folder.Id = text;
                        if (this._tree.NodeMap.Contains(folder.Id))
                        {
                            MessageBox.Show("Lỗi: Tồn tại thư mục c\x00f9ng t\x00ean " + folder.Id);
                        }
                        else
                        {
                            XTreeFolder folder2 = (XTreeFolder) node2;
                            int num = folder2.addFolder(folder);
                            this._tree.NodeMap.Add(folder.Id, folder2.ChildFolders[num]);
                            this.treeFav.Nodes.Clear();
                            this.treeFav.Nodes.Add(this._tree.getTreeNode(this._folderImageIndex, this._leafImageIndex));
                        }
                    }
                }
            }
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeFav.SelectedNode;
            if (selectedNode != null)
            {
                XTreeNode node2 = (XTreeNode) this._tree.NodeMap[selectedNode.Name];
                this._insertedNode.Title = this.txtTitle.Text;
                if (this._tree.NodeMap.Contains(this._insertedNode.Id))
                {
                    XTreeNode node3 = (XTreeNode) this._tree.NodeMap[this._insertedNode.Id];
                    MessageBox.Show("Lỗi: Tr\x00f9ng m\x00e3 với: " + node3.Name);
                }
                else if (node2.ElementNodeName == "folder")
                {
                    XTreeFolder folder = (XTreeFolder) node2;
                    int num = folder.addLeaf((XTreeLeaf) this._insertedNode);
                    this._tree.NodeMap.Add(this._insertedNode.Id, folder.ChildLeaves[num]);
                    this._tree.OperatorMap.Add(this._insertedNode.Id, ((XTreeLeaf) folder.ChildLeaves[num]).oper);
                    this.treeFav.Nodes.Clear();
                    this.treeFav.Nodes.Add(this._tree.getTreeNode(this._folderImageIndex, this._leafImageIndex));
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.treeFav = new TreeView();
            this.txtTitle = new TextBox();
            this.label1 = new Label();
            this.butSave = new Button();
            this.butClose = new Button();
            this.label2 = new Label();
            this.butNewFolder = new Button();
            base.SuspendLayout();
            this.treeFav.LabelEdit = true;
            this.treeFav.Location = new Point(0x36, 0x19);
            this.treeFav.Name = "treeFav";
            this.treeFav.Size = new Size(0x100, 0xd3);
            this.treeFav.TabIndex = 0;
            this.txtTitle.Location = new Point(0x36, 0xf9);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new Size(0x100, 20);
            this.txtTitle.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 0xfc);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x21, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nh\x00e3n";
            this.butSave.Location = new Point(320, 0xf7);
            this.butSave.Name = "butSave";
            this.butSave.Size = new Size(0x4b, 0x17);
            this.butSave.TabIndex = 3;
            this.butSave.Text = "&Lưu";
            this.butSave.UseVisualStyleBackColor = true;
            this.butSave.Click += new EventHandler(this.butSave_Click);
            this.butClose.Location = new Point(320, 0x19);
            this.butClose.Name = "butClose";
            this.butClose.Size = new Size(0x4b, 0x17);
            this.butClose.TabIndex = 4;
            this.butClose.Text = "Đ\x00f3&ng";
            this.butClose.UseVisualStyleBackColor = true;
            this.butClose.Click += new EventHandler(this.butClose_Click);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Lưu trong";
            this.butNewFolder.Location = new Point(320, 0x36);
            this.butNewFolder.Name = "butNewFolder";
            this.butNewFolder.Size = new Size(0x4b, 0x17);
            this.butNewFolder.TabIndex = 6;
            this.butNewFolder.Text = "&Tạo thư mục";
            this.butNewFolder.UseVisualStyleBackColor = true;
            this.butNewFolder.Click += new EventHandler(this.butNewFolder_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
           // base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x193, 0x120);
            base.Controls.Add(this.butNewFolder);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.butClose);
            base.Controls.Add(this.butSave);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.txtTitle);
            base.Controls.Add(this.treeFav);
            base.Name = "FavoriteEditor";
            this.Text = "Favorites";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void refreshTree()
        {
            this.treeFav.Nodes.Clear();
            this.treeFav.Nodes.Add(this._tree.getTreeNode(this._folderImageIndex, this._leafImageIndex));
        }

        public void setFolderImageIndex(int index)
        {
            this._folderImageIndex = index;
        }

        public void setImageList(ImageList imgList)
        {
            this._imgList = imgList;
            this.treeFav.ImageList = this._imgList;
        }

        public void setLeafImageIndex(int index)
        {
            this._leafImageIndex = index;
        }
    }
}
