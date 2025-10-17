namespace Game1
{

    public abstract class LevelTemp
    {

        public static Level levelReturn()
        {
            Level levelStore = new();
            // --- Level ---
            //Level.ImportLevelFromFile("Levels/demo.lvl", out levelStore);

            // room 1 decal
            levelStore.levelObjects.Add(new Button((0.75f, 0, 0.99f), (0f, 0, -1.0f), 0.25f, 0.25f, 7, 8, 0.8f, HeldItem.Colors.Red));

            // room 1
            levelStore.levelObjects.Add(new Cube((0, 0, -1.25f), 2.0f, 1.0f, 0.5f, 4)); // back wall
            levelStore.levelObjects.Add(new Item((0, -0.25f, -0.75f), (0.25f, 0.25f), 3, 0, false, Item.ItemsEnum.RedKeycard));

            // side walls
            levelStore.levelObjects.Add(new Cube((1.5f, 0f, 1.125f), 2.0f, 1.0f, 0.25f, 0));
            levelStore.levelObjects.Add(new Cube((-1.5f, 0f, 1.125f), 2.0f, 1.0f, 0.25f, 0));

            // exit walls
            levelStore.levelObjects.Add(new Cube((-1.25f, 0, 0), 0.5f, 1.0f, 2.0f, 0));
            levelStore.levelObjects.Add(new Cube((1.25f, 0, 0), 0.5f, 1.0f, 2.0f, 0));

            levelStore.levelObjects.Add(new Cube((0, -0.75f, 0), 3.0f, 0.5f, 3.0f, 1));
            levelStore.levelObjects.Add(new Cube((0, 0.75f, 0), 2.5f, 0.5f, 2.5f, 3));

            levelStore.levelObjects.Add(new Door((0, 0, 1.25f), 1.0f, 1.0f, 0.25f, 9, HeldItem.Colors.Red, false));

            // outside room 1

            levelStore.levelObjects.Add(new Item((0, -0.25f, 1.5f), (0.25f, 0.25f), 2, 0, false, Item.ItemsEnum.LargeMedkit));

            // entrance walls
            levelStore.levelObjects.Add(new Cube((1.5f, 0f, 1.375f), 2.0f, 1.0f, 0.25f, 5));
            levelStore.levelObjects.Add(new Cube((-1.5f, 0f, 1.375f), 2.0f, 1.0f, 0.25f, 5));
            levelStore.levelObjects.Add(new Cube((1f, 1f, 1.375f), 2.0f, 1.0f, 0.25f, 5));
            levelStore.levelObjects.Add(new Cube((-1f, 1f, 1.375f), 2.0f, 1.0f, 0.25f, 5));

            // side walls
            levelStore.levelObjects.Add(new Cube((-2.25f, 1f, 2.5f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2.25f, 1f, 4.5f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2.25f, 0f, 2.5f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2.25f, 0f, 4.5f), 0.5f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((2.25f, 1f, 2.5f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2.25f, 1f, 4.5f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2.25f, 0f, 2.5f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2.25f, 0f, 4.5f), 0.5f, 1.0f, 2.0f, 5));

            // exit walls
            levelStore.levelObjects.Add(new Cube((1.25f, 0f, 5.75f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-1.25f, 0f, 5.75f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((1f, 1f, 5.75f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-1f, 1f, 5.75f), 2.0f, 1.0f, 0.5f, 5));

            //floor
            levelStore.levelObjects.Add(new Cube((1, -0.75f, 2.5f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-1, -0.75f, 2.5f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((1, -0.75f, 4.5f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-1, -0.75f, 4.5f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((0, -0.75f, 5.75f), 2.0f, 0.5f, 0.5f, 6));

            // entities
            levelStore.levelObjects.Add(new Enemy((1, -0.2f, 4.5f), (0.4f, 0.6f), 1, 0, 100, true));
            levelStore.levelObjects.Add(new Enemy((-1, -0.2f, 4.5f), (0.4f, 0.6f), 1, 0, 100, true));

            // corridor 1

            //floor
            levelStore.levelObjects.Add(new Cube((0, -0.75f, 7f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((0, -0.75f, 9f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((0, -0.75f, 11f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((2, -0.75f, 11f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((0, -0.75f, 13f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((0, -0.75f, 15f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-2, -0.75f, 15f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((0, -0.75f, 17f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((2, -0.75f, 17f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((4, -0.75f, 17f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((3, -0.75f, 18.25f), 2f, 0.5f, 0.5f, 6));

            // right wall
            levelStore.levelObjects.Add(new Cube((-1.25f, 1f, 7f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-1.25f, 0f, 7f), 0.5f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((-1.25f, 1f, 9f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-1.25f, 0f, 9f), 0.5f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((-1.25f, 1f, 11f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-1.25f, 0f, 11f), 0.5f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((-2f, 1f, 13f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2f, 0f, 13f), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((-2f, 1f, 17f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2f, 0f, 17f), 2.0f, 1.0f, 2.0f, 5));

            // left wall
            levelStore.levelObjects.Add(new Cube((1.25f, 1f, 7f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((1.25f, 0f, 7f), 0.5f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((2f, 1f, 9f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2f, 0f, 9f), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((2f, 1f, 13f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2f, 0f, 13f), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((2f, 1f, 15f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2f, 0f, 15f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((4f, 1f, 15f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((4f, 0f, 15f), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((5.25f, 1f, 17f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((5.25f, 0f, 17f), 0.5f, 1.0f, 2.0f, 5));

            // back wall
            levelStore.levelObjects.Add(new Cube((0f, 1f, 18.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((0f, 0f, 18.25f), 2.0f, 1.0f, 0.5f, 5));

            // exit walls
            levelStore.levelObjects.Add(new Cube((2.0f, 0f, 18.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((5.0f, 0f, 18.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((2f, 1f, 18.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((4.0f, 1f, 18.25f), 2.0f, 1.0f, 0.5f, 5));

            levelStore.levelObjects.Add(new Door((3.5f, 0, 18.25f), 1.0f, 1.0f, 0.25f, 9, HeldItem.Colors.Blue, false));
            levelStore.levelObjects.Add(new Item((3.5f, -0.25f, 17.5f), (0.25f, 0.25f), 4, 0, false, Item.ItemsEnum.BlueKeycard));
            levelStore.levelObjects.Add(new Button((4.25f, 0, 17.99f), (0, 0, -1.0f), 0.25f, 0.25f, 7, 8, 0.8f, HeldItem.Colors.Blue));

            // exit room
            levelStore.levelObjects.Add(new Cube((2, -0.75f, 19.5f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((4, -0.75f, 19.5f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((2, -0.75f, 21.5f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((4, -0.75f, 21.5f), 2.0f, 0.5f, 2.0f, 6));


            levelStore.levelObjects.Add(new Cube((5.25f, 1f, 19.5f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((5.25f, 0f, 19.5f), 0.5f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((5.25f, 1f, 21.5f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((5.25f, 0f, 21.5f), 0.5f, 1.0f, 2.0f, 5));


            levelStore.levelObjects.Add(new Cube((0.75f, 1f, 19.5f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((0.75f, 0f, 19.5f), 0.5f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((0.75f, 1f, 21.5f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((0.75f, 0f, 21.5f), 0.5f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((2f, 1f, 22.75f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((2f, 0f, 22.75f), 2.0f, 1.0f, 0.5f, 5));

            levelStore.levelObjects.Add(new Cube((4f, 1f, 22.75f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((4f, 0f, 22.75f), 2.0f, 1.0f, 0.5f, 5));

            levelStore.Sync_GL_Level();

            levelStore.ExportToFile("Levels/level1.lvl");

            return levelStore;
        }

    }

    class unusedCode {

        private readonly float[] HUD_Vertices = [
            // health indicator
            -0.875f,-0.875f,0f,0f,0.75f, 1.0f,
            -0.875f,-0.625f,0f,0f,1f, 1.0f,
            -0.625f,-0.625f,0f,0.25f,1f, 1.0f,

            -0.875f,-0.875f,0f,0f,0.75f, 1.0f,
            -0.625f,-0.625f,0f,0.25f,1f, 1.0f,
            -0.625f,-0.875f,0f,0.25f,0.75f, 1.0f,

            // crosshair
            -0.00390625f,-0.03125f,0f,0.25f,0.75f, 1.0f,
            -0.00390625f,0.03125f,0f,0.25f,1f, 1.0f,
            0.00390625f,0.03125f,0f,0.5f,1f, 1.0f,

            -0.00390625f,-0.03125f,0f,0.25f,0.75f, 1.0f,
            0.00390625f,0.03125f,0f,0.5f,1f, 1.0f,
            0.00390625f,-0.03125f,0f,0.5f,0.75f, 1.0f,

            -0.03125f,-0.00390625f,0f,0.25f,0.75f,1.0f,
            -0.03125f,0.00390625f,0f,0.25f,1f,1.0f,
            0.03125f,0.00390625f,0f,0.5f,1f,1.0f,

            -0.03125f,-0.00390625f,0f,0.25f,0.75f,1.0f,
            0.03125f,0.00390625f,0f,0.5f,1f,1.0f,
            0.03125f,-0.00390625f,0f,0.5f,0.75f,1.0f,
        ];

    }

}