using OpenTK.Mathematics;

namespace Game1
{
    public class Player
    {
        public Vector3 Position, Scale;
        public Vector3 upRotation, moveRotation;
        public float Health;

        public Player(Vector3 positionIn, Vector3 scaleIn, Vector3 moveRotationIn, float healthIn)
        {
            Position = positionIn;
            Scale = scaleIn;
            moveRotation = moveRotationIn;
            Health = healthIn;
            upRotation = new(0);
        }

        /*
        private Vector3 Position;
        private Vector3 playerScale = (0.25f, 0.5f, 0.25f);
        private Vector3 moveRotation;
        private Vector3 upRotation;
        private Vector3 front;
        private float health = 100;
        */
    }
    

}