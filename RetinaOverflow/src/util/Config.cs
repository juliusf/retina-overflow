using System;
using System.Runtime.ConstrainedExecution;
using System.IO;
using System.Reflection;

namespace RetinaOverflow
{
    public abstract class Config
    {
        private static String m_executionFolder;
        private static String m_assetFolder;
        private static String m_textureFolder;
       
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

        public static String textureFolder
        {
            get
            { 
                if (m_textureFolder == null)
                {
                    m_textureFolder = Path.Combine(assetFolder, "textures");
                }
                return m_textureFolder;
            }    
        }

    }
}

