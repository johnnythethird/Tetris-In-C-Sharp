using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    // The Game Grid is used as the basis for our environment when playing tetris.
    internal class GameGrid
    {
        // Our grid with the read only parameter
        private readonly int[,] grid;
        
        // Rows Variable
        public int Rows { get; }

        // Columns Variable
        public int Columns { get; }

        // Indexer for access
        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        public GameGrid(int rows, int columns)
        {
            Rows = rows; // Get Row
            Columns = columns; // Get Col
            grid = new int[rows, columns]; // Initialize new Grid
        }

        // Check if a row/column exists inside the grid or not.
        public bool IsInside(int r, int c)
        {
            // Return true if all conditions are met.
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }

        // Check if the row/col cell is empty.
        public bool IsEmpty(int r, int c)
        {
            // Return true if all conditions are met.
            return IsInside(r, c) && grid[r, c] == 0;
        }

        // Check all the columns in the row and check if they are full.
        public bool IsRowFull(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        // Check all the columns in the row and check if they are empty.
        public bool IsRowEmpty(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private void ClearRow(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r, c] = 0;
            }
        }

        private void MoveRowDown(int r, int numRows)
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r + numRows, c] = grid[r, c]; // Move the current row down one.
                grid[r, c] = 0; // Make the current row all zeros.
            }
        }

        // This method will clear all of the full rows currently in the grid.
        public int ClearFullRows()
        {
            int cleared = 0;

            for (int r = Rows-1; r >= 0; r--) // Check every row for a full set of blocks.
            {
                if (IsRowFull(r)) // If return true, 
                {
                    ClearRow(r); // Clear the row.
                    cleared++; // Add number of cleared rows.
                }
                else if (cleared > 0) // If we cleared a row, then move on to here if previous didn't apply.
                {
                    MoveRowDown(r, cleared);
                }
            }

            return cleared;
        }
    }
}
