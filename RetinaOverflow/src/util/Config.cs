using System;
using System.Runtime.ConstrainedExecution;
using System.IO;
using System.Reflection;

namespace RetinaOverflow
{
    public abstract class Config
    {
        private static String m_executionFolder;
        public static String m_assetFolder;
       
        public static String executionFolder
        {
            get
            { 
                if (m_executionFolder == null)
                {
                    m_executionFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                }
                return m_executionFolder;
            }
            set{ }
        }
        public static String assetFolder
        {
            get
            { 
                if (m_assetFolder == null)
                {
                    m_assetFolder = Path.Combine(executionFolder, "assets");
                }
                return m_assetFolder;
            }    
        }

    }
}

