// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using O2.Interfaces.O2Core;
using O2.Kernel;
using O2.Kernel.Interfaces;
using O2.Kernel.InterfacesBaseImpl;

namespace O2.Scanners
{
    class DI
    {
        static DI()
        {
            log = PublicDI.log;
            config = PublicDI.config;
        }

        public static IO2Log log { get; set; }
        public static KO2Config config { get; set; }
    }
}
