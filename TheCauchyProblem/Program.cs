using System;
using System.Collections.Generic;

namespace TheCauchyProblem
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			List<double> solution = Runge_Kutta (1, 2, 2, 1, 10);
			foreach (double y in solution) {
				Console.WriteLine (y);
			}
			Console.WriteLine ("Hello World!");
		}
		public static List<double> Runge_Kutta(double a, double b, double y_primary, int x_primary, double n, double first_step){
			List<double> solution = new List<double> ();
			double y = y_primary;
			double x = x_primary;

			double step = (b - a) / n;

			double fi_0 = step*f(x_primary, y_primary);
			double fi_1 = step*f(x_primary+2 * step/3, y_primary + 2*fi_0/3);

			for (int i = 0; i < n; i++) {
				Console.WriteLine (y);
				Console.WriteLine (x);
				Console.WriteLine (fi_0);
				Console.WriteLine (fi_1);
				Console.WriteLine ("\n");
				solution.Add (Math.Round(y,6));
				y += (fi_0 + 3 * fi_1)/4;
				x += step;
				fi_0 = step*f(x, y);
				fi_1 = step*f(x+2*step/3, y + 2*fi_0/3);
			}
			return solution;

			/////////////////////////////////////////////////////////////

			double y_avt = y_primary;
			double x_avt = x_primary;

			double h = first_step;
			double eps = Math.Pow (10, -h);

			double fi_0_h = h * f (x_avt, y_avt);
			double fi_1_h = h * f (x_avt + 2 * h / 3, y_avt + 2 * fi_0 / 3);

			double fi_0_h2 = h * f (x_avt, y_avt) / 2;
			double fi_1_h2 = h * f (x_avt + h / 3, y_avt + 2 * fi_0 / 3) / 2;

			double y_h = (fi_0_h + 3 * fi_1_h) / 4;
			double y_h2 = (fi_0_h2 + 3 * fi_1_h2) / 4;

			double eps_h = 4 * (y_h2 - y_h) / 3;
			double eps_h2 = (y_h2 - y_h) / 3;

			if (Math.Abs (eps_h2) <= eps) {
				x_avt += h;

				fi_0_h = h * f (x_avt, y_avt);
				fi_1_h = h * f (x_avt + 2 * h / 3, y_avt + 2 * fi_0 / 3);

				fi_0_h2 = h * f (x_avt, y_avt) / 2;
				fi_1_h2 = h * f (x_avt + h / 3, y_avt + 2 * fi_0 / 3) / 2;

				y_h = (fi_0_h + 3 * fi_1_h) / 4;
				y_h2 = (fi_0_h2 + 3 * fi_1_h2) / 4;

				eps_h2 = (y_h2 - y_h) / 3;

				y_avt = y_h2 + eps_h2;
				//далі на крок 4
			} else {
				h /= 2;
				x_avt += h;
				//далі на крок 2
			}


		}
		public static double f(double x, double y){
			double ret = (Math.Pow (x, 2) + Math.Pow (y, 2)) / (2 * x * y);
			return ret;
		}

	}
}
