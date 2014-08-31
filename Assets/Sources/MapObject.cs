using System;
using UnityEngine;

namespace sheepgame
{
    public abstract class MapObject
    {
        private GameObject gameObject;

        public abstract Direction Move (Direction playerMovement);

		public abstract char DrawGlyph ();

		public abstract bool IsCharacter ();
	}

	abstract class NonPlayerObject : MapObject
	{
		public override bool IsCharacter ()
		{
			return false;
		}
	}

	class Player : MapObject
	{
		public override Direction Move (Direction playerMovement)
		{
			return playerMovement;
		}

		public override char DrawGlyph ()
		{
			return 'C';
		}

		public override bool IsCharacter ()
		{
			return true;
		}
	}

	class Sheep : NonPlayerObject
	{
		public override Direction Move (Direction playerMovement)
		{
			return new Direction (-playerMovement.x, -playerMovement.y);
		}
		
		public override char DrawGlyph ()
		{
			return 'S';
		}
	}

	class Rock : NonPlayerObject
	{
		public override Direction Move (Direction playerMovement)
		{
			return new Direction (0, 0);
		}
		
		public override char DrawGlyph ()
		{
			return 'X';
		}
	}

    class WaterObject : NonPlayerObject
    {
        public override Direction Move(Direction playerMovement)
        {
            return new Direction(0, 0);
        }

        public override char DrawGlyph()
        {
            return 'W';
        }
    }
    
    class Fir : NonPlayerObject
	{
		public override Direction Move (Direction playerMovement)
		{
			return new Direction (0, 0);
		}
		
		public override char DrawGlyph ()
		{
			return 'T';
		}
	}    
    
    class Fence : NonPlayerObject
	{
		public override Direction Move (Direction playerMovement)
		{
			return new Direction (0, 0);
		}
		
		public override char DrawGlyph ()
		{
			return 'F';
		}
	}
}

