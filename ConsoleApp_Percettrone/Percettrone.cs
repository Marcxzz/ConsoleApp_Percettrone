using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;

namespace ConsoleApp_Percettrone
{
	internal class Percettrone
	{
        public decimal[] Pesi { get; set; }
        public double Bias { get; set; }
        public double LearningRate { get; set; }

		/// <summary>
		/// Costruttore della classe Percettrone
		/// </summary>
		/// <param name="pesi">Array di pesi iniziali</param>
		/// <param name="learningRate">Learning Rate</param>
		/// <param name="bias">Bias</param>
        public Percettrone(decimal[] pesi, double learningRate, double bias)
        {
			Pesi = pesi;
			LearningRate = learningRate;
			Bias = bias;
        }

		/// <summary>
		/// Costruttore della classe Percettrone
		/// </summary>
		/// <param name="dimensionePesi">Dimensione dell'array di pesi</param>
		/// <param name="learningRate">Learning Rate</param>
		/// <param name="bias">Bias</param>
        public Percettrone(int dimensionePesi, double learningRate, double bias) // overloading del costruttore
		{
			Pesi = new decimal[dimensionePesi];
			Bias = bias;
			LearningRate = learningRate;

			GeneraCasualmente();
		}

		private void GeneraCasualmente()
		{
			// genera casualmente i pesi
			Random rnd = new Random();
			for (int i = 0; i < Pesi.Length; i++)
				Pesi[i] = (decimal)rnd.NextSingle() * (2 - (-2)) + (-2); // range [-2, +2)
		}

		// calcola l'attivazione
		private int CalcAttivazione(decimal[] input)
		{
			if (input.Length != Pesi.Length)
				throw new ArgumentException("La dimensione dell'input non corrisponde alla dimensione dei pesi");

			decimal sommaPesata = 0.0m;
			for (int i = 0; i < Pesi.Length; i++)
			{
				sommaPesata += input[i] * Pesi[i];
			}

			return sommaPesata > 0 ? 1 : 0;
		}

		/// <summary>
		/// Addestramento del percettrone
		/// </summary>
		/// <param name="dataset">Array dei dati di input</param>
		/// <param name="targetArray">Array dei target</param>
		public void Allena(decimal[][] dataset, int[] targetArray)
		{
			Console.WriteLine($"Pesi iniziali: {string.Join("  ", Pesi)}");
			int epoca = 1;
			int erroreEpoca = 0;
			for (int i = 0; i < dataset.Length; i++)
			{
				int target = targetArray[i];
				decimal[] datasetRow = new decimal[dataset[0].Length + 1];
				datasetRow[0] = (decimal)Bias; // mette il bias nella prima posizione dell'array
                for (int x = 1; x < datasetRow.Length; x++) // mette gli altri input nell'array
					datasetRow[x] = dataset[i][x - 1];

				int attivazione = CalcAttivazione(datasetRow);

				// calcola l'errore
				int errore = target - attivazione;
				erroreEpoca += Math.Abs(errore);

				// aggiorna i pesi
				for (int j = 0; j < Pesi.Length; j++)
					Pesi[j] += (decimal)LearningRate * datasetRow[j] * errore;

				if (i + 1 == dataset.Length) // controlla se l'epoca è finita
				{
					Console.WriteLine($"Epoch {epoca}: {string.Join("  ", Pesi)}");
					if (erroreEpoca == 0) // controlla l'errore totale dell'epoca
					{
						Console.WriteLine($"CONVERGED at Epoch {epoca}");
						MostraOutput();
						return;
					}

					i = 0;
					erroreEpoca = 0;
					epoca++;
				}
			}
		}

		// mostra in console i pesi
		public void MostraOutput()
		{
			Console.WriteLine("Pesi: " + string.Join("  ", Pesi));
		}
	}
}