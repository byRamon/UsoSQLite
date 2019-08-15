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
    class Editorial
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [NotNull]
        public string nombreEditorial { get; set; }

        [NotNull, MaxLength(50)]
        public string pais { get; set; }

    }
}