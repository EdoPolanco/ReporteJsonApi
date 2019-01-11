using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reportes.Negocio
{
    public class Validador
    {
        public bool validarRut(string rut)
        {
            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                string rutAux = rut.Substring(0, rut.Length - 1);

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int suma = 0;
                int multiplicador = 2;
                for (int i = rutAux.Length - 1; i > -1; i--)
                {
                    suma += int.Parse(rutAux[i].ToString()) * multiplicador;
                    multiplicador = multiplicador == 7 ? 2 : multiplicador + 1;
                }

                char dvAux = '0';
                switch (11 - (suma % 11))
                {
                    case 10:
                        dvAux = 'k';
                        break;
                    case 11:
                        dvAux = '0';
                        break;
                    default:
                        dvAux = char.Parse((11 - suma % 11).ToString());
                        break;
                }

                validacion = dv.Equals(dvAux);
            }
            catch (Exception)
            {

            }

            return validacion;
        }
    }
}
