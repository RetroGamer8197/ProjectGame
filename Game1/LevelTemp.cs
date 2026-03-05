using OpenTK.Graphics.OpenGL;

namespace Game1
{

    public abstract class LevelTemp
    {

        public static Level Level1Return()
        {
            Level levelStore = new();
            // --- Level ---

            // room 1 decal
            levelStore.levelObjects.Add(new Button((0.75f, 0, 0.99f), (0f, 0, -1.0f), 0.25f, 0.25f, 7, 8, 0.8f, HeldItem.Colors.Red));
            //levelStore.levelObjects.Add(new Button((-0.75f, 0, 0.99f), (0f, 0, -1.0f), 0.25f, 0.25f, 7, 8, 0.8f, HeldItem.Colors.Green));

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

            levelStore.levelObjects.Add(new Item((0, -0.25f, 3.0f), (0.25f, 0.25f), 8, 25, false, Item.ItemsEnum.SmallAmmo));

            levelStore.levelObjects.Add(new Item((0, -0.25f, 5.0f), (0.25f, 0.25f), 12, 0, false, Item.ItemsEnum.ArmorPatch));

            levelStore.levelObjects.Add(new Item((0, -0.25f, 6.0f), (0.25f, 0.25f), 11, 0, false, Item.ItemsEnum.ArmorMetal));

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
            levelStore.levelObjects.Add(new Enemy((1, -0.1f, 4.5f), (0.6f, 0.9f), 12, 20, 100, 11, true, 5));
            levelStore.levelObjects.Add(new Enemy((-1, -0.1f, 4.5f), (0.6f, 0.9f), 12, 20, 100, 11, true, 5));

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

            // left corridor
            levelStore.levelObjects.Add(new Plane((2.28f, -0.25f, 11f), (-2, 1, 0), 0.5f, 0.5f, 10, 0.9f));
            levelStore.levelObjects.Add(new Plane((2.5f, -0.25f, 11f), (2, 1, 0), 0.5f, 0.5f, 10, 0.9f));

            levelStore.levelObjects.Add(new Cube((4, -0.75f, 11f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((6, -0.75f, 11f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((8, -0.75f, 11f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((4f, 1f, 9f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((6f, 1f, 9f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((8f, 1f, 9f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((4f, 0f, 9f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((6f, 0f, 9f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((8f, 0f, 9f), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((4f, 1f, 13f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((6f, 1f, 13f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((8f, 1f, 13f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((10f, 1f, 13f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((4f, 0f, 13f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((6f, 0f, 13f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((8f, 0f, 13f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((10f, 0f, 13f), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((10f, -0.75f, 11f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((10f, -0.75f, 9f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((10f, -0.75f, 7f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((11.25f, 1f, 7f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((11.25f, 0f, 7f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((11.25f, 1f, 9f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((11.25f, 0f, 9f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((11.25f, 1f, 11f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((11.25f, 0f, 11f), 0.5f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((8f, -0.75f, 7f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((6f, -0.75f, 7f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((4f, -0.75f, 7f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((4f, 1f, 5f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((6f, 1f, 5f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((8f, 1f, 5f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((10f, 1f, 5f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((4f, 0f, 5f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((6f, 0f, 5f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((8f, 0f, 5f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((10f, 0f, 5f), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Item((4f, -0.25f, 7f), (0.25f, 0.25f), 4, 0, false, Item.ItemsEnum.BlueKeycard));

            levelStore.levelObjects.Add(new Item((4.5f, -0.25f, 7.5f), (0.25f, 0.25f), 8, 25, false, Item.ItemsEnum.SmallAmmo));

            levelStore.levelObjects.Add(new Item((4.5f, -0.25f, 6.5f), (0.25f, 0.25f), 9, 12, false, Item.ItemsEnum.ShellPack));

            levelStore.levelObjects.Add(new Enemy((6f, -0.1f, 7f), (0.6f, 0.9f), 14, 40, 100, 7, true, 4));

            levelStore.levelObjects.Add(new Cube((2.75f, 1f, 7f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2.75f, 0f, 7f), 0.5f, 1.0f, 2.0f, 5));

            // right corridor
            levelStore.levelObjects.Add(new Cube((-2f, -0.75f, 15f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Item((-2f, -0.25f, 15f), (0.25f, 0.25f), 8, 25, false, Item.ItemsEnum.SmallAmmo));
            levelStore.levelObjects.Add(new Item((-2f, -0.25f, 15.5f), (0.25f, 0.25f), 8, 25, false, Item.ItemsEnum.SmallAmmo));
            levelStore.levelObjects.Add(new Item((-2f, -0.25f, 14.5f), (0.25f, 0.25f), 8, 25, false, Item.ItemsEnum.SmallAmmo));

            levelStore.levelObjects.Add(new Cube((-4f, -0.75f, 15f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-4f, -0.75f, 17f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-4f, -0.75f, 19f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-4f, -0.75f, 13f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-4f, -0.75f, 11f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((-6f, 1f, 13f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-6f, 1f, 15f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-6f, 1f, 17f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-6f, 0f, 13f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-6f, 0f, 15f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-6f, 0f, 17f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-8f, 1f, 13f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-8f, 1f, 15f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-8f, 1f, 17f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-8f, 0f, 13f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-8f, 0f, 15f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-8f, 0f, 17f), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((-6f, -0.75f, 19f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-6f, -0.75f, 11f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-8f, -0.75f, 19f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-8f, -0.75f, 11f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((-10f, -0.75f, 15f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-10f, -0.75f, 17f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-10f, -0.75f, 19f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-10f, -0.75f, 13f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-10f, -0.75f, 11f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((-11.25f, 1f, 13f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-11.25f, 1f, 15f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-11.25f, 1f, 17f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-11.25f, 1f, 19f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-11.25f, 1f, 11f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-11.25f, 1f, 9f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-11.25f, 1f, 7f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-11.25f, 0f, 13f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-11.25f, 0f, 15f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-11.25f, 0f, 17f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-11.25f, 0f, 19f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-11.25f, 0f, 11f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-11.25f, 0f, 9f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-11.25f, 0f, 7f), 0.5f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((-2f, 1f, 11f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2f, 0f, 11f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-4f, 1f, 9f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-4f, 0f, 9f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-6f, 1f, 9f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-6f, 0f, 9f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-8f, 1f, 9f), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-8f, 0f, 9f), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((-10f, -0.75f, 9f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-10f, -0.75f, 7f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-8f, -0.75f, 7f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((-10f, 0f, 5.75f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-8f, 0f, 5.75f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-10f, 1f, 5.75f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-8f, 1f, 5.75f), 2.0f, 1.0f, 0.5f, 5));

            levelStore.levelObjects.Add(new Cube((-10f, 0f, 20.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-8f, 0f, 20.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-6f, 0f, 20.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-4f, 0f, 20.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-10f, 1f, 20.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-8f, 1f, 20.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-6f, 1f, 20.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-4f, 1f, 20.25f), 2.0f, 1.0f, 0.5f, 5));

            levelStore.levelObjects.Add(new Cube((-2.75f, 0f, 19f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2.75f, 1f, 19f), 0.5f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((-6.75f, 0f, 7f), 0.5f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-6.75f, 1f, 7f), 0.5f, 1.0f, 2.0f, 5));
            
            levelStore.levelObjects.Add(new Button((-7.01f, 0, 7f), (-1.0f, 0, 0.0f), 0.25f, 0.25f, 7, 8, 0.8f, HeldItem.Colors.Blue));

            levelStore.levelObjects.Add(new Item((-7.5f, -0.25f, 6.5f), (0.25f, 0.25f), 2, 0, false, Item.ItemsEnum.LargeMedkit));
            levelStore.levelObjects.Add(new Item((-7.5f, -0.25f, 7.5f), (0.25f, 0.25f), 11, 0, false, Item.ItemsEnum.ArmorMetal));

            levelStore.levelObjects.Add(new Enemy((-9f, -0.1f, 7f), (0.6f, 0.9f), 14, 40, 100, 7, true, 4));
            levelStore.levelObjects.Add(new Enemy((-10f, -0.1f, 11f), (0.6f, 0.9f), 12, 20, 100, 11, true, 5));
            levelStore.levelObjects.Add(new Enemy((-10f, -0.1f, 14f), (0.6f, 0.9f), 12, 20, 100, 11, true, 5));
            levelStore.levelObjects.Add(new Enemy((-10f, -0.1f, 17f), (0.6f, 0.9f), 12, 20, 100, 11, true, 5));
            
            // back wall
            levelStore.levelObjects.Add(new Cube((0f, 1f, 18.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((0f, 0f, 18.25f), 2.0f, 1.0f, 0.5f, 5));

            // exit walls
            levelStore.levelObjects.Add(new Cube((2.0f, 0f, 18.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((5.0f, 0f, 18.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((2f, 1f, 18.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((4.0f, 1f, 18.25f), 2.0f, 1.0f, 0.5f, 5));

            levelStore.levelObjects.Add(new Door((3.5f, 0, 18.25f), 1.0f, 1.0f, 0.25f, 9, HeldItem.Colors.Blue, false));
            
            //levelStore.levelObjects.Add(new Button((4.25f, 0, 17.99f), (0, 0, -1.0f), 0.25f, 0.25f, 7, 8, 0.8f, HeldItem.Colors.Blue));

            // exit room
            levelStore.levelObjects.Add(new LevelEndButton((4.25f, 0, 18.51f), (0, 0, 1.0f), 0.25f, 0.25f, 7, 8, 0.8f));

            levelStore.levelObjects.Add(new Item((4.0f, -0.25f, 22), (0.25f, 0.25f), 12, 0, false, Item.ItemsEnum.ArmorPatch));
            levelStore.levelObjects.Add(new Item((2.0f, -0.25f, 22), (0.25f, 0.25f), 1, 0, false, Item.ItemsEnum.SmallMedkit));

            levelStore.levelObjects.Add(new Item((3.0f, -0.25f, 22), (0.25f, 0.25f), 9, 10, false, Item.ItemsEnum.ShellPack));

            levelStore.levelObjects.Add(new Enemy((4.0f, -0.1f, 19), (0.6f, 0.9f), 14, 40, 100, 7, true, 4));
            levelStore.levelObjects.Add(new Enemy((2, -0.1f, 19), (0.6f, 0.9f), 14, 40, 100, 7, true, 4));
            
            levelStore.levelObjects.Add(new Enemy((4.0f, -0.1f, 21), (0.6f, 0.9f), 12, 40, 100, 11, true, 5));
            levelStore.levelObjects.Add(new Enemy((2, -0.1f, 21), (0.6f, 0.9f), 12, 40, 100, 11, true, 5));

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

            levelStore.successfullyLoaded = true;

            levelStore.Sync_GL_Level();

            //levelStore.ExportToFile("Levels/level1.lvl");

            return levelStore;
        }

        public static Level DemoReturn()
        {
            Level levelStore = new();
            
            // room 1 decal
            levelStore.levelObjects.Add(new LevelEndButton((0.99f, 0, 0), (-1.0f, 0, 0), 0.5f, 0.5f, 4, 0, 0.8f));

            // room 1
            levelStore.levelObjects.Add(new Cube((0, 0, -1.25f), 2.0f, 1.0f, 0.5f, 4));

            levelStore.levelObjects.Add(new Cube((0.625f, 0, 1.25f), 0.75f, 1.0f, 0.5f, 0));
            levelStore.levelObjects.Add(new Cube((-0.625f, 0, 1.25f), 0.75f, 1.0f, 0.5f, 0));

            levelStore.levelObjects.Add(new Cube((-1.25f, 0, 0), 0.5f, 1.0f, 2.0f, 0));
            levelStore.levelObjects.Add(new Cube((1.25f, 0, 0), 0.5f, 1.0f, 2.0f, 0));

            levelStore.levelObjects.Add(new Cube((0, -0.75f, 0), 2.0f, 0.5f, 2.0f, 1));
            levelStore.levelObjects.Add(new Cube((0, 0.75f, 0), 2.0f, 0.5f, 2.0f, 3));

            //corridor
            levelStore.levelObjects.Add(new Cube((0, -0.75f, 1.25f), 0.5f, 0.5f, 0.5f, 2));
            levelStore.levelObjects.Add(new Cube((0, 0.75f, 1.25f), 0.5f, 0.5f, 0.5f, 3));

            // room 2
            levelStore.levelObjects.Add(new Cube((0, -0.75f, 2.5f), 2.0f, 0.5f, 2.0f, 2));
            levelStore.levelObjects.Add(new Cube((0, 0.75f, 2.5f), 2.0f, 0.5f, 2.0f, 3));

            levelStore.levelObjects.Add(new Cube((-1.25f, 0, 2.5f), 0.5f, 1.0f, 2.0f, 0));
            levelStore.levelObjects.Add(new Cube((1.25f, 0, 2.5f), 0.5f, 1.0f, 2.0f, 0));

            levelStore.levelObjects.Add(new Cube((0, 0, 2.5f), 0.25f, 0.25f, 0.25f, 1));

            // white room
            levelStore.levelObjects.Add(new Cube((0, -0.75f, 4.5f), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((0, 0.75f, 4.5f), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((-1.25f, 0, 4.5f), 0.5f, 1.0f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((1.25f, 0, 4.5f), 0.5f, 1.0f, 2.0f, 6));

            // huge room
            levelStore.levelObjects.Add(new Cube((0, -0.75f, 10.5f), 10.0f, 0.5f, 10.0f, 3));

            // entity test
            levelStore.levelObjects.Add(new Enemy((0, -0.125f, 5.0f), (0.5f, 0.75f), 12, 0, 10f, 11, false, 5));
            levelStore.levelObjects.Add(new Enemy((5.0f, -0.125f, 5.0f), (0.5f, 0.75f), 12, 0, 10f, 11, false, 5));
            levelStore.levelObjects.Add(new Enemy((-5.0f, -0.125f, 5.0f), (0.5f, 0.75f), 12, 0, 10f, 11, false, 5));

            levelStore.levelObjects.Add(new Item((0.0f, -0.25f, 8.0f), (0.25f, 0.25f), 2, +10, false, Item.ItemsEnum.SmallMedkit));

            levelStore.Sync_GL_Level();

            //levelStore.ExportToFile("Levels/demo.lvl");

            return levelStore;
        }

        public static Level QuakeReturn()
        {
            Level levelStore = new();

            // floors

            levelStore.levelObjects.Add(new Cube((-1, -1.0f, 0), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-3, -1.0f, 0), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-1, -1.0f, 2), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-1, -1.0f, 4), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-1, -1.0f, 6), 2.0f, 0.5f, 2.0f, 11));

            levelStore.levelObjects.Add(new Cube((1, -1.0f, 0), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((1, -1.0f, 2), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((1, -1.0f, 4), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((1, -1.0f, 6), 2.0f, 0.5f, 2.0f, 11));

            // middle archway

            levelStore.levelObjects.Add(new Triangle((-2f, 1.75f, 2.5f), (-1.5f, 1.75f, 2.5f), (-2f, 1.25f, 2.5f), [(0.51f, 0.24f), (0.74f, 0.24f), (0.51f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((-2f, 1.75f, 3.5f), (-1.5f, 1.75f, 3.5f), (-2f, 1.25f, 3.5f), [(0.51f, 0.24f), (0.74f, 0.24f), (0.51f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Plane((-1.75f, 1.5f, 3.0f), (1, -1, 0), 1.0f, 0.707f, 14, 0.6f));

            levelStore.levelObjects.Add(new Triangle((-2.0f, 1.75f, 2.5f), (-1.5f, 2.25f, 2.5f), (-1.5f, 1.75f, 2.5f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((-2.0f, 1.75f, 3.5f), (-1.5f, 2.25f, 3.5f), (-1.5f, 1.75f, 3.5f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));

            levelStore.levelObjects.Add(new Triangle((-2.5f, 1.25f, 2.5f), (-2.0f, 1.75f, 2.5f), (-2.0f, 1.25f, 2.5f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((-2.5f, 1.25f, 3.5f), (-2.0f, 1.75f, 3.5f), (-2.0f, 1.25f, 3.5f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));

            levelStore.levelObjects.Add(new Triangle((2f, 1.75f, 2.5f), (1.5f, 1.75f, 2.5f), (2f, 1.25f, 2.5f), [(0.51f, 0.24f), (0.74f, 0.24f), (0.51f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((2f, 1.75f, 3.5f), (1.5f, 1.75f, 3.5f), (2f, 1.25f, 3.5f), [(0.51f, 0.24f), (0.74f, 0.24f), (0.51f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Plane((1.75f, 1.5f, 3.0f), (-1, -1, 0), 1.0f, 0.707f, 14, 0.6f));

            levelStore.levelObjects.Add(new Triangle((2.5f, 1.25f, 2.5f), (2.0f, 1.75f, 2.5f), (2.0f, 1.25f, 2.5f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((2.5f, 1.25f, 3.5f), (2.0f, 1.75f, 3.5f), (2.0f, 1.25f, 3.5f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));

            levelStore.levelObjects.Add(new Triangle((2.0f, 1.75f, 2.5f), (1.5f, 2.25f, 2.5f), (1.5f, 1.75f, 2.5f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((2.0f, 1.75f, 3.5f), (1.5f, 2.25f, 3.5f), (1.5f, 1.75f, 3.5f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            
            levelStore.levelObjects.Add(new Cube((1.0f,2.0f,3.0f), 1.0f, 0.5f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((0.0f,2.0f,3.0f), 1.0f, 0.5f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-1.0f,2.0f,3.0f), 1.0f, 0.5f, 1.0f, 14));

            // level archway

            levelStore.levelObjects.Add(new Triangle((-2f, 1.75f, 7.0f), (-1.5f, 1.75f, 7.0f), (-2f, 1.25f, 7.0f), [(0.51f, 0.24f), (0.74f, 0.24f), (0.51f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((-2f, 1.75f, 8.0f), (-1.5f, 1.75f, 8.0f), (-2f, 1.25f, 8.0f), [(0.51f, 0.24f), (0.74f, 0.24f), (0.51f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Plane((-1.75f, 1.5f, 7.5f), (1, -1, 0), 1.0f, 0.707f, 14, 0.6f));

            levelStore.levelObjects.Add(new Triangle((-2.0f, 1.75f, 7.0f), (-1.5f, 2.25f, 7.0f), (-1.5f, 1.75f, 7.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((-2.0f, 1.75f, 8.0f), (-1.5f, 2.25f, 8.0f), (-1.5f, 1.75f, 8.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));

            levelStore.levelObjects.Add(new Triangle((-2.5f, 1.25f, 7.0f), (-2.0f, 1.75f, 7.0f), (-2.0f, 1.25f, 7.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((-2.5f, 1.25f, 8.0f), (-2.0f, 1.75f, 8.0f), (-2.0f, 1.25f, 8.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));

            levelStore.levelObjects.Add(new Triangle((2f, 1.75f, 7.0f), (1.5f, 1.75f, 7.0f), (2f, 1.25f, 7.0f), [(0.51f, 0.24f), (0.74f, 0.24f), (0.51f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((2f, 1.75f, 8.0f), (1.5f, 1.75f, 8.0f), (2f, 1.25f, 8.0f), [(0.51f, 0.24f), (0.74f, 0.24f), (0.51f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Plane((1.75f, 1.5f, 7.5f), (-1, -1, 0), 1.0f, 0.707f, 14, 0.6f));

            levelStore.levelObjects.Add(new Triangle((2.5f, 1.25f, 7.0f), (2.0f, 1.75f, 7.0f), (2.0f, 1.25f, 7.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((2.5f, 1.25f, 8.0f), (2.0f, 1.75f, 8.0f), (2.0f, 1.25f, 8.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));

            levelStore.levelObjects.Add(new Triangle((2.0f, 1.75f, 7.0f), (1.5f, 2.25f, 7.0f), (1.5f, 1.75f, 7.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((2.0f, 1.75f, 8.0f), (1.5f, 2.25f, 8.0f), (1.5f, 1.75f, 8.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            
            levelStore.levelObjects.Add(new Cube((1.0f,2.0f,7.5f), 1.0f, 0.5f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((0.0f,2.0f,7.5f), 1.0f, 0.5f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-1.0f,2.0f,7.5f), 1.0f, 0.5f, 1.0f, 14));

            // bonus archway

            levelStore.levelObjects.Add(new Triangle((-2f, 1.75f, -2.0f), (-1.5f, 1.75f, -2.0f), (-2f, 1.25f, -2.0f), [(0.51f, 0.24f), (0.74f, 0.24f), (0.51f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((-2f, 1.75f, -1.0f), (-1.5f, 1.75f, -1.0f), (-2f, 1.25f, -1.0f), [(0.51f, 0.24f), (0.74f, 0.24f), (0.51f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Plane((-1.75f, 1.5f, -1.5f), (1, -1, 0), 1.0f, 0.707f, 14, 0.6f));

            levelStore.levelObjects.Add(new Triangle((-2.0f, 1.75f, -2.0f), (-1.5f, 2.25f, -2.0f), (-1.5f, 1.75f, -2.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((-2.0f, 1.75f, -1.0f), (-1.5f, 2.25f, -1.0f), (-1.5f, 1.75f, -1.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));

            levelStore.levelObjects.Add(new Triangle((-2.5f, 1.25f, -2.0f), (-2.0f, 1.75f, -2.0f), (-2.0f, 1.25f, -2.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((-2.5f, 1.25f, -1.0f), (-2.0f, 1.75f, -1.0f), (-2.0f, 1.25f, -1.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));

            levelStore.levelObjects.Add(new Triangle((2f, 1.75f, -2.0f), (1.5f, 1.75f, -2.0f), (2f, 1.25f, -2.0f), [(0.51f, 0.24f), (0.74f, 0.24f), (0.51f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((2f, 1.75f, -1.0f), (1.5f, 1.75f, -1.0f), (2f, 1.25f, -1.0f), [(0.51f, 0.24f), (0.74f, 0.24f), (0.51f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Plane((1.75f, 1.5f, -1.5f), (-1, -1, 0), 1.0f, 0.707f, 14, 0.6f));

            levelStore.levelObjects.Add(new Triangle((2.5f, 1.25f, -2.0f), (2.0f, 1.75f, -2.0f), (2.0f, 1.25f, -2.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((2.5f, 1.25f, -1.0f), (2.0f, 1.75f, -1.0f), (2.0f, 1.25f, -1.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));

            levelStore.levelObjects.Add(new Triangle((2.0f, 1.75f, -2.0f), (1.5f, 2.25f, -2.0f), (1.5f, 1.75f, -2.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            levelStore.levelObjects.Add(new Triangle((2.0f, 1.75f, -1.0f), (1.5f, 2.25f, -1.0f), (1.5f, 1.75f, -1.0f), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 0.8f));
            
            levelStore.levelObjects.Add(new Cube((1.0f,2.0f,-1.5f), 1.0f, 0.5f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((0.0f,2.0f,-1.5f), 1.0f, 0.5f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-1.0f,2.0f,-1.5f), 1.0f, 0.5f, 1.0f, 14));

            // walls
        
            //levelStore.levelObjects.Add(new Cube((-2.5f, -0.25f, 0), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Triangle((-2.0f, -0.75f, 1), (-2.0f, 0.25f, -1), (-2.0f, -0.75f, -1), [(0.51f, 0.0f), (0.74f, 0.24f), (0.74f, 0.0f)], 1.0f));
            levelStore.levelObjects.Add(new Triangle((-2.0f, -0.75f, 1), (-2.0f, 0.25f, -1), (-2.0f, 0.25f, 1), [(0.51f, 0.0f), (0.74f, 0.24f), (0.51f, 0.24f)], 1.0f));
            levelStore.levelObjects.Add(new Cube((-4.5f, -0.25f, 0), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-3.5f, -0.25f, -1.5f), 2.0f, 1.0f, 1.0f, 14));

            levelStore.levelObjects.Add(new Item((-3.0f, -0.5f, 0.0f), (0.25f, 0.25f), 5, 0, false, Item.ItemsEnum.GreenKeycard));

            levelStore.levelObjects.Add(new Cube((-3.0f, -0.25f, 2), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-3.0f, 0.75f, 0), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-2.5f, 0.75f, 2), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-2.5f, -0.25f, 4), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-2.5f, -0.25f, 6), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-2.5f, 0.75f, 4), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-2.5f, 0.75f, 6), 1.0f, 1.0f, 2.0f, 14));


            levelStore.levelObjects.Add(new Cube((2.5f, -0.25f, 0), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((2.5f, -0.25f, 2), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((2.5f, 0.75f, 0), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((2.5f, 0.75f, 2), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((2.5f, -0.25f, 4), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((2.5f, -0.25f, 6), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((2.5f, 0.75f, 4), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((2.5f, 0.75f, 6), 1.0f, 1.0f, 2.0f, 14));

            levelStore.levelObjects.Add(new Cube((1.0f, 0.75f, 7.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((1.5f, -0.25f, 7.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((3.0f, 0.75f, 7.5f), 2.0f, 1.0f, 1.0f, 14));
            //levelStore.levelObjects.Add(new Cube((3.5f, -0.25f, 7.5f), 2.0f, 1.0f, 1.0f, 14));

            levelStore.levelObjects.Add(new Cube((-1.0f, 0.75f, 7.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-1.5f, -0.25f, 7.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-3.0f, 0.75f, 7.5f), 2.0f, 1.0f, 1.0f, 14));
            //levelStore.levelObjects.Add(new Cube((-3.5f, -0.25f, 7.5f), 2.0f, 1.0f, 1.0f, 14));

            // bonus room
            levelStore.levelObjects.Add(new Door((0.0f, -0.25f, -1.5f), 1.0f, 1.0f, 0.5f, 9, HeldItem.Colors.Green, false));
            levelStore.levelObjects.Add(new Button((1.0f, -0.25f, -0.99f), (0, 0, 1), 0.5f, 0.5f, 7, 8, 0.9f, HeldItem.Colors.Green));

            levelStore.levelObjects.Add(new Cube((1.0f, 0.75f, -1.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((1.5f, -0.25f, -1.5f), 2.0f, 1.0f, 1.0f, 14));

            levelStore.levelObjects.Add(new Cube((-1.0f, 0.75f, -1.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-1.5f, -0.25f, -1.5f), 2.0f, 1.0f, 1.0f, 14));

            levelStore.levelObjects.Add(new Cube((0, -1.0f, -1.5f), 1.0f, 0.5f, 1.0f, 11));

            levelStore.levelObjects.Add(new Cube((-1, -1.0f, -3.0f), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((1, -1.0f, -3.0f), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-1, -1.0f, -5.0f), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((1, -1.0f, -5.0f), 2.0f, 0.5f, 2.0f, 11));

            levelStore.levelObjects.Add(new Cube((-1, 1.2f, -3.0f), 2.0f, 0.1f, 2.0f, 2));
            levelStore.levelObjects.Add(new Cube((1, 1.2f, -3.0f), 2.0f, 0.1f, 2.0f, 2));
            levelStore.levelObjects.Add(new Cube((-1, 1.2f, -5.0f), 2.0f, 0.1f, 2.0f, 2));
            levelStore.levelObjects.Add(new Cube((1, 1.2f, -5.0f), 2.0f, 0.1f, 2.0f, 2));

            levelStore.levelObjects.Add(new Cube((-2.5f, -0.25f, -3), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-2.5f, -0.25f, -5), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((2.5f, -0.25f, -3), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((2.5f, -0.25f, -5), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-2.5f, 0.75f, -3), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-2.5f, 0.75f, -5), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((2.5f, 0.75f, -3), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((2.5f, 0.75f, -5), 1.0f, 1.0f, 2.0f, 14));

            levelStore.levelObjects.Add(new Cube((1.0f, 0.75f, -6.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((1.0f, -0.25f, -6.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-1.0f, 0.75f, -6.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-1.0f, -0.25f, -6.5f), 2.0f, 1.0f, 1.0f, 14));

            levelStore.levelObjects.Add(new Item((0.0f, -0.5f, -5.5f), (0.25f, 0.25f), 10, 30, false, Item.ItemsEnum.MediumAmmo));
            levelStore.levelObjects.Add(new Item((-1.0f, -0.5f, -5.5f), (0.25f, 0.25f), 8, 20, false, Item.ItemsEnum.SmallAmmo));
            levelStore.levelObjects.Add(new Item((1.0f, -0.5f, -5.5f), (0.25f, 0.25f), 9, 10, false, Item.ItemsEnum.ShellPack));

            levelStore.levelObjects.Add(new Item((1.5f, -0.5f, -5f), (0.25f, 0.25f), 11, 0, false, Item.ItemsEnum.ArmorMetal));
            levelStore.levelObjects.Add(new Item((1.5f, -0.5f, -4f), (0.25f, 0.25f), 12, 0, false, Item.ItemsEnum.ArmorPatch));
            levelStore.levelObjects.Add(new Item((1.5f, -0.5f, -3f), (0.25f, 0.25f), 11, 0, false, Item.ItemsEnum.ArmorMetal));

            levelStore.levelObjects.Add(new Item((-1.5f, -0.5f, -5f), (0.25f, 0.25f), 2, 0, false, Item.ItemsEnum.LargeMedkit));
            levelStore.levelObjects.Add(new Item((-1.5f, -0.5f, -4f), (0.25f, 0.25f), 2, 0, false, Item.ItemsEnum.LargeMedkit));
            levelStore.levelObjects.Add(new Item((-1.5f, -0.5f, -3f), (0.25f, 0.25f), 2, 0, false, Item.ItemsEnum.LargeMedkit));

            // second room
            levelStore.levelObjects.Add(new Cube((0, -1.0f, 8), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((2, -1.0f, 9), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-2, -1.0f, 9), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((0, -1.125f, 10), 2.0f, 0.5f, 2.0f, 1));
            levelStore.levelObjects.Add(new Cube((2, -1.0f, 11), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-2, -1.0f, 11), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((0, -1.0f, 12), 2.0f, 0.5f, 2.0f, 11));

            levelStore.levelObjects.Add(new Enemy((2, -0.35f, 10), (0.6f,0.9f), 12, 20, 100, 11, true, 5));
            levelStore.levelObjects.Add(new Enemy((-2, -0.35f, 10), (0.6f,0.9f), 12, 20, 100, 11, true, 5));

            levelStore.levelObjects.Add(new Cube((-3.5f, 0.75f, 9f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-3.5f, -0.25f, 9f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-3.5f, 0.75f, 11f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-3.5f, -0.25f, 11f), 1.0f, 1.0f, 2.0f, 14));

            levelStore.levelObjects.Add(new Cube((3.5f, 0.75f, 9f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((3.5f, -0.25f, 9f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((3.5f, 0.75f, 11f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((3.5f, -0.25f, 11f), 1.0f, 1.0f, 2.0f, 14));

            levelStore.levelObjects.Add(new Plane((-2.75f, -0.25f, 8.25f), (1,0,1), 0.708f, 1.0f, 14, 0.88f));
            levelStore.levelObjects.Add(new Plane((-2.75f, 0.75f, 8.25f), (1,0,1), 0.708f, 1.0f, 14, 0.88f));
            levelStore.levelObjects.Add(new Plane((-2.75f, -0.25f, 11.75f), (1,0,-1), 0.708f, 1.0f, 14, 0.8f));
            levelStore.levelObjects.Add(new Plane((-2.75f, 0.75f, 11.75f), (1,0,-1), 0.708f, 1.0f, 14, 0.8f));

            levelStore.levelObjects.Add(new Plane((2.75f, -0.25f, 8.25f), (-1,0,1), 0.708f, 1.0f, 14, 0.8f));
            levelStore.levelObjects.Add(new Plane((2.75f, 0.75f, 8.25f), (-1,0,1), 0.708f, 1.0f, 14, 0.8f));
            levelStore.levelObjects.Add(new Plane((2.75f, -0.25f, 11.75f), (-1,0,-1), 0.708f, 1.0f, 14, 0.8f));
            levelStore.levelObjects.Add(new Plane((2.75f, 0.75f, 11.75f), (-1,0,-1), 0.708f, 1.0f, 14, 0.8f));

            // exit second room
            levelStore.levelObjects.Add(new Cube((1.0f, 0.75f, 12.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((1.5f, -0.25f, 12.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((3.0f, 0.75f, 12.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((3.5f, -0.25f, 12.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((5.0f, 0.75f, 12.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((5.5f, -0.25f, 12.5f), 2.0f, 1.0f, 1.0f, 14));
            //levelStore.levelObjects.Add(new Cube((3.5f, -0.25f, 7.5f), 2.0f, 1.0f, 1.0f, 14));

            levelStore.levelObjects.Add(new Cube((-1.0f, 0.75f, 12.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-1.5f, -0.25f, 12.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-3.0f, 0.75f, 12.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-3.5f, -0.25f, 12.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-5.0f, 0.75f, 12.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-5.5f, -0.25f, 12.5f), 2.0f, 1.0f, 1.0f, 14));
            //levelStore.levelObjects.Add(new Cube((-3.5f, -0.25f, 7.5f), 2.0f, 1.0f, 1.0f, 14));

            levelStore.levelObjects.Add(new Cube((1.0f, 0.75f, 15.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((1.0f, -0.25f, 15.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((3.0f, 0.75f, 16f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((3.0f, -0.25f, 16f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((5.0f, 0.75f, 15.5f), 2.0f, 1.0f, 1.0f, 14));
            //levelStore.levelObjects.Add(new Cube((5.0f, -0.25f, 15.5f), 2.0f, 1.0f, 1.0f, 14));
            

            levelStore.levelObjects.Add(new Cube((-1.0f, 0.75f, 15.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-1.0f, -0.25f, 15.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-3.0f, 0.75f, 16f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-3.0f, -0.25f, 16f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-5.0f, 0.75f, 15.5f), 2.0f, 1.0f, 1.0f, 14));
            //levelStore.levelObjects.Add(new Cube((-5.0f, -0.25f, 15.5f), 2.0f, 1.0f, 1.0f, 14));

            levelStore.levelObjects.Add(new Cube((1, -1.0f, 14), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-1, -1.0f, 14), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((3, -1.0f, 14), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-3, -1.0f, 14), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((5, -1.0f, 14), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-5, -1.0f, 14), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((5, -1.0f, 16), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-5, -1.0f, 16), 2.0f, 0.5f, 2.0f, 11));
            
            levelStore.levelObjects.Add(new Cube((-6.5f, -0.25f, 14f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-6.5f, 0.75f, 14f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-6.5f, -0.25f, 16f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-6.5f, 0.75f, 16f), 1.0f, 1.0f, 2.0f, 14));

            levelStore.levelObjects.Add(new Cube((6.5f, -0.25f, 14f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((6.5f, 0.75f, 14f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((6.5f, -0.25f, 16f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((6.5f, 0.75f, 16f), 1.0f, 1.0f, 2.0f, 14));
            

            //levelStore.levelObjects.Add(new LevelEndButton((1.99f, 0.25f, 3.0f), (-1, 0, 0), 0.5f, 0.5f, 7, 8, 0.9f));

            levelStore.successfullyLoaded = true;

            levelStore.Sync_GL_Level();

            return levelStore;
        }
    }
}