using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IContainer
    {
        int Weight { get; }
        bool IsValuable { get; }
        bool CanStackOnTop(IContainer lower);
    }

}
