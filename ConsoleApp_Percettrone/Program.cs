using ConsoleApp_Percettrone;

// tabella verità AND
decimal[][] dataset = [
			[0, 0],
			[0, 1],
			[1, 0],
			[1, 1]
		];
int[] target = [0, 0, 0, 1]; // output desiderato
decimal[] pesi = [1, 1, -0.8m];
//decimal[] pesi = [1.2m, 0.8m, 0.4m];
double LR = 0.2;
double bias = -1.0;

Percettrone perc = new Percettrone(pesi, LR, bias);
//Percettrone perc = new Percettrone(3, LR, bias);

Console.WriteLine("Addestramento...");
perc.Allena(dataset, target);