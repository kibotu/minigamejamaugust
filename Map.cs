using System;
using System.Collections.Generic;

namespace sheepgame
{
	class Tile
	{
		public int x;
		public int y;
		public MapObject contains;
		
		public Tile (int x, int y)
		{
			this.x = x;
			this.y = y;
			this.contains = null;
		}
	}

	class Map
	{
		public int width;
		public int height;
		public Tile[,] grid;
		public List<Tile> occupiedTiles;
		public List<Movement> currentMovements;
		
		public Map (int width, int height)
		{
			this.width = width;
			this.height = height;
			
			this.grid = new Tile[width, height];
			for (int x = 0; x < this.grid.GetLength(0); x++) {
				for (int y = 0; y < this.grid.GetLength(1); y++) {
					this.grid [x, y] = new Tile (x, y);
				}
			}

			this.occupiedTiles = new List<Tile> ();
		}

		public void ClearGrid ()
		{
			for (int x = 0; x < this.grid.GetLength(0); x++) {
				for (int y = 0; y < this.grid.GetLength(1); y++) {
					this.grid [x, y].contains = null;
				}
			}

			this.occupiedTiles.Clear ();
		}

		public void AddObject (int x, int y, MapObject mo)
		{
			Tile t = this.grid [x, y];
			
			t.contains = mo;
			this.occupiedTiles.Add (t);
		}

		public void StartMovement (Direction playerDirection)
		{
			// the player movement is special; if it fails, everything fails.
			Movement playerMovement;

			List<Movement> movements = new List<Movement> ();
			foreach (Tile t in this.occupiedTiles) {
				Movement m = new Movement (t.contains, t.x, t.y, t.contains.Move (playerDirection));
				movements.Add (m);

				if (m.ParentObject ().IsCharacter ())
					playerMovement = m;
			}

			foreach (Movement m1 in movements) {
				// if we already marked it invalid, skip
				if (m1.Invalid ())
					continue;

				if (m1.NewX () < 0 || m1.NewX () >= this.width 
					|| m1.NewY () < 0 || m1.NewY () >= this.height) {
					m1.SetInvalid ();
					continue;
				}

				foreach (Movement m2 in movements) {
					if (m1 == m2)
						continue;
					if (m1.Conflicts (m2)) {
						m1.SetInvalid ();
						m2.SetInvalid ();
						break;
					}
				}
			}

			this.currentMovements = movements;
		}

		public void FinishMovement()
		{
			// apply the moves
			this.ClearGrid ();
			foreach (Movement m in movements) {
				MapObject o = m.ParentObject ();
				if (m.Invalid () || playerMovement.Invalid ()) {
					this.AddObject (m.OldX (), m.OldY (), o);
				} else {
					this.AddObject (m.NewX (), m.NewY (), o);
				}
			}

			this.currentMovements = null;
		}
	}
}

