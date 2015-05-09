using System;
using OpenTK;
using OpenTK.Input;
using RetinaOverflow;

namespace RetinaOverflow
{
	public class GlobalManager
	{	
		private static GlobalManager theInstance;
		private GameWindow game;

		private GlobalManager()
		{
			this.game = new GameWindow();
			this.logging = new Logging();
		}
			
		public static GlobalManager instance {
			get{ 
				if (theInstance == null) 
				{
					theInstance = new GlobalManager();
				}
				return theInstance;}
			set{ }
		}

		public Logging logging {
			get;
			set;
		}


	}


}

