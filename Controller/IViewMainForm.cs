using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;

namespace Controller
{
    public interface IViewMainForm : IViewMineField
    {
        event Action ResetForm;
    }
}
