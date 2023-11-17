using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Data_Seeding
{
    public static class SeededProducts
    {
        private static string _seedBackup = "300!Bananer!66!per kg" +
                                    "\n301!Äpplen!25,50!per kg" +
                                    "\n302!Kaffe!65,50!per unit" +
                                    "\n303!Choklad!19,90!per unit" +
                                    "\n304!Lösgodis!89,90!per kg" +
                                    "\n305!Rågbröd!55,00!per unit" +
                                    "\n306!Toalettpapper!32,00!per unit" +
                                    "\n307!Kex!25,60!per unit" +
                                    "\n308!Vattenmelon!55,00!per kg" +
                                    "\n309!Smör!79,00!per kg" +
                                    "\n310!Gott & Blandat!29,00!per unit" +
                                    "\n311!Hushållsost!79,00!per kg" +
                                    "\n312!Kycklingfilé!119,00!per kg" +
                                    "\n313!Yoggi!40,00!per unit" +
                                    "\n314!Tomater på burk!11,00!per unit" +
                                    "\n315!Stekpanna!339,00!per unit" +
                                    "\n316!Dammsugare!999,99!per unit" +
                                    "\n317!Västerbottensost!10,00!per kg" +
                                    "\n318!Oxfilé!399,99!per kg" +
                                    "\n319!Päron!35,99!per kg" +
                                    "\n320!Pasta!19,99!per unit";

        public static readonly string[] _seededProducts = _seedBackup.Split('\n');
    }
}
