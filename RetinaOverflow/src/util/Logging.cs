using System;
using System.Threading;
using System.Collections.Concurrent;
using OpenTK.Graphics.ES11;

namespace RetinaOverflow
{
    public struct LogEntry
	{
		public ConsoleColor color;
		public String text;
	}

	public enum LogLevel{Debug, Info, Warning, Error};

	public class Logging
	{
		private static BlockingCollection<LogEntry> queue = new BlockingCollection<LogEntry>();
		public LogLevel logLevel 
		{
			get;
			set;
		}
		public Logging()
		{	
			this.logLevel = LogLevel.Info;
			var thread = new Thread(
				() =>
				{	
					while (true){
						var entry = queue.Take();
						Console.ForegroundColor = entry.color;
						Console.WriteLine(entry.text);
					}
				});
			thread.IsBackground = true;
			thread.Start();
		}

		public void debug(String text)
		{
			if (this.logLevel <= LogLevel.Debug) 
			{
				var entry = new LogEntry();
				entry.color = ConsoleColor.Gray;
				entry.text = "Debug: " + text;
				queue.Add(entry);
			}
		}

		public void info(String text)
		{
			if (this.logLevel <= LogLevel.Info) 
			{
				var entry = new LogEntry();
				entry.color = ConsoleColor.White;
				entry.text = "Info: " + text;
				queue.Add(entry);
			}
		}

		public void warning(String text)
		{
			if (this.logLevel <= LogLevel.Warning) 
			{
				var entry = new LogEntry();
				entry.color = ConsoleColor.Yellow;
				entry.text = "Warning: " + text;
				queue.Add(entry);
			}
		}

		public void error(String text)
		{
			if (this.logLevel <= LogLevel.Error) 
			{
				var entry = new LogEntry();
				entry.color = ConsoleColor.Red;
				entry.text = "Error: " + text;
				queue.Add(entry);
			}
		}
	}
}

