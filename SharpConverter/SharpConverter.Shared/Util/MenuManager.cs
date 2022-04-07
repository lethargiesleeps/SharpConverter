using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpConverter.Shared.Util
{
    public class MenuManager
    {
        public string Input { get; private set; }
        public string Output { get; private set; }
        public NumberSystemConverter NumberSystemConverter { get; private set; }


        public MenuManager()
        {
            this.Input = "";
            this.Output = "";
        }

        public MenuManager(NumberSystemConverter numberSystemConverter)
        {
            this.NumberSystemConverter = numberSystemConverter;
        }


    }
}
