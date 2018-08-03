using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dispersion
{
	class Simulation
	{
		private int n;              // кол-во игр в симуляции
		private double chipEv;      // ChipEV/tournament
		private double winPer;      // процент побед
		private int startBR;        // стартовое кол-во байинов
		private Prizes prizes;      // информация о призовых
		public int[] HistoryBI;     // история симуляции
		private double chipEV_result;   // реальность
		private double win_result;      // реальность
		private int amount_win = 0;     // кол-во побед
		Random rnd;                     // генератор случайных чисел
		private double mathExpect;      // ожидание в BI
		private double rake;			// рейк 1 спина
		public int SizeBI { get; set; } // размер байина
		public bool Fail { get; set; }	// залился?

		/// <summary>
		/// Конструктор, запускающий симуляцию
		/// </summary>
		/// <param name="n">Кол-во спинов</param>
		/// <param name="ev">chipEV/tournament</param>
		/// <param name="startBI">Start bankroll (in BI)</param>
		/// <param name="sizeBI">Size of BI($)</param>
		public Simulation(int n, double ev, int startBI = 0, int sizeBI = 1, double rake = 0.08)
		{
			this.n = n;
			this.chipEv = ev;
			this.winPer = (500 + chipEv) / 1500;
			this.rake = rake;
			Fail = false;
			startBR = startBI;
			prizes = new Prizes();
			HistoryBI = new int[n + 1];
			rnd = new Random();
			SizeBI = sizeBI;
			mathExpect = (((chipEv + 500) / 1500) * (1 - rake)*3 - 1) * n; // FIX
			
			StartSimulation();
		}

		private void StartSimulation()
		{
			HistoryBI[0] = startBR;

			for (int i = 1; i <= n; i++)
			{
				double change = rnd.NextDouble();
				int index = SearchIndex(change);

				if (rnd.NextDouble() <= winPer)
				{
					HistoryBI[i] = HistoryBI[i - 1] + prizes.Mults[index] - 1;
					amount_win++;
				}
				else
					HistoryBI[i] = HistoryBI[i - 1] - 1;
				if (HistoryBI[i] <= 0)
					Fail = true;
			}
			win_result = (double)amount_win / n;
			chipEV_result = win_result * 1500 - 500;
		}

		private int SearchIndex(double target)
		{
			int res = prizes.FreqMult.Length - 1;
			double sum = 0;
			for (int i = prizes.FreqMult.Length - 1; i >= 0; i--)
			{
				sum += prizes.FreqMult[i];
				if (target <= sum)
				{
					res = i;
					break;
				}
			}

			return res;
		}

		public int[] GetData()
		{
			return HistoryBI;
		}

		public override string ToString()
		{
			string startBI = "start BI: " + startBR.ToString() + "\n";
			string finishBI = "finish BI: " + HistoryBI[n].ToString() + "\n";
			string mathBI = "math expectation: " + mathExpect.ToString() + "\n";
			string ev = "chipEV: " + chipEv.ToString() + "\treal chipEV: " + chipEV_result.ToString() + "\nwinRate: " + winPer.ToString() + "\treal winRate: " + win_result.ToString() + "\n";
			string total = "amount spins: " + n.ToString() + "\n";
			string chip_result = "real chipEV: " + chipEV_result.ToString() + "\n";
			string success = "success: " + Fail.ToString() + "\n";

			StringBuilder result = new StringBuilder();
			result.Append(startBI);
			result.Append(finishBI);
			result.Append(mathBI);
			result.Append(ev);
			result.Append(success);
			result.Append(total);
			return result.ToString();
		}
	}
}
