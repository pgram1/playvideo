using System;
using System.Diagnostics;

class playvideo {

	static void Main() {

		do {
			Console.WriteLine("What is the address of the stream's file/playlist? Link/URL:");
			string Url = Console.ReadLine();
			Console.WriteLine("Alright!\nLoading format list...");
			runYTDL(Url);
			Console.WriteLine("What quality?(video+audio or single stream):");
			string Qual = Console.ReadLine();
			do {
				runMPV(Qual, Url);
				Console.WriteLine("Input e to play something else, r to replay the stream or any other character to terminate:");
				string Cond = Console.ReadLine();

				if (Cond == "e") {
					break;
				} else if (Cond == "r") {
					continue;
				} else {
					Environment.Exit(0);
				}

			} while ( true );

		} while ( true );
	}

	static void runMPV(string Qual, string Url) {
		//* Create your Process
		Process process = new Process();
		process.StartInfo.FileName = "mpv";
		process.StartInfo.Arguments = "--force-window=immediate --ytdl-format=\"" + Qual + "\" \"" + Url + "\"";
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.RedirectStandardError = true;
		//* Set your output and error (asynchronous) handlers
		process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
		process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
		//* Start process and handlers
		process.Start();
		process.BeginOutputReadLine();
		process.BeginErrorReadLine();
		process.WaitForExit();
	}

	static void runYTDL(string Url) {
		//* Create your Process
		Process process = new Process();
		process.StartInfo.FileName = "youtube-dl";
		process.StartInfo.Arguments = "-F \"" + Url + "\"";
		process.StartInfo.UseShellExecute = false;
		process.StartInfo.RedirectStandardOutput = true;
		process.StartInfo.RedirectStandardError = true;
		//* Set your output and error (asynchronous) handlers
		process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
		process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
		//* Start process and handlers
		process.Start();
		process.BeginOutputReadLine();
		process.BeginErrorReadLine();
		process.WaitForExit();
	}

	static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine) {
		Console.WriteLine(outLine.Data);
	}
}