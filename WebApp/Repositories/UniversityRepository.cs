﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Contexts;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class UniversityRepository
    {

        public MySqlContext Conn { get; set; }

        public UniversityRepository(MySqlContext dbConn)
        {
            Conn = dbConn;
        }

        public IEnumerable<University> GetAllUniversities()
        {
            return Conn.University.ToList();
        }

    }
}
