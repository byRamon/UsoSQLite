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
    class Libro
    {
        [AutoIncrement, PrimaryKey, Column("id")]
        public int id { get; set; }

        [ManyToOne("EditorialId")]
        public Editorial editorial { get; set; }

        [OneToMany]
        public List<Autor> autores { get; set; }

        [ForeignKey(typeof(Editorial))]
        public int EditorialId { get; set; }

        [MaxLength(100), NotNull]
        public string titulo { get; set; }
        
        [MaxLength(70)]
        public string ciudad { get; set; }

        [MaxLength(70)]
        public string pais { get; set; }

        public int anio { get; set; }

        [Ignore]
        public double valor_extra { get; set; }        
    }
}