using System;
using System.Collections.Generic;

namespace TheCauchyProblem
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine("Which way do you want to choose step(choose number)?\n1. Automatic\n2. Const");
			int step_way = Convert.ToInt32(Console.ReadLine ());
			List<double[]> solution = Runge_Kutta (1, 2, 2, 1, 0.1, step_way);
			foreach (double[] y in solution) {
				Console.WriteLine ("x: {0}, y: {1}",y[0], y[1]);
			}
			Console.WriteLine ("Hello World!");
		}
		public static List<double[]> Runge_Kutta(double a, double b, double y_primary, int x_primary, double first_step, int step_way){

			/////////////////WITH AUTOMATIC STEP/////////////////////
			List<double[]> auto_solution = new List<double[]>();
			auto_solution.Add(new double[2] {x_primary,y_primary});
			int steps_count = 0;

			double y_avt = y_primary;//крок 1
			double x_avt = x_primary;//крок 1

			double h = first_step;//крок 1
			double eps = Math.Pow (10, -4);

			double y_h = y_primary;
			double y_h2 = y_primary;

			double epsh = 0;
			double epsh2 = 0;

			double temp_h = 0;
			double temp_h2 = 0;

			for (int i = 0; b-x_avt>eps; i++) {

				if (h > b - x_avt)
					h = b - x_avt;

				temp_h = y_h + increment(x_avt, y_avt, h);//крок 2
				temp_h2 = y_h2 + increment(x_avt, y_avt, h/2);//крок 2

				epsh = eps_h (temp_h, temp_h2);
				epsh2 = eps_h2 (temp_h, temp_h2);

				if (Math.Abs (epsh2) <= eps) {//крок 3
					y_h = temp_h;
					y_h2 = temp_h2;

					y_avt = y_h2 + eps_h2 (y_h, y_h2);//крок 3

					x_avt += h;//крок 3

					auto_solution.Add (new double[2] {Math.Round(x_avt,6),Math.Round(y_avt,6)});
					steps_count++;

					if (Math.Abs (epsh) <= eps){//крок 4
						h *= 2; 
					}//далі крок 2
					
				} else h /= 2; //далі на крок 2
			}

			/////////////////WITH CONST STEP/////////////////////
			if(step_way == 2){

				List<double[]> const_solution = new List<double[]> ();
				double y = y_primary;
				double x = x_primary;

				double step = (b - a) / (double)steps_count;

				for (int i = 0; i < steps_count; i++) {;
					const_solution.Add (new double[2] {x,Math.Round(y,6)});
					y += increment (x, y, step);
					x += step;
				}
				return const_solution;
			}
			return auto_solution;
		}
		public static double k_1(double x, double y, double h){
			double ret = h * f (x, y);
			return ret;
		}
		public static double k_2(double x, double y, double h){
			double ret = h * f (x + h / 2, y + k_1 (x, y, h) / 2);
			return ret;
		}
		public static double k_3(double x, double y, double h){
			double ret = h * f (x + h / 2, y + k_2 (x, y, h) / 2);
			return ret;
		}
		public static double k_4(double x, double y, double h){
			double ret = h * f (x + h, y + k_3 (x, y, h));
			return ret;
		}
		public static double eps_h2(double y1, double y2){
			return (y2 - y1) / 15;
		}
		public static double eps_h(double y1, double y2){
			return 16*(y2 - y1) / 15;
		}
		public static double increment(double x, double y, double h){
			return (k_1 (x, y, h) + 2 * k_2 (x, y, h) + 2 * k_3 (x, y, h) + k_4 (x, y, h)) / 6;
		}
		public static double f(double x, double y){
			double ret = (Math.Pow (x, 2) - Math.Pow (y, 2)) / (2 * x * y);
			return ret;
		}

	}
}
