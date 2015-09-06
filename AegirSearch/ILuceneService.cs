﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirSearch
{
    public interface ILuceneService
    {
        IEnumerable<ISearchIndexable> Search(string searchTerm);
    }
}
