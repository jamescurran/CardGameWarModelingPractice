using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameWar.Objects
{
    /// <summary>
    /// Anything "written" to this writer is ignored.
    /// </summary>
    public class NullTextWriter : TextWriter 
    {
        public override Encoding Encoding { get { return Encoding.UTF8;} }

    }
}
