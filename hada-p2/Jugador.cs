using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class AmonestacionesMaximoExcedidoArgs
    {
        private int _amonestaciones;
        public int amonestaciones
        {
            get { return _amonestaciones; }
            set
            {
                _amonestaciones = value;
            }
        }
        
        public AmonestacionesMaximoExcedidoArgs(int amonestaciones)
        {
            this.amonestaciones = amonestaciones;
        }
    }

    class FaltasMaximoExcedidoArgs
    {
        private int _faltas;
        public int faltas
        {
            get { return _faltas; }
            set
            {
                _faltas = value;
            }
        }

        public FaltasMaximoExcedidoArgs(int faltas)
        {
            this.faltas = faltas;
        }
    }

    class EnergiaMinimaExcedidaArgs
    {
        private int _energia;
        public int energia
        {
            get { return _energia; }
            set
            {
                _energia = value;
            }
        }

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
        public int amonestaciones
        {
            get { return _amonestaciones; }
            set
            {
                if (value > maxFaltas) ; //lanzar evento amonestacionesMaximoExcedido
                else if (value < 0) _amonestaciones = 0;
            }
        }

        private int _faltas;
        public int faltas
        {
            get { return _faltas; }
            set { if (value > maxFaltas) /*lanzar evento faltasMaximoExcedido*/; }
        }

        private int _energia;
        public int energia
        {
            get { return _energia; }
            set
            {
                if (value < minEnergia) /*lanzar evento energiaMinimaExcedida*/;
                if (value > 100) _energia = 100;
                else if (value < 0) _energia = 0;
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
            return $"[{nombre}] Puntos: {puntos}; Amonestaciones: {amonestaciones}; Faltas: {faltas}; Energía: {energia} %; Ok: {todoOk()}";
        }

        public event EventHandler<AmonestacionesMaximoExcedidoArgs> amonestacionesMaximoExcedido
        {

        }

        public event EventHandler<FaltasMaximoExcedidoArgs> faltasMaximoExcedido
        {

        }

        public event EventHandler<EnergiaMinimaExcedidaArgs> energiaMinimaExcedida
        {

        }

    }
}
