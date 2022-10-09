using System.Collections.Generic;


namespace Tetris
{
    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; } // 2D Array for the blocks.
        protected abstract Position StartOffset { get; } // Starting position for the block.

        public abstract int Id { get; } // ID for each block.

        private int rotationState; // Current rotation state.
        private Position offset; // Offset position.

        // Constructor that makes the offset equal to the starting offset for row and column.
        public Block()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
        }

        public IEnumerable<Position> TilePositions()
        {
            foreach (Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        public void RotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        public void RotateCCW()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }

        public void Move(int rows, int columns)
        {
            offset.Row += rows;
            offset.Column += columns;
        }

        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    }
}
