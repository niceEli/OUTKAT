using System;

class Program
{
	static void Main (string[] args)
	{
		// use dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true
		OUTKAT.ExecuteScript(args[0]);
	}
}
