using System.Windows.Controls;
using VMS.TPS.Common.Model.API;
using esapi.ViewModel;

namespace esapi.UI
{
    public partial class PatientControl : UserControl
    {
        public PatientControl()
        {
            InitializeComponent();
        }

        public void SetPatient(Patient patient)
        {
            //MyPropertyGrid.SelectedObject = new PatientViewModel(patient);
        }
    }
}
