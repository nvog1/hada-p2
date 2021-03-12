using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    public class AmonestacionesMaximoExcedidoArgs : EventArgs
    {
        public int amonestaciones { get; set; }
        
        public AmonestacionesMaximoExcedidoArgs(int amonestaciones)
        {
            this.amonestaciones = amonestaciones;
        }
    }

    public class FaltasMaximoExcedidoArgs : EventArgs
    {
        public int faltas { get; set; }

        public FaltasMaximoExcedidoArgs(int faltas)
        {
            this.faltas = faltas;
        }
    }

    public class EnergiaMinimaExcedidaArgs: EventArgs
    {
        public int energia { get; set; }

        public EnergiaMinimaExcedidaArgs(int energia)
        {
            this.energia = energia;
        }
    }

    class Jugador
    {
        public static int maxAmonestaciones { get; set; }
        public static int maxFaltas { get; set; }
        public static int minEnergia { get; set; }
        public static Random rand { private get; set; }
        public string nombre { get; private set; }
        public int puntos { get; set; }

        private int _amonestaciones;
        private int amonestaciones
        {
            get { return _amonestaciones; }
            set
            {

                if (value > maxAmonestaciones && amonestacionesMaximoExcedido != null)
                {
                    amonestacionesMaximoExcedido(this, new AmonestacionesMaximoExcedidoArgs(value));
                }
                else if (value < 0) _amonestaciones = 0;
            }
        }

        private int _faltas;
        private int faltas
        {
            get { return _faltas; }
            set { if (value > maxFaltas && faltasMaximoExcedido != null)
                {
                    faltasMaximoExcedido(this, new FaltasMaximoExcedidoArgs(value));
                }
            }
        }

        private int _energia;
        private int energia
        {
            get { return _energia; }
            set
            {
                if (value > 100) _energia = 100;
                else if (value < 0) _energia = 0;

                if (value < minEnergia && energiaMinimaExcedida != null)
                {
                    energiaMinimaExcedida(this, new EnergiaMinimaExcedidaArgs(value));
                }
            }
        }

        public Jugador(string nombre, int amonestaciones, int faltas, int energia, int puntos)
        {
            this.nombre = nombre; 
            this.amonestaciones = amonestaciones; 
            this.faltas = faltas; this.energia = energia; 
            this.puntos = puntos;
        }

        public void incAmonestaciones()
        {
            amonestaciones += rand.Next(0, 3);
        }

        public void incFaltas()
        {
            faltas += rand.Next(0, 4);
        }

        public void decEnergia()
        {
            energia -= rand.Next(1, 8);
        }

        public void incPuntos()
        {
            puntos += rand.Next(0, 4);
        }

        public bool todoOk()
        {
            if (amonestaciones <= maxAmonestaciones && energia >= minEnergia && faltas <= maxFaltas) return true;
            return false;
        }

        public void mover()
        {
            if (todoOk())
            {
                incAmonestaciones();
                incFaltas();
                incPuntos();
                decEnergia();
            }
        }

        public override string ToString()
        {
            return $"[{nombre}] Puntos: {puntos}; Amonestaciones: {amonestaciones}; Faltas: {faltas}; Energía: {energia} %; Ok: {todoOk().ToString()}";
        }

        public event EventHandler<AmonestacionesMaximoExcedidoArgs> amonestacionesMaximoExcedido;

        public event EventHandler<FaltasMaximoExcedidoArgs> faltasMaximoExcedido;

        public event EventHandler<EnergiaMinimaExcedidaArgs> energiaMinimaExcedida;
    }
}
