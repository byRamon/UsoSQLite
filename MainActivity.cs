using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using SQLite;
using System.IO;
using UsoSQLite.Modelo;
using System.Collections.Generic;
using SQLiteNetExtensions.Extensions;

namespace UsoSQLiteChat
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            string pathBaseDeDatos = Path.Combine(FilesDir.AbsolutePath, "libros.sqlite");
            File.Delete(pathBaseDeDatos);

            using (var connection = new SQLiteConnection(pathBaseDeDatos))
            {
                connection.CreateTable<Libro>();
                connection.CreateTable<Autor>();
                connection.CreateTable<Editorial>();

                if (connection.Table<Autor>().Count() == 0)
                {
                    List<Autor> autores = new List<Autor> {
                        new Autor {
                            nombre1 = "Luis",
                            nombre2 = "Moises",
                            nombre3 = null,
                            apellidoPaterno = "Burgara",
                            apellidoMaterno = "Lopez" },
                        new Autor { nombre1 = "Luis",
                            nombre2 = null,
                            nombre3 = null,
                            apellidoPaterno = "Leithold",
                            apellidoMaterno = null }
                    };
                    connection.InsertAll(autores);
                }

                if (connection.Table<Editorial>().Count() == 0)
                {
                    List<Editorial> editoriales = new List<Editorial> {
                        new Editorial {
                            nombreEditorial = "Springer",
                            pais = "USA" },new Editorial {
                            nombreEditorial = "Limusa",
                            pais = "Mexico" },
                    };

                    connection.InsertAll(editoriales);
                }

                var editorialess = connection.Table<Editorial>().Where(r => r.nombreEditorial == "Springer").ToList()[0];
                var autoress = connection.Table<Autor>().Where(r => r.apellidoPaterno == "Burgara").ToList()[0];

                if (connection.Table<Libro>().Count() == 0)
                {
                    List<Libro> libros = new List<Libro> {
                    new Libro {
                        titulo = "Calculo",
                        EditorialId = connection.Table<Editorial>().Where(r=>r.nombreEditorial=="Springer").ToList()[0].id,
                        editorial = connection.Table<Editorial>().Where(r=>r.nombreEditorial=="Springer").ToList()[0],
                        ciudad = "New York",
                        pais = "USA",
                        anio = 1999,
                        autores = new List<Autor>{connection.Table<Autor>().Where(r=>r.apellidoPaterno=="Leithold").ToList()[0]}
                    },
                    new Libro {
                        titulo = "Algebra",
                        EditorialId = connection.Table<Editorial>().Where(r=>r.nombreEditorial=="Limusa").ToList()[0].id,
                        editorial = connection.Table<Editorial>().Where(r=>r.nombreEditorial=="Limusa").ToList()[0],
                        ciudad = "Ciudad de Mexico",
                        pais = "Mexico",
                        anio = 1984,
                        autores = new List<Autor>{connection.Table<Autor>().Where(r=>r.apellidoPaterno=="Burgara").ToList()[0]}
                    }
                };
                    connection.InsertAll(libros);
                    libros[0].autores = new List<Autor> { connection.Table<Autor>().Where(r => r.apellidoPaterno == "Leithold").ToList()[0] };
                    connection.UpdateWithChildren(libros[0]);

                    libros[1].autores = new List<Autor> { connection.Table<Autor>().Where(r => r.apellidoPaterno == "Burgara").ToList()[0] };
                    connection.UpdateWithChildren(libros[1]);
                }
                List<Libro> librosTodos = connection.GetAllWithChildren<Libro>();

                var editoriale = connection.Table<Editorial>().Where(r => r.nombreEditorial == "Springer").ToList()[0];
                librosTodos[0].editorial = editoriale;
                connection.Update(librosTodos[0]);
                librosTodos = connection.Table<Libro>().ToList();
                librosTodos = connection.GetAllWithChildren<Libro>();
                connection.Close();
                connection.Dispose();

                string books = "";
                foreach (Libro libro in librosTodos)
                {
                    string linea = "";
                    foreach(Autor autor in libro.autores)
                    {
                        linea += (linea.Length < 1 ? "" : ", ") + autor.apellidoPaterno + " " + autor.apellidoMaterno + " " + autor.nombre1;
                    }
                    linea += ". (" + libro.anio  +")." + libro.titulo + ". " + libro.pais + ", " + libro.ciudad + ": Editorial " + libro.editorial.nombreEditorial;
                    books += linea + "\n";
                }

                EditText txtFiles = FindViewById<EditText>(Resource.Id.etBooks);
                txtFiles.Text = books;
                //autor. (año). titulo. lugar: editoria.
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}