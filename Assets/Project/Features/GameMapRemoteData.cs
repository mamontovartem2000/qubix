using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Features
{
	[Serializable]
	public class GameMapRemoteData
	{
		public byte[] bytes;
		public int offset;

		public GameMapRemoteData(TextAsset sourceMap)
		{
			var omg = sourceMap.text.Split('\n');
			ParseMap(omg);
		}

		public GameMapRemoteData(string sourceMap)
		{
			var omg = sourceMap.Split('\n');
			ParseMap(omg);
		}

		private void ParseMap(string[] lines)
		{
			List<byte> byteList = new List<byte>();

			var arr = lines[0].Split(' ');
			offset = arr.Length + 2;
			var wall = CreateWalls();
			byteList.AddRange(wall);

			foreach (var line in lines)
			{
				byteList.Add(0);
				var stringArray = line.Split(' ');
				byte[] byteArray = Array.ConvertAll(stringArray, s => byte.Parse(s));
				byteList.AddRange(byteArray);
				byteList.Add(0);
			}

			byteList.AddRange(wall);
			bytes = byteList.ToArray();

			//DebugMapMatrix();
		}

		private List<byte> CreateWalls()
		{
			List<byte> wall = new List<byte>(offset);

			for (int i = 0; i < offset; i++)
			{
				wall.Add(0);
			}

			return wall;
		}

		private void DebugMapMatrix()
		{
			int count = 0;
			string df = string.Empty;

			for (int i = 0; i < bytes.Length; i++)
			{
				count++;
				df += bytes[i].ToString();

				if (count == offset)
				{
					count = 0;
					df += "\n";
				}
			}

			Debug.Log(df);
		}
	}
}