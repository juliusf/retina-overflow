using System;

namespace RetinaOverflow
{
    public class RetinaException : Exception
    {
        public RetinaException(){
            GlobalManager.instance.logging.error(GetType().ToString());
        }
        public RetinaException(string message) : base(message){
            GlobalManager.instance.logging.error(GetType() + ": " + message);
        }
    }

   
}

