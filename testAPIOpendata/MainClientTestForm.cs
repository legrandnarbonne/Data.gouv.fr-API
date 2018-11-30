using APIODataGouv;
using APIODataGouv.Classes.APIObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace testAPIOpendata
{
    public partial class MainClientTestForm : Form
    {
        APIRequest _r;

        int _pageCount;
        int _totalItem;
        int _currentPage;

        public MainClientTestForm()
        {
            InitializeComponent();

            comboBox1.Items.Add("Organization");
            comboBox1.Items.Add("DataSet");

            comboBox1.SelectedIndex = 0;
            txtKey.Text = "";

        }

        #region Display function
        /// <summary>
        /// Set display when starting and stoping event
        /// </summary>
        /// <param name="v">True when starting work</param>
        private void setWorking(bool v)
        {
            Cursor.Current = v ? Cursors.WaitCursor : Cursors.Default;
            button6.Enabled = !v;
        }

        /// <summary>
        /// Build MessageBox from APIResponse
        /// </summary>
        /// <param name="response"></param>
        private void displayResponseMessage(APIResponse response, bool displaySuccess = true)
        {
            if (response.Status == HttpStatusCode.OK || response.Status == HttpStatusCode.Created)
            {
                if (displaySuccess)
                    MessageBox.Show("L'opération c'est déroulée avec succés", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //{"errors": {"title": ["Ce champ est requis."]}}
                var deserializedResponse = response.Content == null ? null : JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content);
                var message = "";

                if (deserializedResponse != null)
                {
                    message = deserializedResponse.Keys.Contains("message") ?
                                deserializedResponse["message"].ToString() :
                                (deserializedResponse.Keys.Contains("errors") ?
                                    deserializedResponse["errors"].ToString().Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "") :
                                    "");
                }

                MessageBox.Show(response.Status + ": " + message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void displayPage(int totalResponseItem)
        {
            txtMaxPage.Text = "/..";
            btnNext.Enabled = btnPrevious.Enabled = false;

            if (totalResponseItem == -1)
            {
                return;
            }

            _pageCount = (int)Math.Ceiling(((double)totalResponseItem / _r.PageSize));
            _currentPage = _r.RequestedPage;
            _totalItem = totalResponseItem;

            txtMaxPage.Text = "/" + _pageCount;

            managePager();

        }

        private void managePager()
        {
            txtPage.Text = _currentPage.ToString();

            btnNext.Enabled = _pageCount > _currentPage;
            btnPrevious.Enabled = _currentPage > 1;
        }

        /// <summary>
        /// Display collection in treeview
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="parent"></param>
        /// <param name="addChild"></param>
        private void display<T>(IList<T> collection, TreeNode parent = null, string addChild = null)
        {
            if (collection == null) return;
            if (!chkKeepPreviousRequest.Checked && parent == null) treeView1.Nodes.Clear();

            var root = parent ?? new TreeNode(collection.GetType().GetGenericArguments().Single().Name);

            foreach (var o in collection)
            {
                var node = new TreeNode { Tag = o, Text = o.ToString() };
                if (!string.IsNullOrEmpty(addChild))
                {
                    var childs = (IEnumerable<object>)o.GetType().GetProperty(addChild).GetValue(o, null);
                    if (childs != null)
                        foreach (var child in childs)
                        {
                            var childNode = new TreeNode { Tag = child, Text = child.ToString() };
                            node.Nodes.Add(childNode);
                        }
                }
                root.Nodes.Add(node);
            }

            if (parent == null)
            {
                treeView1.Nodes.Add(root);
                if (root.Nodes.Count > 0)
                    treeView1.SelectedNode = root.Nodes[0];
            }

            root.Expand();

            //if (collection is APIRequest.ResponseList)
        }
        #endregion

        #region Events handlers
        private void txtPageSize_TextChanged(object sender, EventArgs e)
        {
            var pageSize = getNumberFromTextField(txtPageSize);

            _pageCount = (int)Math.Ceiling(((double)_totalItem / pageSize));
            _currentPage = 1;

            txtPage.Text = "1";
            txtMaxPage.Text = "/" + _pageCount;
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            _currentPage++;
            managePager();

            search();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _currentPage--;
            managePager();

            search();
        }
        private void txtURL_TextChanged(object sender, EventArgs e)
        {
            _r = new APIRequest(txtURL.Text, txtKey.Text);
        }

        private void txtPage_TextChanged(object sender, EventArgs e)
        {
            _currentPage = getPageNumber();
            managePager();
        }

        private int getPageNumber()
        {
            var page = getNumberFromTextField(txtPage);

            if (page > _pageCount) page = _pageCount;
            if (page == 0) page = 1;

            return page;
        }

        private int getNumberFromTextField(ToolStripTextBox tb)
        {
            int n;
            if (!int.TryParse(tb.Text, out n)) return 1;

            return n;
        }
        /// <summary>
        /// Update selected object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void update_Click(object sender, EventArgs e)
        {
            setWorking(true);
            btnUpdate.Enabled = false;
            var result = new APIResponse();

            var obj = (APIObject)propertyGrid1.SelectedObject;

            if (obj == null) return;

            if (((IAPIObject)obj).IsParentIdRequired())
                result = _r.UpdateObject((IAPIObject)obj, (APIObject)treeView1.SelectedNode.Parent.Tag);
            else
                result = _r.UpdateObject((IAPIObject)obj);

            displayResponseMessage(result);

            setWorking(false);
        }

        /// <summary>
        /// Display item list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_Click(object sender, EventArgs e)
        {
            search();
        }

        /// <summary>
        /// Open resource file
        /// </summary>
        private void openResources()
        {
            if (treeView1.SelectedNode == null) MessageBox.Show("Sélectionnez une resource");
            setWorking(true);
            var res = (Resource)treeView1.SelectedNode.Tag;

            //if (res.filetype != FileTypes.file) MessageBox.Show("Cette resource n'est pas un fichier");

            using (var client = new WebClient())
            {
                client.DownloadFile(res.url, $"tmp.{res.format}");
            }
            setWorking(false);
            Process.Start($"tmp.{res.format}");
        }

        /// <summary>
        /// Update propertygrid on treeview selection change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            propertyGrid1.SelectedObject = treeView1.SelectedNode.Tag;
        }

        /// <summary>
        /// Set displayed option in treeview context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextTreeview_Opening(object sender, CancelEventArgs e)
        {
            int[] toDisplay = null;

            switch (treeView1.SelectedNode.Tag.GetType().Name)
            {
                case "Organization":
                    toDisplay = new int[] { 0, 5 };
                    break;
                case "DataSet":
                    toDisplay = new int[] { 4, 6 };
                    break;
                case "Resource":
                    toDisplay = new int[] { 1, 2, 3 };
                    break;
                default:
                    e.Cancel = true;
                    return;
            }

            for (int i = 0; i < contextTreeview.Items.Count; i++)
            {
                contextTreeview.Items[i].Visible = toDisplay.Contains(i);
            }
        }

        private void obtenirLesRessourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openResources();
        }

        private void obtenirLesDatasetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getDataSets();
        }
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag is Organization) getDataSets();

            if (treeView1.SelectedNode.Tag is Resource) openResources();
        }
        private void propertyGrid1_SelectedObjectsChanged(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            btnUpdate.Enabled = true;
        }

        private void remplacerLeFichierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setWorking(true);

            var res = (Resource)treeView1.SelectedNode.Tag;

            var dset = (APIODataGouv.Classes.APIObject.DataSet)treeView1.SelectedNode.Parent.Tag;

            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _r.Id = res.id;
                _r.ParentId = dset.id;

                var rep = _r.UploadFile(openFileDialog.FileName);
            }

            displayResponseMessage(_r.LastResponse);

            treeView1.SelectedNode = getParentOrganization(treeView1.SelectedNode);
            getDataSets();

            setWorking(false);
        }

        private void effacerLaRessourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete();
        }

        private void ajouterUneRessourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addObject<Resource>();
        }

        private void ajouterUnDatasetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addObject<APIODataGouv.Classes.APIObject.DataSet>();
        }

        private void supprimerLeDatasetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete();
        }
        #endregion

        #region API Methods calls
        /// <summary>
        /// Get object list
        /// </summary>
        private void search()
        {
            setWorking(true);

            _r.PageSize = getNumberFromTextField(txtPageSize);
            _r.RequestedPage = getNumberFromTextField(txtPage);

            if (string.IsNullOrEmpty(txtId.Text + txtName.Text))
            {
                _r.Id = _r.ParentId = null;

                switch (comboBox1.SelectedItem.ToString())
                {
                    case "Organization":
                        display(_r.GetList<Organization>().Items);
                        break;
                    case "DataSet":
                        display(_r.GetList<APIODataGouv.Classes.APIObject.DataSet>().Items, null, "resources");
                        break;
                }

                displayPage(_r.LastResponseTotalItem);
                displayResponseMessage(_r.LastResponse, false);
            }
            else
            {
                _r.Id = txtId.Text;
                _r.ObjectName = txtName.Text;
                treeView1.Nodes.Clear();

                switch (comboBox1.SelectedItem.ToString())
                {
                    case "Organization":
                        display(new List<Organization> { _r.GetItem<Organization>() });
                        break;
                    case "DataSet":
                        display(new List<APIODataGouv.Classes.APIObject.DataSet> { _r.GetItem<APIODataGouv.Classes.APIObject.DataSet>() });
                        break;
                }

                displayPage(1);
                displayResponseMessage(_r.LastResponse, false);
            }



            setWorking(false);
        }
        /// <summary>
        /// Perform add object operation
        /// </summary>
        /// <typeparam name="T">Requested object type</typeparam>
        private void addObject<T>() where T : IAPIObject, new()
        {
            setWorking(true);

            _r = new APIRequest(txtURL.Text, txtKey.Text);
            var o = new T();
            var frm = new FrmNew(o);

            if (frm.ShowDialog() == DialogResult.OK)
            {

                displayResponseMessage(_r.AddObject(o, (APIObject)treeView1.SelectedNode.Tag));

                //refresh treeview
                if (o is APIODataGouv.Classes.APIObject.DataSet || o is Resource)
                {
                    treeView1.SelectedNode = getParentOrganization(treeView1.SelectedNode);
                    getDataSets();
                }

                setWorking(false);
            }
        }

        /// <summary>
        /// Perform deletion
        /// </summary>
        private void delete()
        {
            _r = new APIRequest(txtURL.Text, txtKey.Text);
            //delete / datasets /{ dataset}/
            if (MessageBox.Show("Confirmez vous la suppression de cet élément?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            setWorking(true);

            var currentObj = (APIObject)treeView1.SelectedNode.Tag;

            if (treeView1.SelectedNode.Parent.Tag is APIObject)
                _r.ParentId = ((APIObject)treeView1.SelectedNode.Parent.Tag).id;

            displayResponseMessage(_r.Delete(currentObj));

            //refresh treeview
            if (currentObj is APIODataGouv.Classes.APIObject.DataSet || currentObj is Resource)
            {
                treeView1.SelectedNode = getParentOrganization(treeView1.SelectedNode);
                getDataSets();
            }

            setWorking(false);
        }

        /// <summary>
        /// Update Dataset list
        /// </summary>
        private void getDataSets()
        {
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("Sélectionnez une organisation");
                return;
            }

            setWorking(true);

            _r = new APIRequest(txtURL.Text, txtKey.Text);
            var org = (Organization)treeView1.SelectedNode.Tag;


            _r.PageSize = 999;
            _r.ParentId = org.id;

            treeView1.SelectedNode.Nodes.Clear();

            display(_r.GetList<APIODataGouv.Classes.APIObject.DataSet>().Items, treeView1.SelectedNode, "resources");

            displayResponseMessage(_r.LastResponse, false);

            setWorking(false);
        }
        #endregion

        #region Helper
        /// <summary>
        /// Search in parent to find organisation return organization node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private TreeNode getParentOrganization(TreeNode node)
        {
            if (node.Tag is Organization) return node;

            while (node.Parent != null)
            {
                if (node.Parent.Tag is Organization)
                    return node.Parent;

                node = node.Parent;
            }

            return null;
        }


        #endregion


    }
}
