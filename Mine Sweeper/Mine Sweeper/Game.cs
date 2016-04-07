using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mine_Sweeper
{
    public enum Difficulty : byte
    {
        Easy = 0,
        Normal = 1,
        Hard = 2,
    }

    public enum dCell : byte
    {
        None,
        Mine,
        
        // User Position
        Undiscovered,
        Discovered ,
        MineMarked,
        QuestionMarked,
    }
}