using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace UsoSQLite.Modelo
{
    class Autor
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [NotNull]
        public string nombre1 { get; set; }
        public string nombre2 { get; set; }
        public string nombre3 { get; set; }
        [NotNull]
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }

        [ForeignKey(typeof(Libro))]
        public int LibroId { get; set; }

    }
}