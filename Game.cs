using System.Numerics;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class TrianglePoint
{
    public Vector3 Pos;
    public float TextureID = 0;
    public Vector2 UV;
}

public class Helpers
{
    static public Vector3 PixelToGLPos(Vector3 pos, int width, int height)
        {
            return new Vector3(
                (pos.X / width) * 2f - 1f,
                1f - (pos.Y / height) * 2f,
                pos.Z
            );
        } 

    public static Vector3 GLPosToPixel(Vector3 pos, int width, int height)
    {
        return new Vector3(
            ((pos.X + 1f) * 0.5f) * width,
            ((1f - pos.Y) * 0.5f) * height,
            pos.Z
        );
    }
}

public class TextRendering
{
    static Vector4 GetUV(int index)
    {
        int rows = 25;
        int columns = 32;
        
        int x = index % columns;
        int y = index / columns;

        float cellWidth = 1f / columns;
        float cellHeight = 1f / rows;

        float uMin = x * cellWidth;
        float vMin = y * cellHeight;

        float uMax = uMin + cellWidth;
        float vMax = vMin + cellHeight;

        return new Vector4(uMin, vMin, uMax, vMax);
    }
    static int GetIndexFromString(string input)
    {
        if (input == " ") return 0;
        
        else if (input == "A") return 33;
        else if (input == "B") return 34;
        else if (input == "C") return 35;
        else if (input == "D") return 36;
        else if (input == "E") return 37;
        else if (input == "F") return 38;
        else if (input == "G") return 39;
        else if (input == "H") return 40;
        else if (input == "I") return 41;
        else if (input == "J") return 42;
        else if (input == "K") return 43;
        else if (input == "L") return 44;
        else if (input == "M") return 45;
        else if (input == "N") return 46;
        else if (input == "O") return 47;
        else if (input == "P") return 48;
        else if (input == "Q") return 49;
        else if (input == "R") return 50;
        else if (input == "S") return 51;
        else if (input == "T") return 52;
        else if (input == "U") return 53;
        else if (input == "V") return 54;
        else if (input == "W") return 55;
        else if (input == "X") return 56;
        else if (input == "Y") return 57;
        else if (input == "Z") return 58;

        else if (input == "A".ToLower()) return 33 + 32;
        else if (input == "B".ToLower()) return 34 + 32;
        else if (input == "C".ToLower()) return 35 + 32;
        else if (input == "D".ToLower()) return 36 + 32;
        else if (input == "E".ToLower()) return 37 + 32;
        else if (input == "F".ToLower()) return 38 + 32;
        else if (input == "G".ToLower()) return 39 + 32;
        else if (input == "H".ToLower()) return 40 + 32;
        else if (input == "I".ToLower()) return 41 + 32;
        else if (input == "J".ToLower()) return 42 + 32;
        else if (input == "K".ToLower()) return 43 + 32;
        else if (input == "L".ToLower()) return 44 + 32;
        else if (input == "M".ToLower()) return 45 + 32;
        else if (input == "N".ToLower()) return 46 + 32;
        else if (input == "O".ToLower()) return 47 + 32;
        else if (input == "P".ToLower()) return 48 + 32;
        else if (input == "Q".ToLower()) return 49 + 32;
        else if (input == "R".ToLower()) return 50 + 32;
        else if (input == "S".ToLower()) return 51 + 32;
        else if (input == "T".ToLower()) return 52 + 32;
        else if (input == "U".ToLower()) return 53 + 32;
        else if (input == "V".ToLower()) return 54 + 32;
        else if (input == "W".ToLower()) return 55 + 32;
        else if (input == "X".ToLower()) return 56 + 32;
        else if (input == "Y".ToLower()) return 57 + 32;
        else if (input == "Z".ToLower()) return 58 + 32;

        else if (input == "´") return (10*32)+10;


        return 32;
    }
    
    public static void DisplayText(string Text, Vector2 scale, Vector2 Position, Engine.Window window)
    {
        for (int i=0; i<Text.Length; i++)
        {   
            Vector2 UVMin = new Vector2(GetUV(GetIndexFromString(Text.ElementAt(i).ToString())).X, GetUV(GetIndexFromString(Text.ElementAt(i).ToString())).Y);
            Vector2 UVMax = new Vector2(GetUV(GetIndexFromString(Text.ElementAt(i).ToString())).Z, GetUV(GetIndexFromString(Text.ElementAt(i).ToString())).W);

            Vector2 position = new Vector2(Position.X+((i*scale.X)-(10*i)), Position.Y);

            window.DisplayTextureUV(10, position, scale, UVMin, UVMax, 0);
        }
    }
}

public class Error
{
    static public void NoButtonFunctionException()
    {
        Console.WriteLine("You forgor to set the onpress function, :D");
    }
}

public class GUIButton
{
    public Vector2 A;
    public Vector2 B;
    public Action OnPress = Error.NoButtonFunctionException;
}

public class Debug
{
    static public void Log(string Output)
    {
        Console.WriteLine(Output);
    }

    static public void Test()
    {
        Debug.Log("Test passed");
    }
}

namespace Engine
{
    
    public class Window : GameWindow
    {
        public int VertexBufferObject;
        public TrianglePoint[] Triangles = new TrianglePoint[600];
        public float[] vertices = {};

        public int Handle;
        public int VertexArrayObject;

        public Vector2 WindowSize = new Vector2(0, 0);

        private int frameCount;
        private double fpsTimer;

        public Texture Image = new Texture();

        public double FPS { get; private set; }
        public int TexturesLoaded = 0;

        public string[] TexturePaths = new string[14];

        public Vector2 PlayerPos = new Vector2(0f, 0f);


        public GUIButton[] Buttons = new GUIButton[1];


        public void RenderText(string Text, Vector2 scale, Vector2 Position, Window window)
        {
            TextRendering.DisplayText(Text, scale, Position, window);
        }
        public void CreateButton(Vector2 Position, Vector2 Size, int TextureID, Action OnPress)
        {
            DisplayTexture(Position, Size, TextureID);

            GUIButton button = new GUIButton();
            button.A = Position;
            button.B = Position + Size;
            
            if (OnPress != null) button.OnPress = OnPress;
            
            Buttons[0] = button;
        }
        public void CheckButtons()
        {

            bool IsOnButton = false;
            int SelectedButton = 0;

            Vector2 MousePos = new Vector2(MousePosition.X, MousePosition.Y);

            for (int i=0; i < Buttons.Length; i++)
            {
                if (MousePos.X > Buttons[i].A.X && MousePos.X < Buttons[i].B.X && MousePos.Y < Buttons[i].B.Y && MousePos.Y > Buttons[i].A.Y)
                {
                    IsOnButton = true;
                    SelectedButton = i;
                }
            }

            if (IsOnButton && IsMouseButtonPressed(OpenTK.Windowing.GraphicsLibraryFramework.MouseButton.Button1))
            {
                Buttons[SelectedButton].OnPress();
            }
        }
        public void InputChecks()
        {
            if (KeyboardState.IsKeyPressed(Keys.F11))
            {
                if (WindowState == WindowState.Fullscreen)
                    WindowState = WindowState.Normal;
                else
                    WindowState = WindowState.Fullscreen;
            }

            if (IsKeyPressed(Keys.Delete))
            {
                Close();
            }
        }
        public void DisplayTextureUV(int TextureIndex, Vector2 Position, Vector2 Size, Vector2 uvMin, Vector2 uvMax, float Z = 0)
        {
            TrianglePoint triangle1 = new TrianglePoint();
            triangle1.Pos = new Vector3(Position.X, Position.Y, Z);
            triangle1.UV = new Vector2(uvMin.X, uvMin.Y);
            triangle1.TextureID = TextureIndex;

            TrianglePoint triangle2 = new TrianglePoint();
            triangle2.Pos = new Vector3(Position.X + Size.X, Position.Y, Z);
            triangle2.UV = new Vector2(uvMax.X, uvMin.Y);
            triangle2.TextureID = TextureIndex;

            TrianglePoint triangle3 = new TrianglePoint();
            triangle3.Pos = new Vector3(Position.X + Size.X, Position.Y + Size.Y, Z);
            triangle3.UV = new Vector2(uvMax.X, uvMax.Y);
            triangle3.TextureID = TextureIndex;

            TrianglePoint triangle4 = new TrianglePoint();
            triangle4.Pos = new Vector3(Position.X, Position.Y, Z);
            triangle4.UV = new Vector2(uvMin.X, uvMin.Y);
            triangle4.TextureID = TextureIndex;

            TrianglePoint triangle5 = new TrianglePoint();
            triangle5.Pos = new Vector3(Position.X, Position.Y + Size.Y, Z);
            triangle5.UV = new Vector2(uvMin.X, uvMax.Y);
            triangle5.TextureID = TextureIndex;

            TrianglePoint triangle6 = new TrianglePoint();
            triangle6.Pos = new Vector3(Position.X + Size.X, Position.Y + Size.Y, Z);
            triangle6.UV = new Vector2(uvMax.X, uvMax.Y);
            triangle6.TextureID = TextureIndex;

            try
            {
                Triangles[TexturesLoaded * 6 + 0] = triangle1;
                Triangles[TexturesLoaded * 6 + 1] = triangle2;
                Triangles[TexturesLoaded * 6 + 2] = triangle3;
                Triangles[TexturesLoaded * 6 + 3] = triangle4;
                Triangles[TexturesLoaded * 6 + 4] = triangle5;
                Triangles[TexturesLoaded * 6 + 5] = triangle6;
            }
            catch
            {
                return;
            }

            TexturesLoaded++;
        }
        public Window(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { ClientSize = (width, height), Title = title, APIVersion = new Version(3, 1), Profile = ContextProfile.Any})
        {
            WindowSize = new Vector2(width, height);
        }
        public void ChangeCurrentTexture(string path, float index)
        {
            DoTextureStuff(path, index);
        }
        public void DisplayTexture(Vector2 Position, Vector2 Size, float TextureIndex, float Z = 0)
        {   
            //triangle 1
            TrianglePoint triangle1 = new TrianglePoint();
            triangle1.Pos = new System.Numerics.Vector3(0 + Position.X, 0 + Position.Y, Z); //top left
            triangle1.UV = new System.Numerics.Vector2(0, 0);
            triangle1.TextureID = TextureIndex;

            TrianglePoint triangle2 = new TrianglePoint();
            triangle2.Pos = new System.Numerics.Vector3(Size.X + Position.X, 0 + Position.Y, Z); //top right
            triangle2.UV = new System.Numerics.Vector2(1, 0);
            triangle2.TextureID = TextureIndex;

            TrianglePoint triangle3 = new TrianglePoint();
            triangle3.Pos = new System.Numerics.Vector3(Size.X + Position.X, Size.Y + Position.Y, Z); //bottom right
            triangle3.UV = new System.Numerics.Vector2(1f, 1);
            triangle3.TextureID = TextureIndex;

            //triangle 2
            TrianglePoint triangle4 = new TrianglePoint();
            triangle4.Pos = new System.Numerics.Vector3(0 + Position.X, 0 + Position.Y, Z); //top left
            triangle4.UV = new System.Numerics.Vector2(0, 0);
            triangle4.TextureID = TextureIndex;

            TrianglePoint triangle5 = new TrianglePoint();
            triangle5.Pos = new System.Numerics.Vector3(0 + Position.X, Size.Y + Position.Y, Z); //bottom left
            triangle5.UV = new System.Numerics.Vector2(0, 1);
            triangle5.TextureID = TextureIndex;

            TrianglePoint triangle6 = new TrianglePoint();
            triangle6.Pos = new System.Numerics.Vector3(Size.X + Position.X, Size.Y + Position.Y, Z); //bottom right
            triangle6.UV = new System.Numerics.Vector2(1f, 1);
            triangle6.TextureID = TextureIndex;

            try
            {
                Triangles[0 + (TexturesLoaded*6)] = triangle1;
                Triangles[1 + (TexturesLoaded*6)] = triangle2;
                Triangles[2 + (TexturesLoaded*6)] = triangle3;
                Triangles[3 + (TexturesLoaded*6)] = triangle4;
                Triangles[4 + (TexturesLoaded*6)] = triangle5;
                Triangles[5 + (TexturesLoaded*6)] = triangle6;
            }
            catch
            {
                return;
            }

            TexturesLoaded++;
        }
        public void ConvertTrianglesToVertices()
        {
            float[] finalVertices = new float[Triangles.Length * 6];

            for (int i = 0; i < Triangles.Length; i++)
            {
                if (Triangles[i] == null) break;
                
                Vector3 position = Helpers.PixelToGLPos(Triangles[i].Pos, (int)WindowSize.X, (int)WindowSize.Y);
                Vector2 uv = Triangles[i].UV;

                finalVertices[(i * 6) + 0] = position.X;
                finalVertices[(i * 6) + 1] = position.Y;
                finalVertices[(i * 6) + 2] = position.Z;

                finalVertices[(i * 6) + 3] = uv.X;
                finalVertices[(i * 6) + 4] = uv.Y;

                finalVertices[(i * 6) + 5] = Triangles[i].TextureID;
            }

            vertices = finalVertices;
        }
        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            Handle = LoadShaders();

            GL.UseProgram(Handle);

            GL.Uniform1(GL.GetUniformLocation(Handle, "texture0"), 0);
            GL.Uniform1(GL.GetUniformLocation(Handle, "texture1"), 1);
            GL.Uniform1(GL.GetUniformLocation(Handle, "texture2"), 2);
            GL.Uniform1(GL.GetUniformLocation(Handle, "texture3"), 3);
            GL.Uniform1(GL.GetUniformLocation(Handle, "texture4"), 4);
            GL.Uniform1(GL.GetUniformLocation(Handle, "texture5"), 5);
            GL.Uniform1(GL.GetUniformLocation(Handle, "texture6"), 6);
            GL.Uniform1(GL.GetUniformLocation(Handle, "texture7"), 7);
            GL.Uniform1(GL.GetUniformLocation(Handle, "texture8"), 8);
            GL.Uniform1(GL.GetUniformLocation(Handle, "texture9"), 9);
            GL.Uniform1(GL.GetUniformLocation(Handle, "texture10"), 10);
            GL.Uniform1(GL.GetUniformLocation(Handle, "texture11"), 11);
            GL.Uniform1(GL.GetUniformLocation(Handle, "texture12"), 12);
            GL.Uniform1(GL.GetUniformLocation(Handle, "texture13"), 13);

            Image.StartImageStuff();

            for (int i=0; i<6; i++)
            {
                if (TexturePaths[i] == null) TexturePaths[i] = "./assets/Prototype.png";
                
                
                ChangeCurrentTexture(TexturePaths[i], i);
            }

            // Create and bind the VAO.
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            // Create and bind the VBO.
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.BufferData(
                BufferTarget.ArrayBuffer,
                vertices.Length * sizeof(float),
                vertices,
                BufferUsageHint.DynamicDraw
            );

            // Tell OpenGL how each vertex is structured.
            
            GL.VertexAttribPointer(
                index: 0,
                size: 3,
                type: VertexAttribPointerType.Float,
                normalized: false,
                stride: 6 * sizeof(float),
                offset: 0
            );

            GL.VertexAttribPointer(
                index: 1,
                size: 2,
                type: VertexAttribPointerType.Float,
                normalized: false,
                stride: 6 * sizeof(float),
                offset: 3 * sizeof(float)
            );

            GL.VertexAttribPointer(
                index: 2,
                size: 1,
                type: VertexAttribPointerType.Float,
                normalized: false,
                stride: 6 * sizeof(float),
                offset: 5 * sizeof(float)
            );

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);

            // Optional cleanup of current bindings.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);

            GL.Enable(EnableCap.Blend);

            GL.BlendFunc(
                BlendingFactor.SrcAlpha,
                BlendingFactor.OneMinusSrcAlpha
            );
        }
        protected override void OnUnload()
        {
            base.OnUnload();
        }
        int LoadShaders()
        {
            
            //create the shaders
            int VertexShader;
            int FragmentShader;
            
            string VertexShaderSource = File.ReadAllText("shader.vert");
            string FragmentShaderSource = File.ReadAllText("shader.frag");

            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);

            
            //Compile the shaders
            int successVertex;
            int successFragment;
            
            GL.CompileShader(VertexShader);

            GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out successVertex);
            if (successVertex == 0)
            {
                string infoLog = GL.GetShaderInfoLog(VertexShader);
                Console.WriteLine(infoLog);
            }

            GL.CompileShader(FragmentShader);

            GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out successFragment);
            if (successFragment == 0)
            {
                string infoLog = GL.GetShaderInfoLog(FragmentShader);
                Console.WriteLine(infoLog);
            }

            
            //link the shaders
            int Handle;
            
            Handle = GL.CreateProgram(); 

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            GL.BindAttribLocation(Handle, 0, "aPosition");
            GL.BindAttribLocation(Handle, 1, "aTexCoord");
            GL.BindAttribLocation(Handle, 2, "aTexture");

            GL.LinkProgram(Handle);

            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetProgramInfoLog(Handle);
                Console.WriteLine(infoLog);
            }

            //Cleanup
            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);

            return Handle;

        }
        void DoTextureStuff(string path, float Slot = 0)
        {
            Image.Use(path, Slot);

            //Repeat: The default behavior for textures. Repeats the texture image.
            //MirroredRepeat: Same as GL_REPEAT but mirrors the image with each repeat.
            //ClampToEdge: Clamps the coordinates between 0 and 1. The result is that higher coordinates become clamped to the edge, resulting in a stretched edge pattern.
            //ClampToBorder: Coordinates outside the range are now given a user-specified border color.

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            //set the filter to nearest
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            //Mipmap stuff
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            //Code goes here.
            GL.UseProgram(Handle);

            GL.BindVertexArray(VertexArrayObject);
            
            GL.DrawArrays(
                PrimitiveType.Triangles,
                first: 0,
                count: vertices.Length/6
            );

            CheckButtons();

            InputChecks();

            SwapBuffers();

            frameCount++;
            fpsTimer += e.Time;

            if (fpsTimer >= 1.0)
            {
                FPS = frameCount / fpsTimer;

                Title = $"OpenGL Window | FPS: {FPS:F0}";

                frameCount = 0;
                fpsTimer = 0.0;
            }
        }
        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);

            //GL.Viewport(0, 0, e.Width, e.Height);
            WindowSize = new Vector2(e.Width, e.Height);
        }
    }
}