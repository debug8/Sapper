using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using View;

namespace Controller
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IModelMineField model = new MineField(10);
            var view = new MainForm();

            var controller = new Controller(view, model);
            controller.Initialize();

            Application.Run(view);
        }
    }
}
