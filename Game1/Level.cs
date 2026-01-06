using OpenTK.Mathematics;

namespace Game1 {
    public class Level
    {
        
        public List<Object> levelObjects;
        public List<Object> temporaryObjects;
        
        public float[] GL_Level;
        public bool successfullyLoaded = true;

        public Level()
        {
            levelObjects = [];
            temporaryObjects = [];
            GL_Level = [];
        }

        public void Sync_GL_Level()
        {
            GL_Level = GenerateStaticFloatData();
        } 

        private float[] GenerateStaticFloatData()
        {
            List<float> staticFloatData = [];

            foreach (Object levelObject in levelObjects)
            {
                if (levelObject.objectType == Object.ObjectType.Triangle || levelObject.objectType == Object.ObjectType.Plane ||levelObject.objectType == Object.ObjectType.Cube)
                {
                    staticFloatData.AddRange(levelObject.GenerateFloatData((0,0,0)));
                }
            }

            return [..staticFloatData];
        }

        public float[] GenerateTileEntityFloatData()
        {
            List<float> tileEntityFloatData = [];

            foreach (Object levelObject in levelObjects)
            {
                if (levelObject.objectType == Object.ObjectType.Door || levelObject.objectType == Object.ObjectType.Button ||levelObject.objectType == Object.ObjectType.EndButton)
                {
                    tileEntityFloatData.AddRange(levelObject.GenerateFloatData((0,0,0)));
                }
            }

            return [..tileEntityFloatData];
        }

        public float[] GenerateEntityFloatData(Vector3 playerRotation)
        {
            List<float> entityFloatData = [];

            foreach (Object levelObject in levelObjects)
            {
                if (levelObject.objectType == Object.ObjectType.Entity)
                {
                    entityFloatData.AddRange(levelObject.GenerateFloatData(playerRotation));
                }
            }
            foreach (Object tempObject in temporaryObjects)
            {
                if (tempObject.objectType == Object.ObjectType.Entity)
                {
                    entityFloatData.AddRange(tempObject.GenerateFloatData(playerRotation));
                }
            }

            return [..entityFloatData];
        }

        public static void ImportLevelFromFile(string fileLocation, out Level level)
        {
            level = new();

            BinaryReader levelFile;

            try
            {
                levelFile = new(File.Open(fileLocation, FileMode.Open));
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Missing file: {0}", fileLocation);
                level.successfullyLoaded = false;
                return;
            }

            try
            {
                while (!(levelFile.BaseStream.Position == levelFile.BaseStream.Length))
                {
                    Object.ObjectType type = (Object.ObjectType)levelFile.ReadByte();
                    switch (type)
                    {
                        case Object.ObjectType.None:
                            break;
                        case Object.ObjectType.Triangle:
                            Triangle.LoadFromFile(out Triangle triTemp, ref levelFile);
                            level.levelObjects.Add(triTemp);
                            break;
                        case Object.ObjectType.Plane:
                            Plane.LoadFromFile(out Plane planeTemp, ref levelFile);
                            level.levelObjects.Add(planeTemp);
                            break;
                        case Object.ObjectType.Cube:
                            Cube.LoadFromFile(out Cube cubeTemp, ref levelFile);
                            level.levelObjects.Add(cubeTemp);
                            break;

                        case Object.ObjectType.Entity:
                            Entity.EntityType entityType = (Entity.EntityType)levelFile.ReadByte();
                            switch (entityType)
                            {
                                case Entity.EntityType.None:
                                    Entity.LoadFromFile(out Entity entityTemp, ref levelFile);
                                    level.levelObjects.Add(entityTemp);
                                    break;

                                case Entity.EntityType.Enemy:
                                    Enemy.LoadFromFile(out Enemy enemyTemp, ref levelFile);
                                    level.levelObjects.Add(enemyTemp);
                                    break;

                                case Entity.EntityType.Item:
                                    Item.LoadFromFile(out Item itemTemp, ref levelFile);
                                    level.levelObjects.Add(itemTemp);
                                    break;
                            }
                            break;
                        
                        case Object.ObjectType.Button:
                            Button.LoadFromFile(out Button buttonTemp, ref levelFile);
                            level.levelObjects.Add(buttonTemp);
                            break;
                        case Object.ObjectType.EndButton:
                            LevelEndButton.LoadFromFile(out LevelEndButton LE_buttonTemp, ref levelFile);
                            level.levelObjects.Add(LE_buttonTemp);
                            break;
                        case Object.ObjectType.Door:
                            Door.LoadFromFile(out Door doorTemp, ref levelFile);
                            level.levelObjects.Add(doorTemp);
                            break;
                    }
                }
            } catch
            {
                Console.WriteLine("Possibly corrupt level file!");
                level.successfullyLoaded = false;
                levelFile.Close();
                return;
            }
            levelFile.Close();
            level.Sync_GL_Level();
        }

        public void ExportToFile(string levelFileName)
        {
            BinaryWriter levelWriter = new(File.Open(levelFileName, FileMode.Create));

            foreach (Object obj in levelObjects)
            {
                levelWriter.Write((byte)obj.objectType);
                obj.ExportToFile(ref levelWriter);
            }

            levelWriter.Close();
        }
    }

}