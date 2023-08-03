using System;
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Threading;

public class OUTKAT
{
	private static int currentLine = 3; // Start from line 3 as specified in your format
	private static bool shouldStop = false;

	public static void ExecuteScript (string filePath)
	{
		string[] scriptLines = File.ReadAllLines(filePath);

		string description = scriptLines[0];
		float version = float.Parse(scriptLines[1]);
		string interpreterVersion = scriptLines[2];

		while (currentLine < scriptLines.Length && !shouldStop)
		{
			string line = scriptLines[currentLine].Trim();
			currentLine++;

			if (!string.IsNullOrEmpty(line))
			{
				string[] parts = line.Split(' ');
				string command = parts[0];
				string[] kArgs = new string[parts.Length - 1];
				Array.Copy(parts, 1, kArgs, 0, kArgs.Length);

				ExecuteCommand(command, kArgs, scriptLines);
			}
		}
	}

	public static void StopExecution ()
	{
		shouldStop = true;
	}

	private static void ExecuteCommand (string command, string[] kArgs, string[] scriptLines)
	{
		switch (command)
		{
			case "OUT":
				Console.WriteLine(string.Join(" ", kArgs));
				break;

			case "DWN":
				DownloadFile(kArgs[0], kArgs[1]);
				break;

			case "MOV":
				MoveFile(kArgs[0], kArgs[1]);
				break;

			case "GTO":
				int lineToGo = int.Parse(kArgs[0]);
				if (lineToGo >= 0 && lineToGo < scriptLines.Length)
				{
					currentLine = lineToGo - 1;
				}
				break;

			case "STP":
				StopExecution();
				break;

			// Implement other commands here...

			case "WIT":
				int milliseconds = int.Parse(kArgs[0]);
				Thread.Sleep(milliseconds);
				break;

			case "KAT":
				// This is a comment line, do nothing.
				break;

			default:
				Console.WriteLine("Unknown command: " + command);
				break;
		}
	}

	private static void DownloadFile (string url, string destination)
	{
		using (WebClient client = new())
		{
			client.DownloadFile(url, destination);
		}
	}

	private static void MoveFile (string source, string destination)
	{
		File.Move(source, destination);
	}

	// Implement other methods for different commands...

}