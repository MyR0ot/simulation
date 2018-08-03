using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dispersion
{
	class Prizes
	{

		public double[] FreqMult;  // частоты множителей
		public int[] Mults;        // множители

		public Prizes()
		{
			FreqMult = new double[] {0.73518, 0.18366, 0.075,  0.005, 0.001,  0.0001, 0.00005};
			Mults = new int[] { 2, 4, 10, 25, 120, 240, 360 };
		}
	}
}
