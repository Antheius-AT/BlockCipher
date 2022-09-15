using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptools.Interfaces
{
    public interface ICryptographicKey
    {
        byte[] KeyBytes { get; }
    }
}
