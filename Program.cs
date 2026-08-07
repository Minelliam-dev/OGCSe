using System.Numerics;
using Engine;

//640 x 360 will be the final resolution

try
{
    Window window = new Window(640, 360, "test window");
    
    
    window.TexturePaths[0] = "./assets/bomb.png";
    window.TexturePaths[1] = "./assets/texture.png";
    window.TexturePaths[2] = "./assets/Button-texture.png";
    window.TexturePaths[10] = "./assets/Font.png";
    
    window.DisplayTexture(new System.Numerics.Vector2(0, 0), new System.Numerics.Vector2(100, 100), 0);
    window.DisplayTexture(new System.Numerics.Vector2(100, 0), new System.Numerics.Vector2(100, 100), 1);
    window.DisplayTexture(new System.Numerics.Vector2(200, 0), new System.Numerics.Vector2(100, 100), 14);

    window.CreateButton(new Vector2(0, 150), new Vector2(150, 50), 2, Debug.Test);

    window.RenderText("Text´", new Vector2(40, 50), new Vector2(0, 250), window);
    

    window.ConvertTrianglesToVertices();
    using (window)
    {

        window.Run();
    }
}
catch (Exception exception)
{
    Console.WriteLine(exception);
}
