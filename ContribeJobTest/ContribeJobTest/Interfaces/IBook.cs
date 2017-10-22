using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContribeJobTest.Interfaces
{
    public interface IBook
    {
        string Title { get; }
        string Author { get; }
        float Price { get; }
        int InStock { get; }
    }
}
