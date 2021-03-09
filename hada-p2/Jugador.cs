using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Jugador
    {
        public static int maxAmonestaciones { get; set; }
        public static int maxFaltas { get; set; }
        public static int minEnergia { get; set; }
        public static Random rand { private get; set; }
        public string nombre { get; private set; }
        public int puntos { get; set; }
        private int amonestaciones 
        {
            get { return amonestaciones; }
            set 
            { 
                if (value > maxFaltas) ; //lanzar evento amonestacionesMaximoExcedido
                else if (value < 0) amonestaciones = 0;
            }
        }
        private int faltas 
        { 
            get { return faltas; } 
            set { if (value > maxFaltas) /*lanzar evento faltasMaximoExcedido*/; } 
        }
        private int energia 
        { 
            get { return energia; } 
            set
            {
                if (value < minEnergia) /*lanzar evento energiaMinimaExcedida*/;
                if (value > 100) energia = 100;
                else if (value < 0) energia = 0;
            }
        } 

        static void Main(string[] args)
        {
        }
    }
}
