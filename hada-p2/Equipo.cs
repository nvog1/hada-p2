using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    /// <summary>
    /// Esta clase contiene un equipo formado por jugadores de la clase Hada.Jugador
    /// </summary>
    class Equipo
    {
        public static int minJugadores { get; set; }
        public static int maxNumeroMovimientos { get; set; }
        public int movimientos { get; private set; }
        public string nombreEquipo { get; private set; }
        private List<Jugador> jugadores { get; set; }
        private List<Jugador> jugadoresExpulsados { get; set; }
        private List<Jugador> jugadoresLesionados { get; set; }
        private List<Jugador> jugadoresRetirados { get; set; }

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="nj">Número de jugadores del equipo.</param>
        /// <param name="nom">Nombre del equipo.</param>
        public Equipo(int nj, string nom)
        {
            nombreEquipo = nom;
            jugadores = new List<Jugador>();
            jugadoresExpulsados = new List<Jugador>();
            jugadoresLesionados = new List<Jugador>();
            jugadoresRetirados = new List<Jugador>();

            for(int i = 0; i < nj; i++)
            {
                Jugador jugadorAux = new Jugador("Jugador_" + i.ToString(), 0, 0, 50, 0);
                jugadorAux.amonestacionesMaximoExcedido += cuandoAmonestacionesMaximoExcedido;
                jugadorAux.energiaMinimaExcedida += cuandoEnergiaMinimaExcedida;
                jugadorAux.faltasMaximoExcedido += cuandoFaltasMaximoExcedido;
                jugadores.Add(jugadorAux);
            }
        }

        /// <summary>
        /// Mueve los jugadores que aún se puedan mover.
        /// </summary>
        /// <returns>True si después de haber movido aún quedan como mínimo minJugadores disponibles. False en caso contrario.</returns>
        public bool moverJugadores()
        {
            int jugadoresOk = 0; //jugadores que aun pueden jugar despues de este movimiento
            
            for (int i = 0; i < jugadores.Count(); i++)
            {
                if (jugadores[i].todoOk())
                {
                    jugadores[i].mover();
                    if (jugadores[i].todoOk()) jugadoresOk++;
                }
            }

            movimientos++;

            if (jugadoresOk >= minJugadores)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Mueve los jugadores mientras se puedan mover.
        /// </summary>
        public void moverJugadoresEnBucle()
        {
            while (moverJugadores()) ;
        }

        /// <summary>
        /// Suma los puntos del equipo.
        /// </summary>
        /// <returns>Int con la suma de los puntos de todos los jugadores.</returns>
        public int sumarPuntos()
        {
            int sumaPuntos = 0;

            for (int i = 0; i < jugadores.Count(); i++)
            {
                sumaPuntos += jugadores[i].puntos;
            }

            return sumaPuntos;
        }

        public List<Jugador> getJugadoresExcedenLimiteAmonestaciones()
        {
            return jugadoresExpulsados;
        }

        public List<Jugador> getJugadoresExcedenLimiteFaltas()
        {
            return jugadoresLesionados;
        }

        public List<Jugador> getJugadoresExcedenMinimoEnergia()
        {
            return jugadoresRetirados;
        }

        public override string ToString()
        {
            string s = "";
            s += $"[{nombreEquipo}] Puntos: {sumarPuntos()}; Expulsados: {jugadoresExpulsados.Count()}; Lesionados: {jugadoresLesionados.Count()}; Retirados: {jugadoresRetirados.Count()}\n";
            
            for(int i = 0; i < jugadores.Count(); i++)
            {
                s += jugadores[i].ToString() + "\n";
            }

            return s;
        }

        /// <summary>
        /// Subscriber del evento AmonestacionesMaximoExcedido de la clase Hada.Jugador
        /// </summary>
        /// <param name="sender">Jugador que ha sido expulsado</param>
        /// <param name="args">Numero de amonestaciones del jugador</param>
        private void cuandoAmonestacionesMaximoExcedido(object sender, AmonestacionesMaximoExcedidoArgs args)
        {
            Jugador j = (Hada.Jugador)sender;
            jugadoresExpulsados.Add(j);
            Console.WriteLine("¡¡Número máximo excedido de amonestaciones. Jugador expulsado!!");
            Console.WriteLine("Jugador: " + j.nombre);
            Console.WriteLine("Equipo: " + nombreEquipo);
            Console.WriteLine("Amonestaciones: " + args.amonestaciones.ToString());
        }

        /// <summary>
        /// Subscriber del evento FaltasMaximoExcedido
        /// </summary>
        /// <param name="sender">Jugador que se ha lesionado</param>
        /// <param name="args">Numero de faltas recibidas por el jugador.</param>
        private void cuandoFaltasMaximoExcedido(object sender, FaltasMaximoExcedidoArgs args)
        {
            Jugador j = (Hada.Jugador)sender;
            jugadoresLesionados.Add(j);
            Console.WriteLine("¡¡Número máximo excedido de faltas recibidas. Jugador lesionado!!");
            Console.WriteLine("Jugador: " + j.nombre);
            Console.WriteLine("Equipo: " + nombreEquipo);
            Console.WriteLine("Faltas: " + args.faltas);
        }

        /// <summary>
        /// Subscriber del evento EnergiaMinimaExcedida
        /// </summary>
        /// <param name="sender">Jugador que se ha cansado</param>
        /// <param name="args">Cantidad de energía restante del jugador.</param>
        private void cuandoEnergiaMinimaExcedida(object sender, EnergiaMinimaExcedidaArgs args)
        {
            Jugador j = (Hada.Jugador)sender;
            jugadoresRetirados.Add(j);
            Console.WriteLine("¡¡Energía mínima excedida. Jugador retirado!!");
            Console.WriteLine("Jugador: " + j.nombre);
            Console.WriteLine("Equipo: " + nombreEquipo);
            Console.WriteLine("Energía: " + args.energia.ToString() + " %");
        } 


    }
}
