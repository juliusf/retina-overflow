using System;
using OpenTK;
using OpenTK.Input;
using RetinaOverflow;
using RetinaOverflow.src.drawing;

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
            this.renderer = new Renderer();
        }

        public static GlobalManager instance
        {
            get
            { 
                if (theInstance == null)
                {
                    theInstance = new GlobalManager();
                }
                return theInstance;
            }
            set{ }
        }

        public Logging logging
        {
            get;
        }

        public Renderer renderer
        {
            get;
        }


    }


}

