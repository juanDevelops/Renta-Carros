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
        public string Placas {  get; set; }

        [BsonElement("color")]
        public string Color { get; set; }

        [BsonElement("precio")]
        public string Precio { get; set; }
    }

    public class CarrosViewModel
    {
        public ObservableCollection<Carros> CarrosCollection { get; set; }

        public CarrosViewModel() 
        {
            db db = new db();
            List<Carros> documents = db.ObtenerCarros();
            this.CarrosCollection = new ObservableCollection<Carros>();

            foreach (var document in documents)
            {
                //var imageSource = ImageSource.FromStream(() => new MemoryStream(document.Imagen.AsByteArray));
                //var image = new Image
                //{
                //    Source = imageSource,
                //    HeightRequest = 100,
                //    WidthRequest = 100
                //};

                this.CarrosCollection.Add(new Carros() { Imagen= document.Imagen, Marca=document.Marca, Modelo= document.Modelo, Año= document.Año, Color= document.Color, Placas= document.Placas, Precio= document.Precio });
            }
        }
    }
}
