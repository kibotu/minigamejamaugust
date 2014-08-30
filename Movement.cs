using System;

namespace sheepgame
{
	struct Direction
	{
		public int x, y;

		public Direction (int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		
		public static Direction MoveNorth (int n)
		{
			return new Direction (0, -n);
		}
		
		public static Direction MoveEast (int n)
		{
			return new Direction (n, 0);
		}
		
		public static Direction MoveSouth (int n)
		{
			return new Direction (0, n);
		}
		
		public static Direction MoveWest (int n)
		{
			return new Direction (-n, 0);
		}
	}
	
	class Movement
	{
		private MapObject parentObject;
		private int oldX, oldY;
		private Direction direction;
		private bool invalid = false;
		
		public Movement (MapObject o, int x, int y, Direction direction)
		{
			this.parentObject = o;
			this.oldX = x;
			this.oldY = y;
			this.direction = direction;
		}

		public MapObject ParentObject ()
		{
			return this.parentObject;
		}
		
		public int NewX ()
		{
			return this.oldX + direction.x;
		}
		
		public int NewY ()
		{
			return this.oldY + direction.y;
		}
		
		public int OldX ()
		{
			return this.oldX;
		}
		
		public int OldY ()
		{
			return this.oldY;
		}
		
		public void SetInvalid ()
		{
			this.invalid = true;
		}
		
		public bool Invalid ()
		{
			return this.invalid;
		}

		public bool Conflicts (Movement other)
		{
			
			// if the objects are moving into eachother, cancel both
			if (this.NewX () == other.NewX () && this.NewY () == other.NewY ())
				return true;
			
			// if the two objects swapped places, cancel both
			if (this.NewX () == other.OldX () && other.NewX () == this.OldX ()
				&& this.NewY () == other.OldY () && other.NewY () == this.OldY ())
				return true;

			return false;
		}

		public override string ToString ()
		{
			return "from " + this.OldX () + ", " + this.OldY () + " to " + this.NewX () + ", " + this.NewY ();
		}
	}
}

