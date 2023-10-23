using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM2E10069.modelo
{
    public class Sitio
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public byte[] foto { get; set; }

        public double latitude
        {
            get; set;
        }

        public double longitude
        { get; set; }

        public String descripcion { get; set; }

        public bool IsShowingUser { get; set; }
    }
}
