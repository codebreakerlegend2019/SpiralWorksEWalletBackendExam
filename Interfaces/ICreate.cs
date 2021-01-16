using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiralWorksWalletBackendExam.Interfaces
{
    public interface ICreate<T> where T: class
    {
        void Create(T model);
    }
}
