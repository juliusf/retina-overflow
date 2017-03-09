using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace RetinaOverflow
{
    public class Material
    {
        public String name;
        public String diffuseTexturePath;

        private static int fallbackID = -1;
        public int diffuseTextureID
        {
            get{return m_diffuseTexId;}
        }
        private int m_diffuseTexId;

        public Material()
        {
        }


        public void initialize()
        {
            loadFallbackTexture();
            loadTextures();
        }


        private void loadFallbackTexture()
        {
            if (fallbackID == -1)
            {
                var fullDiffuseTexturePath = Path.Combine(Config.textureFolder, "fallback.png");
                fallbackID = loadTexture(fullDiffuseTexturePath);
            }
        }
        private void loadTextures()
        {
            try
            {
                GlobalManager.instance.logging.info(String.Format("loading texture: {0}", diffuseTexturePath));
                var fullDiffuseTexturePath = Path.Combine(Config.textureFolder, diffuseTexturePath);
                m_diffuseTexId = loadTexture(fullDiffuseTexturePath);

            } 
            catch (System.ArgumentNullException)
            {
                GlobalManager.instance.logging.warning(String.Format("texture {0} not found! using fallback", diffuseTexturePath));
                m_diffuseTexId = fallbackID;
            }
        }

        private int loadTexture(String fullPath)
        {
            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Bitmap bmp = new Bitmap(fullPath);
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            bmp.UnlockBits(bmp_data);

            return id;
        }
    } 
}

