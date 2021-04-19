using CPoolUtil.Core;
using System;
using System.Windows.Forms;

namespace CPoolUtil.Interface
{
    public partial class frmDuplicateSoldiers : Form
    {
        private IOutputter _outputter = new ConsoleOutputter();

        public frmDuplicateSoldiers()
        {
            InitializeComponent();
        }
    }
}