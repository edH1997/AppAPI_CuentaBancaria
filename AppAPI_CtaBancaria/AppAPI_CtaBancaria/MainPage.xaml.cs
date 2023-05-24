using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppAPI_CtaBancaria
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private string Url = "https://cuentas2.azurewebsites.net/api";
        private Models.Cuentas_Bancarias[] Ctabancarias { get; set; }
        
        private void Btn_Leer(object sender, EventArgs e)
        {
            using (var wc = new WebClient()){
                wc.Headers.Add("Content-Type", "Application/json");
                var json = wc.DownloadString(Url + "/cuentab/" + textId.Text);
                var Ctabancaria = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Cuentas_Bancarias>(json);

                if (Ctabancaria != null) {
                    textId.Text = Ctabancaria.idCB;
                    textNombre.Text = Ctabancaria.nombreCB;
                    textDescripcion.Text = Ctabancaria.descripcionCB;
                    textEntidad.Text = Ctabancaria.entidadCB;
                    textEstado.Text = Ctabancaria.estadoCB;

                }
                else
                {
                    textId.Text ="";
                    textNombre.Text = "";
                    textDescripcion.Text = "";
                    textEntidad.Text = "";
                    textEstado.Text = "";
                }
                

            }

        }
        private void Btn_Insert(object sender, EventArgs e)
        {
            using (var wc = new WebClient()) {
                wc.Headers.Add("Content-Type", "Application/json");
                var datos = new Models.Cuentas_Bancarias
                {
                    idCB = textId.Text,
                    nombreCB = textNombre.Text,
                    entidadCB = textEntidad.Text,
                    descripcionCB = textDescripcion.Text,
                    estadoCB = textEstado.Text
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(datos);
                wc.UploadString(Url+ "/cuentab", "POST", json);
                lblDatos.IsVisible = true;
                lblDatos.Text = "Cuenta bancaria registrada con éxito!!";
            }

        }
        private void Btn_Update(object sender, EventArgs e)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "Application/json");
                var datos = new Models.Cuentas_Bancarias
                {
                    idCB = textId.Text,
                    nombreCB = textNombre.Text,
                    entidadCB = textEntidad.Text,
                    descripcionCB = textDescripcion.Text,
                    estadoCB = textEstado.Text
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(datos);
                wc.UploadString(Url + "/cuentab/" + textId.Text, "PUT", json);
                lblDatos.IsVisible = true;
                lblDatos.Text = "Cuenta bancaria actualizada con éxito!!";
            }

            }
            private void Btn_Delete(object sender, EventArgs e)
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "Application/json");
                wc.UploadString(Url + "/cuentab/" + textId.Text, "DELETE", "");
                lblDatos.IsVisible = true;
                lblDatos.Text = "Cuenta bancaria eliminada con éxito!!";
                textId.Text = "";
                textNombre.Text = "";
                textDescripcion.Text = "";
                textEntidad.Text = "";
                textEstado.Text = "";

            }
        }
    }
}
