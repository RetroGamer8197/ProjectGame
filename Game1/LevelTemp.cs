using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

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

            levelStore.levelObjects.Add(new Enemy((1, -0.35f, 6), (0.6f,0.9f), 12, 20, 100, 11, true, 5));
            levelStore.levelObjects.Add(new Enemy((-1, -0.35f, 6), (0.6f,0.9f), 12, 20, 100, 11, true, 5));

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
            levelStore.levelObjects.Add(new Item((1.0f, -0.5f, -5.5f), (0.25f, 0.25f), 9, 12, false, Item.ItemsEnum.ShellPack));

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

            // left corridor
            levelStore.levelObjects.Add(new Cube((5, -1.0f, 18), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Enemy((5, -0.3f, 18), (0.8f, 0.9f), 2, 60, 300, 7, true, 7));
            levelStore.levelObjects.Add(new Enemy((9, -0.3f, 18), (0.8f, 0.9f), 2, 60, 300, 7, true, 7));
            levelStore.levelObjects.Add(new Cube((5f, -0.25f, 20f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((5f, 0.75f, 20f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((3.5f, -0.25f, 18f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((3.5f, 0.75f, 18f), 1.0f, 1.0f, 2.0f, 14));

            levelStore.levelObjects.Add(new Cube((7, -1.0f, 18), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((7f, -0.25f, 20), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((7f, 0.75f, 20f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((8f, -0.25f, 16.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((8f, 0.75f, 16.5f), 2.0f, 1.0f, 1.0f, 14));
            
            levelStore.levelObjects.Add(new Cube((9, -1.0f, 18), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((9f, -0.25f, 20f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((9f, 0.75f, 20f), 2.0f, 1.0f, 2.0f, 14));

            levelStore.levelObjects.Add(new Enemy((5, 0.5f, 22f), (0.5f, 0.5f), 0, 40, 150, 7, true, 7));
            levelStore.levelObjects.Add(new Enemy((7, 0.5f, 22f), (0.5f, 0.5f), 0, 40, 150, 7, true, 7));

            levelStore.levelObjects.Add(new Cube((10f, -0.25f, 16.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((10f, 0.75f, 16.5f), 2.0f, 1.0f, 1.0f, 14));

            levelStore.levelObjects.Add(new Cube((11, -1.0f, 18), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((12f, -0.25f, 16.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((12f, 0.75f, 16.5f), 2.0f, 1.0f, 1.0f, 14));

            levelStore.levelObjects.Add(new Cube((12.5f, -0.25f, 22f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((12.5f, 0.75f, 22f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((12.5f, -0.25f, 20), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((12.5f, 0.75f, 20f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((12.5f, -0.25f, 18), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((12.5f, 0.75f, 18f), 1.0f, 1.0f, 2.0f, 14));

            levelStore.levelObjects.Add(new Cube((11, -1.0f, 20), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((11, -1.0f, 22), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((11f, -0.25f, 24f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((11f, 0.75f, 24f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((9f, -0.25f, 24f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((9f, 0.75f, 24f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((7f, -0.25f, 24f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((7f, 0.75f, 24f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((7f, -0.25f, 26f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((7f, 0.75f, 26f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((7f, -0.25f, 28f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((7f, 0.75f, 28f), 2.0f, 1.0f, 2.0f, 14));

            levelStore.levelObjects.Add(new Cube((5f, -0.25f, 29.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((5f, 0.75f, 29.5f), 2.0f, 1.0f, 1.0f, 14));

            levelStore.levelObjects.Add(new Cube((3f, -0.25f, 22f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((3f, 0.75f, 22f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((3f, -0.25f, 24f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((3f, 0.75f, 24f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((3f, -0.25f, 26f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((3f, 0.75f, 26f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((3f, -0.25f, 28f), 2.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((3f, 0.75f, 28f), 2.0f, 1.0f, 2.0f, 14));

            levelStore.levelObjects.Add(new Cube((9, -1.0f, 22), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((7, -1.0f, 22), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((5, -1.0f, 22), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((5, -1.0f, 24), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((5, -1.0f, 26), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((5, -1.0f, 28), 2.0f, 0.5f, 2.0f, 11));

            levelStore.levelObjects.Add(new Item((5f, -0.5f, 28f), (0.25f, 0.25f), 5, 0, false, Item.ItemsEnum.RedKeycard));
            levelStore.levelObjects.Add(new Item((5f, -0.5f, 26f), (0.25f, 0.25f), 15, 5, false, Item.ItemsEnum.LargeAmmo));
            levelStore.levelObjects.Add(new Item((5.5f, -0.5f, 26f), (0.25f, 0.25f), 9, 12, false, Item.ItemsEnum.ShellPack));
            levelStore.levelObjects.Add(new Item((4.5f, -0.5f, 26f), (0.25f, 0.25f), 8, 25, false, Item.ItemsEnum.SmallAmmo));
            levelStore.levelObjects.Add(new Item((4.5f, -0.5f, 27f), (0.25f, 0.25f), 10, 30, false, Item.ItemsEnum.MediumAmmo));
            levelStore.levelObjects.Add(new Item((5.5f, -0.5f, 27f), (0.25f, 0.25f), 15, 5, false, Item.ItemsEnum.LargeAmmo));
            levelStore.levelObjects.Add(new Item((5.0f, -0.5f, 27f), (0.25f, 0.25f), 10, 30, false, Item.ItemsEnum.MediumAmmo));
            levelStore.levelObjects.Add(new Item((4.5f, -0.5f, 28f), (0.25f, 0.25f), 2, 0, false, Item.ItemsEnum.LargeMedkit));
            levelStore.levelObjects.Add(new Item((5.5f, -0.5f, 28f), (0.25f, 0.25f), 11, 0, false, Item.ItemsEnum.ArmorMetal));


            // right corridor
            levelStore.levelObjects.Add(new Cube((-5, -1.0f, 18), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-5f, -0.25f, 19.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-5f, 0.75f, 19.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-3.5f, -0.25f, 18f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-3.5f, 0.75f, 18f), 1.0f, 1.0f, 2.0f, 14));

            levelStore.levelObjects.Add(new Cube((-7, -1.0f, 18), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-7f, -0.25f, 19.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-7f, 0.75f, 19.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-8f, -0.25f, 16.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-8f, 0.75f, 16.5f), 2.0f, 1.0f, 1.0f, 14));
            
            levelStore.levelObjects.Add(new Cube((-9, -1.0f, 18), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-9f, -0.25f, 19.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-9f, 0.75f, 19.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-10f, -0.25f, 16.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-10f, 0.75f, 16.5f), 2.0f, 1.0f, 1.0f, 14));
            
            levelStore.levelObjects.Add(new Cube((-10.5f, 0.75f, 18f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-10.5f, -0.25f, 16.5f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-10.5f, -0.25f, 19.5f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Door((-10.5f, -0.25f, 18f), 0.5f, 1.0f, 1.0f, 9, HeldItem.Colors.Red, false));
            levelStore.levelObjects.Add(new Button((-9.25f, -0.25f, 18.99f), (0,0,-1), 0.5f, 0.5f, 7, 8, 0.88f, HeldItem.Colors.Red));
            levelStore.levelObjects.Add(new Cube((-11, -1.0f, 18), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Cube((-13, -1.0f, 18), 2.0f, 0.5f, 2.0f, 11));
            levelStore.levelObjects.Add(new Enemy((-13, -0.3f, 18), (0.8f, 0.9f), 2, 60, 300, 7, true, 7));

            levelStore.levelObjects.Add(new Cube((-11f, -0.25f, 19.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-11f, 0.75f, 19.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-12f, -0.25f, 16.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-12f, 0.75f, 16.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-13f, -0.25f, 19.5f), 2.0f, 1.0f, 1.0f, 14));
            levelStore.levelObjects.Add(new Cube((-13f, 0.75f, 19.5f), 2.0f, 1.0f, 1.0f, 14));

            levelStore.levelObjects.Add(new Cube((-13.5f, 0.75f, 18f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-13.5f, -0.25f, 16.5f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-13.5f, -0.25f, 19.5f), 1.0f, 1.0f, 2.0f, 14));
            levelStore.levelObjects.Add(new Cube((-13.5f, -0.25f, 18f), 0.5f, 1.0f, 1.0f, 9));
            levelStore.levelObjects.Add(new LevelEndButton((-12f, -0.25f, 18.99f), (0,0,-1), 0.5f, 0.5f, 7, 8, 0.88f));

            //levelStore.levelObjects.Add(new LevelEndButton((1.99f, 0.25f, 3.0f), (-1, 0, 0), 0.5f, 0.5f, 7, 8, 0.9f));

            levelStore.successfullyLoaded = true;

            levelStore.Sync_GL_Level();

            return levelStore;
        }

        public static Level AllObjects()
        {
            Level levelStore = new();

            levelStore.levelObjects.Add(new Cube((0,-1f, 0), 1.0f, 0.5f, 1.0f, 0));

            levelStore.levelObjects.Add(new Cube((1,0.25f, 2f), 0.5f, 0.5f, 0.5f, 1));

            levelStore.levelObjects.Add(new Triangle((0.25f, 0f, 2f), (-0.25f, 0f, 2f), (0, 0.5f, 2f), [(0.5f, 0f), (0.75f, 0f), (0.625f, 0.25f)], 0.88f));

            levelStore.levelObjects.Add(new Plane((-1,0.25f, 2f), (0,0,-1), 0.5f, 0.5f, 2, 0.88f));

            levelStore.levelObjects.Add(new Door((1,-0.5f, 2f), 0.5f, 0.5f, 0.25f, 9, HeldItem.Colors.Green, false));

            levelStore.levelObjects.Add(new Enemy((0,-0.5f, 2f), (0.5f, 0.5f), 1, 100, 100, 1, false, 0f));

            levelStore.levelObjects.Add(new Button((-1,-0.5f, 2f), (0,0,-1), 0.5f, 0.5f, 7, 8, 0.88f, HeldItem.Colors.Green));

            levelStore.successfullyLoaded = true;

            levelStore.Sync_GL_Level();

            return levelStore;
        }

        public static Level Vertical_Return()
        {
            Level levelStore = new();

            levelStore.levelObjects.Add(new Cube((0f,0.5f,-2.0f), 2.0f, 0.5f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-1.5f,-0.25f,-1.5f), 2.0f, 1.0f, 1.0f, 5));
            levelStore.levelObjects.Add(new Cube((1.5f,-0.25f,-1.5f), 2.0f, 1.0f, 1.0f, 5));

            levelStore.levelObjects.Add(new Cube((-1.5f,-0.25f,-3f), 1.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((1.5f,-0.25f,-3f), 1.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((0f,-0.25f,-3.5f), 2.0f, 1.0f, 1.0f, 5));

            levelStore.levelObjects.Add(new Door((0f,-0.25f,-1.5f), 1.0f, 1.0f, 0.5f, 9, HeldItem.Colors.Blue, false));

            levelStore.levelObjects.Add(new LevelEndButton((0f,-0.25f,-2.99f), (0,0,1f), 0.5f, 0.5f, 7, 8, 0.88f));

            levelStore.levelObjects.Add(new Cube((0,-1,-2), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((0,-1,0), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Item((-0.5f,-0.5f,-0.5f), (0.25f, 0.25f), 9, 12, false, Item.ItemsEnum.ShellPack));
            levelStore.levelObjects.Add(new Item((0.5f,-0.5f,-0.5f), (0.25f, 0.25f), 10, 30, false, Item.ItemsEnum.MediumAmmo));

            levelStore.levelObjects.Add(new Cube((0,-1,2), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((0,-1,4), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((0,0.875f,0), 2.0f, 0.25f, 2.0f, 3));
            levelStore.levelObjects.Add(new Cube((0,0.875f,2), 2.0f, 0.25f, 2.0f, 3));
            levelStore.levelObjects.Add(new Cube((0,0.875f,4), 2.0f, 0.25f, 2.0f, 3));

            levelStore.levelObjects.Add(new Cube((0,-0.25f,6), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((0,0.75f,6), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((0,1.75f,6), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((0,2.75f,6), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((-2,0.75f,2), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2,0.75f,2), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2,0.75f,0), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2,0.75f,0), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((-2,0.25f,4), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2,-0.75f,4), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2,-0.25f,2), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2,-0.25f,2), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2,-0.25f,0), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2,-0.25f,0), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((2,1.125f,4), 2.0f, 0.25f, 2.0f, 3));
            levelStore.levelObjects.Add(new Cube((2,1.75f,4), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2,2.75f,4), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((2,-0.75f,4), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((2,-0.5f,6), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((2,-0.25f,8), 2.0f, 0.5f, 2.0f,6));
            levelStore.levelObjects.Add(new Cube((0,0.0f,8), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-2,0.25f,8), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-2,0.5f,6), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((-2,0.875f,4), 2.0f, 0.25f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((0,1.125f,4), 2.0f, 0.25f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((0,1.125f,2), 2.0f, 0.25f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((0,1.125f,0), 2.0f, 0.25f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((0,1f,-2), 2.0f, 0.5f, 2.0f, 6));

            Vector3[] pillars =
            {
                new(4, 0, 4), new(4, 0, 6), new(4, 0, 8),
                new(2, 0, 10), new(0, 0, 10), new(-2, 0, 10),
                new(-4, 0, 4), new(-4, 0, 6), new(-4, 0, 8),
            };

            foreach (Vector3 pillar in pillars)
            {
                levelStore.levelObjects.Add(new Cube((pillar.X,-0.25f,pillar.Z), 2.0f, 1.0f, 2.0f, 5));
                levelStore.levelObjects.Add(new Cube((pillar.X,0.75f,pillar.Z), 2.0f, 1.0f, 2.0f, 5));
                levelStore.levelObjects.Add(new Cube((pillar.X,1.75f,pillar.Z), 2.0f, 1.0f, 2.0f, 5));
                levelStore.levelObjects.Add(new Cube((pillar.X,2.75f,pillar.Z), 2.0f, 1.0f, 2.0f, 5));
            }
            
            levelStore.levelObjects.Add(new Cube((2,2.75f,2), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2,1.75f,2), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2,2.75f,2), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2,1.75f,2), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((2,2.75f,0), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2,1.75f,0), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2,2.75f,0), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2,1.75f,0), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Cube((2,2.75f,-2), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2,1.75f,-2), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2,2.75f,-2), 2.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-2,1.75f,-2), 2.0f, 1.0f, 2.0f, 5));

            levelStore.levelObjects.Add(new Door((1.25f,2.75f,-4), 0.5f, 1.0f, 2.0f, 5, HeldItem.Colors.Blue, false));
            levelStore.levelObjects.Add(new Door((1.25f,1.75f,-4), 0.5f, 1.0f, 2.0f, 5, HeldItem.Colors.Blue, false));
            levelStore.levelObjects.Add(new Door((-1.25f,2.75f,-4), 0.5f, 1.0f, 2.0f, 5, HeldItem.Colors.Blue, false));
            levelStore.levelObjects.Add(new Door((-1.25f,1.75f,-4), 0.5f, 1.0f, 2.0f, 5, HeldItem.Colors.Blue, false));

            levelStore.levelObjects.Add(new Cube((3,2.75f,-4), 1.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((3,1.75f,-4), 1.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-3,2.75f,-4), 1.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((-3,1.75f,-4), 1.0f, 1.0f, 2.0f, 5));
            levelStore.levelObjects.Add(new Cube((2f,1.75f,-5.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((2f,2.75f,-5.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-2f,1.75f,-5.25f), 2.0f, 1.0f, 0.5f, 5));
            levelStore.levelObjects.Add(new Cube((-2f,2.75f,-5.25f), 2.0f, 1.0f, 0.5f, 5));

            levelStore.levelObjects.Add(new Enemy((-1.5f,1.65f,-4.5f), (0.6f, 0.9f), 12, 20, 100, 11, true, 5));
            levelStore.levelObjects.Add(new Enemy((-1.5f,1.65f,-3.5f), (0.6f, 0.9f), 12, 20, 100, 11, true, 5));
            levelStore.levelObjects.Add(new Enemy((1.5f,1.65f,-4.5f), (0.6f, 0.9f), 12, 20, 100, 11, true, 5));
            levelStore.levelObjects.Add(new Enemy((1.5f,1.65f,-3.5f), (0.6f, 0.9f), 12, 20, 100, 11, true, 5));

            levelStore.levelObjects.Add(new Cube((-2,1.0f,-4), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((0,1.0f,-4), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((2,1.0f,-4), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Enemy((-2,1.65f,-6), (0.6f, 0.9f), 12, 20, 100, 11, true, 5));
            levelStore.levelObjects.Add(new Enemy((-0,1.65f,-6), (0.6f, 0.9f), 12, 20, 100, 11, true, 5));
            levelStore.levelObjects.Add(new Enemy((2,1.65f,-6), (0.6f, 0.9f), 14, 40, 100, 7, true, 4));
            levelStore.levelObjects.Add(new Enemy((-2,1.65f,-8), (0.6f, 0.9f), 12, 20, 100, 11, true, 5));
            levelStore.levelObjects.Add(new Enemy((0,1.65f,-8), (0.6f, 0.9f), 14, 40, 100, 7, true, 4));
            levelStore.levelObjects.Add(new Enemy((2,1.65f,-8), (0.6f, 0.9f), 12, 20, 100, 11, true, 5));

            levelStore.levelObjects.Add(new Cube((-2,1.0f,-6), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((0,1.0f,-6), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((2,1.0f,-6), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((-2,1.0f,-8), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((0,1.0f,-8), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((2,1.0f,-8), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Cube((-2,1.0f,-10), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((0,1.0f,-10), 2.0f, 0.5f, 2.0f, 6));
            levelStore.levelObjects.Add(new Cube((2,1.0f,-10), 2.0f, 0.5f, 2.0f, 6));

            levelStore.levelObjects.Add(new Item((0,1.5f,-10), (0.25f, 0.25f), 4, 0, false, Item.ItemsEnum.BlueKeycard));
            levelStore.levelObjects.Add(new Item((-2,1.5f,-10), (0.25f, 0.25f), 2, 0, false, Item.ItemsEnum.LargeMedkit));
            levelStore.levelObjects.Add(new Item((2,1.5f,-10), (0.25f, 0.25f), 9, 12, false, Item.ItemsEnum.ShellPack));

            Vector3[] toproomwalls =
            {
                new(-4, 0, -6), new(-4, 0, -8), new(-4, 0, -10),
                new(2, 0, -12), new(0, 0, -12), new(-2, 0, -12),
                new(4, 0, -6), new(4, 0, -8), new(4, 0, -10),
            };

            foreach (Vector3 wall in toproomwalls)
            {
                levelStore.levelObjects.Add(new Cube((wall.X,1.75f,wall.Z), 2.0f, 1.0f, 2.0f, 5));
                levelStore.levelObjects.Add(new Cube((wall.X,2.75f,wall.Z), 2.0f, 1.0f, 2.0f, 5));
            }

            levelStore.levelObjects.Add(new Button((0,2.0f,-10.99f), (0,0,1), 0.5f, 0.5f, 7, 8, 0.88f, HeldItem.Colors.Blue));

            levelStore.successfullyLoaded = true;

            levelStore.Sync_GL_Level();

            return levelStore;
        }

        public static Level Level3Return()
        {
            Level levelStore = new();

            levelStore.levelObjects.Add(new LevelEndCondition());

            for (int x = -10; x < 11; x+=2)
            {
                for (int z = -10; z < 11; z+=2)
                {
                    levelStore.levelObjects.Add(new Cube((x,-1,z), 2, 0.5f, 2, 2));
                }
            }

            for (int z = -10; z < 11; z+=2)
            {
                levelStore.levelObjects.Add(new Cube((-10.5f,-0.25f,z), 1, 1f, 2, 14));
                levelStore.levelObjects.Add(new Cube((10.5f,-0.25f,z), 1, 1f, 2, 14));
                levelStore.levelObjects.Add(new Cube((-10.5f,0.75f,z), 1, 1f, 2, 14));
                levelStore.levelObjects.Add(new Cube((10.5f,0.75f,z), 1, 1f, 2, 14));

                levelStore.levelObjects.Add(new Cube((-10.5f,1.75f,z), 1, 1f, 2, 14));
                levelStore.levelObjects.Add(new Cube((10.5f,1.75f,z), 1, 1f, 2, 14));
                levelStore.levelObjects.Add(new Cube((-10.5f,2.75f,z), 1, 1f, 2, 14));
                levelStore.levelObjects.Add(new Cube((10.5f,2.75f,z), 1, 1f, 2, 14));

                levelStore.levelObjects.Add(new Plane((9.5f,2.75f,z), (-1, -1, 0), 2, 1.414f, 14, 0.6f));
                levelStore.levelObjects.Add(new Plane((-9.5f,2.75f,z), (1, -1, 0), 2, 1.414f, 14, 0.6f));
            }

            for (int x = -10; x < 11; x+=2)
            {
                levelStore.levelObjects.Add(new Cube((x,-0.25f,-10.5f), 2, 1f, 1, 14));
                levelStore.levelObjects.Add(new Cube((x,-0.25f,10.5f), 2, 1f, 1, 14));
                levelStore.levelObjects.Add(new Cube((x,0.75f,-10.5f), 2, 1f, 1, 14));
                levelStore.levelObjects.Add(new Cube((x,0.75f,10.5f), 2, 1f, 1, 14));

                levelStore.levelObjects.Add(new Cube((x,1.75f,-10.5f), 2, 1f, 1, 14));
                levelStore.levelObjects.Add(new Cube((x,1.75f,10.5f), 2, 1f, 1, 14));
                levelStore.levelObjects.Add(new Cube((x,2.75f,-10.5f), 2, 1f, 1, 14));
                levelStore.levelObjects.Add(new Cube((x,2.75f,10.5f), 2, 1f, 1, 14));

                levelStore.levelObjects.Add(new Plane((x,2.75f,9.5f), (0, -1, -1), 2, 1.414f, 14, 0.5f));
                levelStore.levelObjects.Add(new Plane((x,2.75f,-9.5f), (0, -1, 1), 2, 1.414f, 14, 0.7f));
            }

            levelStore.levelObjects.Add(new Enemy((0, 0.7f, -4f), (2.0f, 2.0f), 0, 50, 2000f, 7, true, 15));
            
            Vector3[] enemies = [new(-4, 0.5f, 4), new(-4, 0.5f, -4), new(4, 0.5f, -4), new(4, 0.5f, 4), new(-4, 0.5f, 0)];

            for (int i = 0; i < 3; i++)
            {
                levelStore.levelObjects.Add(new Enemy(enemies[i], (0.6f, 0.6f), 0, 40, 150f, 7, true, 7));
            }

            Vector3[] large = [new(-6, -0.5f, 4), new(2, -0.5f, 0), new(4, -0.5f, 8), new(-4, -0.5f, -8)];

            for (int i = 0; i < 4; i++)
            {
                levelStore.levelObjects.Add(new Item(large[i], (0.25f, 0.25f), 15, 5, false, Item.ItemsEnum.LargeAmmo));
            }

            Vector3[] medium = [new(4, -0.5f, 4), new(0, -0.5f, -2), new(-4, -0.5f, 2), new(8, -0.5f, 0), new(4, -0.5f, 0)];

            for (int i = 0; i < 5; i++)
            {
                levelStore.levelObjects.Add(new Item(medium[i], (0.25f, 0.25f), 10, 30, false, Item.ItemsEnum.MediumAmmo));
            }

            Vector3[] shells = [new(6, -0.5f, 6), new(-2, -0.5f, 6), new(-8, -0.5f, 8)];

            for (int i = 0; i < 3; i++)
            {
                levelStore.levelObjects.Add(new Item(shells[i], (0.25f, 0.25f), 9, 12, false, Item.ItemsEnum.ShellPack));
            }

            Vector3[] medkits = [new(8, -0.5f, 8), new(-4, -0.5f, 6), new(-2, -0.5f, -4), new(-8, -0.5f, 2), new(2, -0.5f, -6)];

            for (int i = 0; i < 5; i++)
            {
                levelStore.levelObjects.Add(new Item(medkits[i], (0.25f, 0.25f), 2, 0, false, Item.ItemsEnum.LargeMedkit));
            }
            
            levelStore.successfullyLoaded = true;

            levelStore.Sync_GL_Level();

            return levelStore;
        }
    }
}