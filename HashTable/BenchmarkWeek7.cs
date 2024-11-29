using BenchmarkDotNet.Attributes;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace HashTable
{
	[MemoryDiagnoser]
	public class BenchmarkWeek7
	{
		// Files that are not compiled are not available where the exe file is
		// right click on the file to be added, click properties, on the first option, click until it says copy if newer
		public readonly String FILE_PATH = "food.txt";
		private String[]? foodItems;
		private String[]? randFoodItems;
		private String[] itemsToFind = [];
        public (String, float)[]? menuTT;
		public Hashtable menuHT = new Hashtable();//Can't be same as namespace

		[GlobalSetup]
		public void GlobalSetup()
		{
			Random rand = new Random(42);
			foodItems = File.ReadAllLines(FILE_PATH);

			menuTT = new (String, float)[foodItems.Length];

			randFoodItems = foodItems.OrderBy(i => rand.Next()).ToArray();

			//Randomly put food items into both menus with random prices
			for (int i = 0; i < randFoodItems.Length; i++)
			{
				String food = randFoodItems[i];
				float price = rand.NextSingle() * 19 + 0.99f;
				
				menuTT[i] = (food, price);
				menuHT.Add(food, price);
			}
		}

		[IterationSetup]
		public void SetuIteration()
		{
            // Get 10 items to find randomly
            itemsToFind = randFoodItems!
				.OrderBy(i => Random.Shared.Next())
				.Take(10)
				.ToArray();
        }

		[Benchmark]
		public void SearchTupleType()
		{
			foreach (String item in itemsToFind)
			{
				foreach ((String food, float price) in menuTT!)
				{
					if (food.Equals(item))
					{
						break;
					}
				}
			}
		}

		[Benchmark]
		public void SearchHashTable()
		{
            foreach (String item in itemsToFind)
            {
				Debug.Assert(menuHT[item] != null);
            }
        }

		[Benchmark]
		[IterationCount(1)]
		public void ShowTupleTypeMemory()
		{
			//Deep clone menuTT
			(String, float)[] menuTTClone = new (String, float)[menuTT!.Length];
			for (int i = 0; i < menuTT.Length; i++)
			{
				menuTTClone[i] = menuTT[i];
			}
		}

		[Benchmark]
		[IterationCount(1)]
		public void ShowHashtableMemory()
		{
			//Deep clone menuHT
			Hashtable menuHTClone = new Hashtable();
			foreach (DictionaryEntry kvp in menuHT)
			{
				menuHTClone.Add(kvp.Key, kvp.Value);
			}

		}
	}
}
