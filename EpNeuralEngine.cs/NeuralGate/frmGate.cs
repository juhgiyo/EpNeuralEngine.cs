using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralGate
{
    public partial class frmGate : Form
    {
        private DigitalNeuralGate gate;
        public frmGate()
        {
            InitializeComponent();
        }

        private void btnTrain1000_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 1000; i++)
                {
                    TrainOnce();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error. Check whether the input is valid - " + ex.Message);
            }
            
        }

        private void btnTrainOnce_Click(object sender, EventArgs e)
        {
            try
            {
                TrainOnce();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error. Check whether the input is valid - " + ex.Message);
            }
            
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                tbRout1.Text = gate.Run(Convert.ToInt32(tbR11.Text), Convert.ToInt32(tbR12.Text)).ToString();
                tbRout2.Text = gate.Run(Convert.ToInt32(tbR21.Text), Convert.ToInt32(tbR22.Text)).ToString();
                tbRout3.Text = gate.Run(Convert.ToInt32(tbR31.Text), Convert.ToInt32(tbR32.Text)).ToString();
                tbRout4.Text = gate.Run(Convert.ToInt32(tbR41.Text), Convert.ToInt32(tbR42.Text)).ToString();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void TrainOnce()
        {
            gate.Train(Convert.ToInt32(tb11.Text),Convert.ToInt32(tb12.Text),Convert.ToInt32(tbOut1.Text));
            gate.Train(Convert.ToInt32(tb21.Text),Convert.ToInt32(tb22.Text),Convert.ToInt32(tbOut2.Text));
            gate.Train(Convert.ToInt32(tb31.Text),Convert.ToInt32(tb32.Text),Convert.ToInt32(tbOut3.Text));
            gate.Train(Convert.ToInt32(tb41.Text), Convert.ToInt32(tb42.Text), Convert.ToInt32(tbOut4.Text));
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            gate = new DigitalNeuralGate();
        }

        private void frmGate_Load(object sender, EventArgs e)
        {
            gate = new DigitalNeuralGate();
            lblTrainInfo.Text = "Initialized with TT of XOR";
            tbOut1.Text = "0";
            tbOut2.Text = "1";
            tbOut3.Text = "1";
            tbOut4.Text = "0";
        }

        private void btnAnd_Click(object sender, EventArgs e)
        {
            lblTrainInfo.Text = "Initialized with TT of AND";
            tbOut1.Text = "0";
            tbOut2.Text = "0";
            tbOut3.Text = "0";
            tbOut4.Text = "1";
        }

        private void btnOr_Click(object sender, EventArgs e)
        {
            lblTrainInfo.Text = "Initialized with TT of OR";
            tbOut1.Text = "0";
            tbOut2.Text = "1";
            tbOut3.Text = "1";
            tbOut4.Text = "1";
        }

        private void btnXor_Click(object sender, EventArgs e)
        {
            lblTrainInfo.Text = "Initialized with TT of XOR";
            tbOut1.Text = "0";
            tbOut2.Text = "1";
            tbOut3.Text = "1";
            tbOut4.Text = "0";
        }


    }
}
