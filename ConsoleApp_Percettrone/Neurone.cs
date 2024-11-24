namespace ConsoleApp_Percettrone
{
	internal class Neurone

	{
		byte[][] _input;
		byte[] _target;
		float[] _weights;
		int _epoch = 0;

		public Neurone(float LR, int bias, byte[][] input, byte[] target, float[] starting_weights)
		{
			LearningRate = LR;
			Bias = bias;
			_input = input;
			_target = target;
			_weights = starting_weights;

			Learn(); // inizia il processo di apprendimento
		}

		public int Learn()
		{
			_epoch++;
			for (int i = 0; i < _input[0].Length; i++)
			{
				float weighted_sum = Bias * _weights[0] + _input[0][i] * _weights[1] + _input[1][i] * _weights[2]; // calcola la media pesata
				int threshold = weighted_sum > 0 ? 1 : 0;
				int error = _target[i] - threshold;
			}
			return 0;
		}

		public float LearningRate { get; }
		public int Bias { get; } // I0
		public int[][] Data { get; set; } // data contiene gli array degli input (I1, I2, ..., In) e l'array del target
	}
}
