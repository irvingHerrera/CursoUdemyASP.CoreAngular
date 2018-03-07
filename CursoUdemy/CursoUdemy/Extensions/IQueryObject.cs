﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoUdemy.Extensions
{
    public interface IQueryObject
    {
        string SortBy { get; set; }

        bool IsSortAscending { get; set; }
    }
}
