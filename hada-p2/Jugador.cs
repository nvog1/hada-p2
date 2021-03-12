using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    /// <summary>
    /// Clase usada para pasar las amonestaciones como argumento del evento AmonestacionesMaximoArgs
    /// </summary>
    public class AmonestacionesMaximoExcedidoArgs : EventArgs
    {
        public int amonestaciones { get; set; }
        
        public AmonestacionesMaximoExcedidoArgs(int amonestaciones)
        {
            this.amonestaciones = amonestaciones;
        }
    }

    /// <summary>
    /// Clase usada para pasar las faltas como argumento del evento FaltasMaximoArgs
    /// </summary>
    public class FaltasMaximoExcedidoArgs : EventArgs
    {
        public int faltas { get; set; }

        public FaltasMaximoExcedidoArgs(int faltas)
        {
            this.faltas = faltas;
        }
    }

    /// <summary>
    /// Clase usada para pasar la energia como argumento del evento EnergiaMinimaExcedida
    /// </summary>
    public class EnergiaMinimaExcedidaArgs: EventArgs
    {
        public int energia { get; set; }

        public EnergiaMinimaExcedidaArgs(int energia)
        {
            this.energia = energia;
        }
    }

    /// <summary>
    /// Jugadores del partido. Tienen nombre, amonestaciones, faltas recibidas, energia y puntos.
    /// </summary>
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
                if (value < 0) _amonestaciones = 0;
                _amonestaciones = value;
                if (value > maxAmonestaciones && amonestacionesMaximoExcedido != null)
                {
                    amonestacionesMaximoExcedido(this, new AmonestacionesMaximoExcedidoArgs(value));
                }
            }
        }

        private int _faltas;
        private int faltas
        {
            get { return _faltas; }
            set 
            {
                _faltas = value;
                if (value > maxFaltas && faltasMaximoExcedido != null)
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
                if (value < 0) _energia = 0;
                _energia = value;

                if (value < minEnergia && energiaMinimaExcedida != null)
                {
                    energiaMinimaExcedida(this, new EnergiaMinimaExcedidaArgs(value));
                }
            }
        }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="nombre">Nombre del jugador</param>
        /// <param name="amonestaciones">Amonestaciones del jugador</param>
        /// <param name="faltas">Faltas recibidas por el jugador</param>
        /// <param name="energia">Energía del jugador</param>
        /// <param name="puntos">Puntos del jugador</param>
        public Jugador(string nombre, int amonestaciones, int faltas, int energia, int puntos)
        {
            this.nombre = nombre; 
            this.amonestaciones = amonestaciones; 
            this.faltas = faltas;
            this.energia = energia; 
            this.puntos = puntos;
        }

        /// <summary>
        /// Incrementa entre 0 y 2 la cantidad de amonestaciones
        /// </summary>
        public void incAmonestaciones()
        {
            amonestaciones += rand.Next(0, 3);
        }

        /// <summary>
        /// Incrementa entre 0 y 3 el numero de faltas
        /// </summary>
        public void incFaltas()
        {
            faltas += rand.Next(0, 4);
        }

        /// <summary>
        /// Decrementa la energía entre 1 y 7 puntos
        /// </summary>
        public void decEnergia()
        {
            energia -= rand.Next(1, 8);
        }

        /// <summary>
        /// Incrementa entre 0 y 3 los puntos del jugador
        /// </summary>
        public void incPuntos()
        {
            puntos += rand.Next(0, 4);
        }

        /// <summary>
        /// Comprueba que el jugador puede seguir jugando
        /// </summary>
        /// <returns>True si no se ha retirado ni le han expulsado ni se ha lesionado. False en caso contrario.</returns>
        public bool todoOk()
        {
            if (amonestaciones <= maxAmonestaciones && energia >= minEnergia && faltas <= maxFaltas) return true;
            return false;
        }

        /// <summary>
        /// Mueve el jugador. LLama a incAmonestaciones(), incFaltas(), incPuntos() y decEnergia().
        /// </summary>
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
