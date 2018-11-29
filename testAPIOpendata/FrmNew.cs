using APIODataGouv.Classes.APIObject;
using System.Windows.Forms;

namespace testAPIOpendata
{
    public partial class FrmNew : Form
    {
        public FrmNew(IAPIObject obj)
        {
            InitializeComponent();

            propertyGrid1.SelectedObject=obj;
        }
    }
}
