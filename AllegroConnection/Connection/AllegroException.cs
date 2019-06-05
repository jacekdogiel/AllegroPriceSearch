using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllegroPriceSearch
{
    class AllegroException : Exception
    {
        public AllegroException(string message, Exception innerException)
            :base(message,innerException)
        {

        }
    }
}
