using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Renta_Carros
{
    public class Carros
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        [BsonElement("imagen")]
        public BsonBinaryData Imagen { get; set; }

        [BsonElement("marca")]
        public string Marca { get; set; }

        [BsonElement("modelo")]
        public string Modelo { get; set; }

        [BsonElement("año")]
        public string Año { get; set; }

        [BsonElement("placas")]
        public string Placas { get; set; }

        [BsonElement("color")]
        public string Color { get; set; }

        [BsonElement("precio")]
        public string Precio { get; set; }

        // New property to indicate rental status
        [BsonElement("enRenta")]
        public bool EnRenta { get; set; }
    }


    public class CarrosViewModel : ObservableCollection<Carros>
    {
        public ObservableCollection<Carros> CarrosCollection { get; set; }

        public CarrosViewModel()
        {
            var tabbedPage = Application.Current.MainPage as Menu;
            Menu menu = tabbedPage as Menu;
            try
            {
                dbMethods db = new dbMethods(tabbedPage.ipv4);
                List<Carros> documents = db.ObtenerCarrosDisponibles();

                CarrosCollection = new ObservableCollection<Carros>();

                foreach (var document in documents)
                {
                    CarrosCollection.Add(new Carros() { Imagen = document.Imagen, Marca = document.Marca, Modelo = document.Modelo, Año = document.Año, Color = document.Color, Placas = document.Placas, Precio = document.Precio });
                }
            }
            catch (Exception)
            {

            }
            
        }
    }
}
