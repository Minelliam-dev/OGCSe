using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common.Input;
using StbImageSharp;

public class Texture
{
    public int[] Handle = new int[7];

    public void StartImageStuff()
    {
        for (int i=0; i<Handle.Length; i++)
        {
            Handle[i] = GL.GenTexture();
        }
        
    }
    ImageResult Load(string Path)
    {
        //StbImage.stbi_set_flip_vertically_on_load(0);

        ImageResult image = ImageResult.FromStream(File.OpenRead(Path), ColorComponents.RedGreenBlueAlpha);

        return image;
    }

    public void Use(string Path, float index)
    {
        ImageResult image = Load(Path);

        GL.ActiveTexture(TextureUnit.Texture0 + (int)index);

        GL.BindTexture(TextureTarget.Texture2D, Handle[(int)index]);

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, image.Data);
    }
}