using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlPdfCelta
{
    class FormatStringFactura
    {
        public static string dateTimeStringToFormat(string date)
        {
            date = date.Replace("/", "-");
            string[] newDate = date.Split(' ')[0].Split('-');
            return newDate[2] + "/" + newDate[1] + "/" + newDate[0];
        }


        public static string stringToPesos(string valor)
        {

           
            if ((Object.ReferenceEquals(null, valor))||valor.Equals("0")||valor.Equals("")) {
                return "";
            }
            string retorno = "";


            string valorIngresado = valor.Replace(".", ",");

            char[] caracteres = valorIngresado.ToCharArray();
            int posicionComa = caracteres.Length;

            if (valorIngresado.Contains(','))
            {
                for (int i = 0; i < caracteres.Length; i++)
                {
                    if (caracteres[i] == ',') posicionComa = i;
                }
            }

            for (int i = posicionComa; i < caracteres.Length; i++)
            {
                retorno += caracteres[i];
            }

            for (int i = posicionComa - 1; i >= 0; i--)
            {
                if (i == posicionComa - 3)
                {
                    retorno = "." + caracteres[i] + retorno;
                }
                else if (i == posicionComa - 6)
                {
                    retorno = "." + caracteres[i] + retorno;
                }
                else if (i == posicionComa - 9)
                {
                    retorno = "." + caracteres[i] + retorno;
                }
                else if (i == posicionComa - 12)
                {
                    retorno = "." + caracteres[i] + retorno;
                }
                else if (i == posicionComa - 15)
                {
                    retorno = "." + caracteres[i] + retorno;
                }
                else if (i == posicionComa - 18)
                {
                    retorno = "." + caracteres[i] + retorno;
                }
                else if (i == posicionComa - 21)
                {
                    retorno = "." + caracteres[i] + retorno;
                }
                else
                {
                    retorno = caracteres[i] + retorno;
                }
            }

            if (retorno.StartsWith(".")) retorno = retorno.TrimStart('.');
            else if (retorno.StartsWith("-."))
            {
                retorno = retorno.Replace("-.", "-");
            }

            return "$"+retorno;
        }


        public static string numberToWord(string numero)
        {
            string resultado = "";
            bool negativo = false;
            //if (numero < 0) negativo = true;

            char[] numeroChar = numero.ToString().Replace("-", "").ToCharArray();

            int division3 = numeroChar.Length / 3;

            if (division3 * 3 == numeroChar.Length) division3--;

            while (division3 >= 0)
            {
                int comienzo = numeroChar.Length - 3 * (division3 + 1);
                int final = numeroChar.Length - 3 * division3;
                if (comienzo < 0) comienzo = 0;

                string num = "";
                for (int i = comienzo; i < final; i++)
                {
                    num = num + numeroChar[i];
                }

                string lectura = retornarValorMenorAMilComoString(num);

                if (division3 == 4)
                {
                    if (lectura == "uno ") lectura = "un billón";
                    else
                    {
                        lectura += "billones ";
                    }
                }
                else if (division3 == 3)
                {
                    if (lectura == "uno ") lectura = "un mil millones ";
                    else
                    {
                        lectura += "miles de millones ";
                    }
                }
                else if (division3 == 2)
                {
                    if (lectura == "uno ") lectura = "un millón ";
                    else
                    {
                        lectura += "millones ";
                    }
                }
                else if (division3 == 1)
                {
                    if (lectura == "uno ") lectura = "un mil ";
                    else
                    {
                        lectura += "mil ";
                    }
                }
                resultado += lectura;

                division3--;
            }
            if (negativo) resultado = "-" + resultado;

            return resultado.ToUpper();
        }

        static string retornarValorMenorAMilComoString(string numero)
        {
            string resultado = "";
            char[] numeros = new char[3];

            if (numero.Length == 3)
                numeros = numero.ToCharArray();
            else if (numero.Length == 2)
            {
                numeros[0] = '0';
                numeros[1] = numero.ToCharArray()[0];
                numeros[2] = numero.ToCharArray()[1];
            }
            else if (numero.Length == 1)
            {
                numeros[0] = '0';
                numeros[1] = '0';
                numeros[2] = numero.ToCharArray()[0];
            }
            else return resultado;

            if (numeros[0] == '1')
            {
                if (numero[1] != '0' || numero[2] != '0') resultado += "ciento ";
                else resultado += "cien ";
            }
            else if (numeros[0] == '2')
            {
                resultado += "doscientos ";
            }
            else if (numeros[0] == '3')
            {
                resultado += "trescientos ";
            }
            else if (numeros[0] == '4')
            {
                resultado += "cuatrocientos ";
            }
            else if (numeros[0] == '5')
            {
                resultado += "quinientos ";
            }
            else if (numeros[0] == '6')
            {
                resultado += "seiscientos ";
            }
            else if (numeros[0] == '7')
            {
                resultado += "setecientos ";
            }
            else if (numeros[0] == '8')
            {
                resultado += "ochocientos ";
            }
            else if (numeros[0] == '9')
            {
                resultado += "novecientos ";
            }

            string ultimosDos = numeros[1] + "" + numeros[2];
            if (int.Parse(ultimosDos) > 9 && int.Parse(ultimosDos) < 30)
            {
                if (ultimosDos == "10")
                {
                    resultado += "diez ";
                }
                else if (ultimosDos == "11")
                {
                    resultado += "once ";
                }
                else if (ultimosDos == "12")
                {
                    resultado += "doce ";
                }
                else if (ultimosDos == "13")
                {
                    resultado += "trece ";
                }
                else if (ultimosDos == "14")
                {
                    resultado += "catorce ";
                }
                else if (ultimosDos == "15")
                {
                    resultado += "quince ";
                }
                else if (ultimosDos == "16")
                {
                    resultado += "dieciséis ";
                }
                else if (ultimosDos == "17")
                {
                    resultado += "diecisiete ";
                }
                else if (ultimosDos == "18")
                {
                    resultado += "dieciocho ";
                }
                else if (ultimosDos == "19")
                {
                    resultado += "diecinueve ";
                }
                else if (ultimosDos == "20")
                {
                    resultado += "veinte ";
                }
                else if (ultimosDos == "21")
                {
                    resultado += "veintiuno ";
                }
                else if (ultimosDos == "22")
                {
                    resultado += "veintidos ";
                }
                else if (ultimosDos == "23")
                {
                    resultado += "veintitres ";
                }
                else if (ultimosDos == "24")
                {
                    resultado += "veinticuatro ";
                }
                else if (ultimosDos == "25")
                {
                    resultado += "veinticinco ";
                }
                else if (ultimosDos == "26")
                {
                    resultado += "veintiséis ";
                }
                else if (ultimosDos == "27")
                {
                    resultado += "veintisiete ";
                }
                else if (ultimosDos == "28")
                {
                    resultado += "veintiocho ";
                }
                else if (ultimosDos == "29")
                {
                    resultado += "veintinueve ";
                }
            }
            else
            {
                if (numeros[1] == '3')
                {
                    resultado += "treinta ";
                    if (numeros[2] != '0') resultado += "y ";
                }
                else if (numeros[1] == '4')
                {
                    resultado += "cuarenta ";
                    if (numeros[2] != '0') resultado += "y ";
                }
                else if (numeros[1] == '5')
                {
                    resultado += "cincuenta ";
                    if (numeros[2] != '0') resultado += "y ";
                }
                else if (numeros[1] == '6')
                {
                    resultado += "sesenta ";
                    if (numeros[2] != '0') resultado += "y ";
                }
                else if (numeros[1] == '7')
                {
                    resultado += "setenta ";
                    if (numeros[2] != '0') resultado += "y ";
                }
                else if (numeros[1] == '8')
                {
                    resultado += "ochenta ";
                    if (numeros[2] != '0') resultado += "y ";
                }
                else if (numeros[1] == '9')
                {
                    resultado += "noventa ";
                    if (numeros[2] != '0') resultado += "y ";
                }

                if (numeros[2] == '1')
                {
                    resultado += "uno ";
                }
                if (numeros[2] == '2')
                {
                    resultado += "dos ";
                }
                if (numeros[2] == '3')
                {
                    resultado += "tres ";
                }
                if (numeros[2] == '4')
                {
                    resultado += "cuatro ";
                }
                if (numeros[2] == '5')
                {
                    resultado += "cinco ";
                }
                if (numeros[2] == '6')
                {
                    resultado += "seis ";
                }
                if (numeros[2] == '7')
                {
                    resultado += "siete ";
                }
                if (numeros[2] == '8')
                {
                    resultado += "ocho ";
                }
                if (numeros[2] == '9')
                {
                    resultado += "nueve ";
                }
            }

            return resultado;
        }

        public static string ivaNewFormat(string iva) {
            iva = iva + ",00%";
            return iva;
        }
    }
}
