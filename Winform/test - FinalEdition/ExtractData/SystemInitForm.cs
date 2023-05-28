namespace ExtractData
{
    public partial class SystemInitForm : Form
    {
        public SystemInitForm(string msg)
        {
            InitializeComponent();

            Lbl_Msg.Text = msg;
        }
    }
}
