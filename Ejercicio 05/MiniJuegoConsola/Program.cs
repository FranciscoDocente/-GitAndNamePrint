using System;
using System.Diagnostics; // Necesario para ejecutar comandos en la consola

namespace MiniJuegoConsola
{
    class Program
    {
        // Atributos globlaes del jugador
        static string player = "";
        static int fuerza = 10;
        static int fuerzaMaxima = 10;
        static int resistencia = 10;
        static int resistenciaMaxima = 10;
        static int salud = 100;
        static int saludMaxima = 100;
        static int munición = 100;
        static int municiónMaxima = 100;
        static int numeroBotiquines = 2;
        static int numeroBotiquinesMaximo = 2;
        static ConsoleColor colorSable = ConsoleColor.White;

        // Atributos globales partida e historia
        static bool partidaIniciada = false;
        static int eleccionesMaximas = 0;
        static int registro = 0;
        static int numeroEleccion = 0;
        static ConsoleColor colorNotificacion = ConsoleColor.DarkGray;
        static ConsoleColor colorNarracion = ConsoleColor.DarkYellow;
        static ConsoleColor colorOpciones = ConsoleColor.DarkCyan;
        static ConsoleColor colorErrores = ConsoleColor.DarkRed;
        static ConsoleColor colorDefecto = ConsoleColor.White;

        static void Main(string[] args)
        {
            Console.Title = "Star Wars: El Destino del Padawan";
            Intro();

            do
            {
                ResetearEstadisticas();
                EscenaInicial();
            } while (PreguntarReinicio());
            Despedida();


        }

        private static void Intro()
        {
            Console.Clear();
            Console.ForegroundColor = colorDefecto;
            EscribirCentrado("Bienvenido a Star Wars: El Destino del Padawan");
            EscribirCentrado("¿Cómo te llamas, joven padawan?");
            player = Console.ReadLine()?.Trim() is { Length: > 0 } input ? input : "Narf";
            Console.Clear();
            EscribirCentrado($"Bien joven {player}, tu historia comienza ahora...");
            Pausa();
        }
        private static void EscenaInicial()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            EscribirCentrado("HACE MUCHO TIEMPO EN UNA GALAXIA\nMUY, MUY LEJANA...");
            Pausa();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            EscribirCentrado("STAR WARS\nEl Destino del Padawan");
            Console.Clear();
            string historia = " Eres un joven padawan que escapó hace muchos años de la orden 66 gracias al sacrificio de tu maestra Ilvi Grip.\n" +
        " Debido a una visión de tu antigua maestra, te encuentras en el planeta helado de Ilum. " +
        "Estas en busqueda de un nuevo cristal Kyber para tu nuevo sable laser aún sin construir.\n" +
        " Debido a la peligrosidad del planeta y a su invasión por parte del Imperio, deberás tomar decisiones que afectarán a tu destino y al de la galaxia.\n";
            EscribirParrafoCentrado(historia);
            Console.ForegroundColor = colorDefecto;
            Pausa();
            MuroDeHielo();
            return;
        }

        private static void MuroDeHielo()
        {
            bool escenaCompletada = false;

            while(!escenaCompletada)
            {
                Console.Clear();
                MostrarEstado();
                EscribirParrafoCentrado("Después de horas de caminata por el planeta helado, llegas a un enorme muro de hielo y roca. Es tan grande que no puedes ver la cima ni tampoco el final del muro. Sientes que hay algo más allá, pero no sabes cómo llegar. Debes tomar una decisión.");

                List<string> opciones = new List<string>
                {
                    "Usar la Fuerza para sentir. (-4 de Fuerza)",
                    "Usar el pioleto para escalar. (-4 de Resistencia)",
                    "Caminar alrededor del muro. (-2 de Resistencia)",
                    "Descansar. (Recupera 2 de Resistencia, 2 de Fuerza y 2 de Salud)"
                };
                if (salud < saludMaxima)
                {
                    opciones.Add("Usar un botiquín para recuperar salud.(Recupera 20 de Salud)");
                }

                int eleccion = MostrarMenuYPedirOpcion(opciones);

                switch (eleccion)
                {
                    case 1:
                        if (fuerza >= 4)
                            {
                                Notificacion(ModificarRecurso(ref fuerza, -4, fuerzaMaxima, "Fuerza"), colorNotificacion);
                                Pausa();
                                escenaCompletada = true;
                                MuroDeHielo2(); // Llamada a la siguiente escena del muro de hielo
                                return;
                            }
                        else
                            {
                                EscribirCentrado("No tienes suficiente Fuerza para concentrarte.");
                                Notificacion(ModificarRecurso(ref fuerza, -4, fuerzaMaxima, "Fuerza"), colorNotificacion);
                            }
                        Pausa();
                        break;
                    case 2:
                        
                        if (resistencia < 4)
                        {
                            EscribirParrafoCentrado($"Te has quedado sin resistencia y no se clava bien el piolet soltandose precipitandote al vacío. Caes al suelo.");
                            Notificacion(ModificarRecurso(ref resistencia, -4, resistenciaMaxima, "Resistencia"), colorNotificacion);
                            Notificacion(ModificarRecurso(ref salud, -75, saludMaxima, "Salud"), colorNotificacion);
                            
                            if (salud <= 0)
                            {
                                FinalPerdedor(); // Llamada a la escena final de perdedor
                                return; // Salir de la función si el jugador pierde
                            }
                        }
                        else
                        {
                            EscribirCentrado($"Subes con esfuerzo hasta llegar a un saliente.");
                            Notificacion(ModificarRecurso(ref resistencia, -4, resistenciaMaxima, "Resistencia"), colorNotificacion);
                        }
                        Pausa();
                        break;
                    case 3:
                        if (resistencia < 2)
                        {
                            EscribirCentrado("Te has quedado sin fuerzas y no puedes continuar.");
                            Notificacion(ModificarRecurso(ref resistencia, -2, resistenciaMaxima, "Resistencia"), colorNotificacion);
                        }
                        else
                        {
                            EscribirParrafoCentrado($"Decides caminar alrededor del muro de hielo.");
                            Notificacion(ModificarRecurso(ref resistencia, -2, resistenciaMaxima, "Resistencia"), colorNotificacion);
                        }
                        Pausa();
                        break;
                    case 4:
                        Descansar(2);
                        Pausa();
                        break;
                    case 5:
                        if (numeroBotiquines > 0)
                        {
                            UsarBotiquin(20);
                        }
                        else
                        {
                            EscribirCentrado("No tienes botiquines suficientes.");
                        }
                        Pausa();
                        break;
                }
            }
        }

        private static void MuroDeHielo2()
        {
            bool escenaCompletada = false;

            while(!escenaCompletada)
            {
                Console.Clear();
                MostrarEstado();
                EscribirParrafoCentrado("Canalizas la Fuerza, sientes un susurro que te llama desde una altura lejana.");
                List<string> opciones = new List<string>
                {
                    "Usar el piolet para escalar y buscar el susurro (-4 de Resistencia)",
                    "Caminar alrededor del muro (-2 de Resistencia)",
                    "Descansar. (Recupera 2 de Resistencia, 2 de Fuerza y 2 de Salud)"
                };
                if (salud < saludMaxima)
                {
                    opciones.Add("Usar un botiquín para recuperar salud.(Recupera 20 de Salud)");
                }

                int eleccion = MostrarMenuYPedirOpcion(opciones);

                switch (eleccion)
                {
                    case 1:
                        
                        if (resistencia < 4)
                        {
                            Notificacion(ModificarRecurso(ref resistencia, -4, resistenciaMaxima, "Resistencia"), colorNotificacion);
                            EscribirParrafoCentrado("Te has quedado sin fuerzas y no se clava bien el piolet soltandose precipitandote al vacío. Caes al suelo.");
                            Notificacion(ModificarRecurso(ref salud, -75, saludMaxima, "Salud"), colorNotificacion);
                            if (salud <= 0)
                            {
                                FinalPerdedor(); // Llamada a la escena final de perdedor
                                return; // Salir de la función si el jugador pierde
                            }
                        }
                        else
                        {
                            EscribirParrafoCentrado("Subes en la dirección del susurro hasta llegar a un saliente.");
                            Notificacion(ModificarRecurso(ref resistencia, -4, resistenciaMaxima, "Resistencia"), colorNotificacion);
                        }
                        Pausa();
                        Mecanismo();
                        return;
                    case 2:
                        
                        if (resistencia < 2)
                        {
                            EscribirCentrado("Te has quedado sin fuerzas y no puedes continuar.");
                            Notificacion(ModificarRecurso(ref resistencia, -2, resistenciaMaxima, "Resistencia"), colorNotificacion);
                        }
                        else
                        {
                            EscribirCentrado("Decides caminar alrededor del muro de hielo.");
                            Notificacion(ModificarRecurso(ref resistencia, -2, resistenciaMaxima, "Resistencia"), colorNotificacion);
                        }
                        Pausa();
                        break;
                    case 3:
                        Descansar(2);
                        Pausa();
                        break;
                    case 4:
                        if (numeroBotiquines > 0)
                        {
                            UsarBotiquin(20);
                        }
                        else
                        {
                            EscribirCentrado("No tienes botiquines suficientes.");
                        }
                        Pausa();
                        break;
                }
            }
        }

        private static void Mecanismo()
        {
            bool escenaCompletada = false;

            while (!escenaCompletada)
            {
                Console.Clear();
                MostrarEstado();
                EscribirParrafoCentrado("Al llegar al saliente, notas que hay algo raro en la pared.");
                List<string> opciones = new List<string>
                {
                    "Sentir con la Fuerza.(-4 de Fuerza)",
                    "Golpear la pared con el piolet.(-2 de Resistencia)",
                    "Seguir subiendo el muro.(-4 de resistencia)",
                    "Descansar.(Recupera 2 de Resistencia, 2 de Fuerza y 2 de Salud)"
                };
                if (salud < saludMaxima)
                {
                    opciones.Add("Usar un botiquín para recuperar salud.(Recupera 20 de Salud)");
                }

                int eleccion = MostrarMenuYPedirOpcion(opciones);

                switch (eleccion)
                {
                    case 1:
                        if (fuerza < 4)
                        {
                            EscribirCentrado("Te has quedado sin Fuerza y no puedes sentir nada");
                            Notificacion(ModificarRecurso(ref fuerza, -4, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        }
                        else
                        {
                            EscribirCentrado("Usas la Fuerza y descubres un mecanismo secreto en la pared.");
                            Notificacion(ModificarRecurso(ref fuerza, -4, fuerzaMaxima, "Fuerza"), colorNotificacion);
                            Pausa();
                            ActivarMecanismo(); // Llamada a la función que activa el mecanismo
                            return;
                        }
                        Pausa();
                        break;
                    case 2:
                        if (resistencia < 2)
                        {
                            EscribirCentrado("Te has quedado sin fuerzas y se te cae el piolet al intentar golpear la pared.");
                            Notificacion(ModificarRecurso(ref resistencia, -2, resistenciaMaxima, "Resistencia"), colorNotificacion);
                        }
                        else
                        {
                            EscribirCentrado("Golpeas la pared con el piolet, pero no pasa nada.");
                            Notificacion(ModificarRecurso(ref resistencia, -2, resistenciaMaxima, "Resistencia"), colorNotificacion);
                        }
                        Pausa();
                        break;
                    case 3:
                        if (resistencia < 4)
                        {
                            EscribirCentrado("Te has quedado sin fuerzas y no se clava bien el piolet soltandose precipitandote al vacío. Caes al suelo.");
                            Notificacion(ModificarRecurso(ref resistencia, -4, resistenciaMaxima, "Resistencia"), colorNotificacion);
                            Notificacion(ModificarRecurso(ref salud, -75, saludMaxima, "Salud"), colorNotificacion);
                            Pausa();
                            if (salud <= 0)
                            {
                                FinalPerdedor(); // Llamada a la escena final de perdedor
                                return; // Salir de la función si el jugador pierde
                            }
                            
                        }
                        else
                        {
                            EscribirCentrado("Subes con esfuerzo hasta llegar a un saliente. Pierdes 4 de Resistencia.");
                            Notificacion(ModificarRecurso(ref resistencia, -4, resistenciaMaxima, "Resistencia"), colorNotificacion);
                        }
                        Pausa();
                        break;
                    case 4:
                        Descansar(2);
                        Pausa();
                        break;
                    case 5:
                        if (numeroBotiquines > 0)
                        {
                            UsarBotiquin(20);
                        }
                        else
                        {
                            EscribirCentrado("No tienes botiquines suficientes.");
                        }
                        Pausa();
                        break;
                }
            }
        }

        private static void ActivarMecanismo()
        {
            int golpesPiolet = 0;
            bool desatascado = false;

            while (!desatascado)
            {
                Console.Clear();
                MostrarEstado();
                EscribirCentrado("El mecanismo está oculto en la pared y sientes que está atascado. ¿Qué deseas hacer?\n");
                List<string> opciones = new List<string>
                {
                    "Utilizar la Fuerza para activar el mecanismo (-15 de Fuerza)",
                    "Golpear el muro con el piolet en la zona donde crees que está el mecanismo (-2 de Resistencia)",
                    "Disparar con el blaster en el lugar del mecanismo (-5 de Munición)",
                    "Lanzar un detonador termico en el lugar del mecanismo",
                    "Descansar. (Recupera 2 de Resistencia, 2 de Fuerza y 2 de Salud)"
                };

                if (salud < saludMaxima)
                {
                    opciones.Add("Usar un botiquín para recuperar salud.(Recupera 20 de Salud)");
                }

                int eleccion = MostrarMenuYPedirOpcion(opciones);

                switch (eleccion)
            {
                case 1:
                    if (fuerza < fuerzaMaxima)
                    {
                        EscribirCentrado("Te has quedado sin Fuerza y no puedes activar el mecanismo.");
                        Notificacion(ModificarRecurso(ref fuerza, -15, fuerzaMaxima, "Fuerza"), colorNotificacion);
                    }
                    else
                    {
                        EscribirCentrado("Usas la Fuerza, pero el mecanismo está demasiado encasquillado por el hielo. Necesitas un impacto físico.");
                        Notificacion(ModificarRecurso(ref fuerza, -15, fuerzaMaxima, "Fuerza"), colorNotificacion);
                    }
                    Pausa();
                    break;
                case 2:
                    if (resistencia < 2)
                    {
                        EscribirCentrado("Te has quedado sin resistencia y no puedes golpear el muro.");
                        Notificacion(ModificarRecurso(ref resistencia, -2, resistenciaMaxima, "Resistencia"), colorNotificacion);
                        Pausa();
                        break;
                    }

                    golpesPiolet++;
                    if (golpesPiolet < 4)
                    {
                        EscribirCentrado("Golpeas el muro con el piolet y notas un leve chasquido entre el hielo. Parece que funciona.");
                        Notificacion(ModificarRecurso(ref resistencia, -4, resistenciaMaxima, "Resistencia"), colorNotificacion);
                        Pausa();
                    }
                    else
                    {
                        EscribirCentrado("\n¡*CRACK*!\n");
                        EscribirCentrado("Tras este último golpe, el hielo resquebraja por completo y los engranajes se liberan. Pierdes 2 de Resistencia.");
                        Pausa();
                        Puerta();
                        return;
                    }
                    break;
                case 3:
                    if (munición < 5)
                    {
                        EscribirCentrado("Te has quedado sin munición y no puedes disparar.");
                        Notificacion(ModificarRecurso(ref munición, -5, municiónMaxima, "Munición"), colorNotificacion);
                    }
                    else
                    {
                        EscribirCentrado("Disparas con el blaster, pero rebota en el muro. Escuchas pasos lejanos...");
                        Notificacion(ModificarRecurso(ref munición, -5, municiónMaxima, "Munición"), colorNotificacion);
                    }
                    Pausa();
                    break;
                case 4:
                    EscribirCentrado("Lanzas un detonador termico. La detonación es tan fuerte que el muro comienza a temblar, el saliente se destruye y caes al vacío.");
                    Pausa();
                    FinalPerdedor(); // Llamada a la escena final de perdedor
                    return;
                case 5:
                    Descansar(2);
                    Pausa();
                    break;
                case 6:
                    if (numeroBotiquines > 0)
                    {
                        UsarBotiquin(20);
                    }
                    else
                    {
                        EscribirCentrado("No tienes botiquines suficientes.");
                    }
                    Pausa();
                    break;
            }
            }
        }

        private static void Puerta()
        {
            bool escenaCompleta = false;

            while (!escenaCompleta)
            {
                Console.Clear();
                MostrarEstado();
                EscribirParrafoCentrado("El mecanismo se activa. Oyes engranajes moviéndose y de repente, una puerta secreta se abre ante ti. \n¿Qué deseas hacer?");
                List<string> opciones = new List<string>
                {
                    "Entrar por la puerta secreta",
                    "Utilizar la Fuerza para sentir si hay peligro detrás de la puerta (-4 de Fuerza)",
                    "Descansar. (Recupera 2 de Resistencia, 2 de Fuerza y 2 de Salud)"
                };
                if (salud < saludMaxima)
                {
                    opciones.Add("Usar un botiquín para recuperar salud.(Recupera 20 de Salud)");
                }

                int eleccion = MostrarMenuYPedirOpcion(opciones);
                switch (eleccion)
                {
                    case 1:
                        EscribirCentrado("Decides entrar por la puerta secreta.");
                        Pausa();
                        Sala1(); // Llamada a la siguiente escena
                        return;
                    case 2:
                        if (fuerza < 4)
                        {
                            EscribirCentrado("Te has quedado sin Fuerza y no puedes sentir nada.");
                            Notificacion(ModificarRecurso(ref fuerza, -4, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        }
                        else
                        {
                            EscribirParrafoCentrado("Usas la Fuerza y sientes que hay soldados de asalto imperiales al final del pasillo. También percibes un susurro lejano. Pierdes 4 de Fuerza.");
                            Notificacion(ModificarRecurso(ref fuerza, -4, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        }
                        Pausa();
                        break;
                    case 3:
                        Descansar(2);
                        Pausa();
                        break;
                    case 4:
                        if (numeroBotiquines > 0)
                        {
                            UsarBotiquin(20);
                        }
                        else
                        {
                            EscribirCentrado("No tienes botiquines suficientes.");
                        }
                        Pausa();
                        break;
                }
            }
        }

        private static void Sala1()
        {
            bool escenaCompletada = false;

            while (!escenaCompletada)
            {
                Console.Clear();
                MostrarEstado();
                EscribirParrafoCentrado("Caminas por un pasillo oscuro hasta llegar a una sala llena de soldados de asalto imperiales.\nAún no te han visto. ¿Qué deseas hacer?");
                List<string> opciones = new List<string>
                {
                    "Atacas con tu blaster a los soldados.",
                    "Usas la Fuerza para sentir. (-4 de Fuerza)",
                    "Usas la Fuerza para manipular la mente de un soldado para que ataque a sus compañeros. (-9 de Fuerza)",
                    "Descansar. (Recupera 2 de Resistencia, 2 de Fuerza y 2 de Salud)"
                };
                if (salud < saludMaxima)
                {
                    opciones.Add("Usar un botiquín para recuperar salud.(Recupera 20 de Salud)");
                }

                int eleccion = MostrarMenuYPedirOpcion(opciones);

                switch (eleccion)
                {
                    case 1:
                        EscribirParrafoCentrado("Desenfundas tu blaster. Apuntando a los soldados, disparas con precisión y logras eliminarlos a algunos, pero son demasiados y logran abatirte. Has perdido la batalla.");
                        Pausa();
                        FinalPerdedor();
                        return;
                    case 2:
                        if (fuerza < 4)
                        {
                            EscribirCentrado("Te has quedado sin Fuerza y no puedes sentir nada.");
                            Notificacion(ModificarRecurso(ref fuerza, -4, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        }
                        else
                        {
                            EscribirCentrado("Usas la Fuerza y sientes que hay una sala contigua donde el susurro es cada vez más fuerte.");
                            Notificacion(ModificarRecurso(ref fuerza, -4, fuerzaMaxima, "Fuerza"), colorNotificacion);
                            Pausa();
                            Sala1_1();
                            return;
                        }
                        Pausa();
                        break;
                    case 3:
                        if (fuerza < 9)
                        {
                            EscribirCentrado("Te has quedado sin Fuerza y no puedes manipular la mente de nadie.");
                            Notificacion(ModificarRecurso(ref fuerza, -9, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        }
                        else
                        {
                            EscribirCentrado("Usas la Fuerza para manipular la mente de un soldado y lo haces atacar a sus compañeros.");
                            Notificacion(ModificarRecurso(ref fuerza, -9, fuerzaMaxima, "Fuerza"), colorNotificacion);
                            Pausa();
                            Sala1_2();
                            return;
                        }
                        Pausa();
                        break;
                    case 4:
                        Descansar(2);
                        Pausa();
                        break;
                    case 5:
                        if (numeroBotiquines > 0)
                        {
                            UsarBotiquin(20);
                        }
                        else
                        {
                            EscribirCentrado("No tienes botiquines suficientes.");
                        }
                        Pausa(); 
                        break;
                }
            }
        }

        private static void Sala1_1()
        {
            bool escenaCompletada = false;

            while (!escenaCompletada)
            {
                Console.Clear();
                MostrarEstado();
                EscribirParrafoCentrado("Caminas por un pasillo oscuro hasta llegar a una sala llena de soldados de asalto imperiales.\nAún no te han visto. ¿Qué deseas hacer?");
                List<string> opciones = new List<string>
                {
                    "Atacas con tu blaster a los soldados.",
                    "Vas a la sala contigua de forma sigilosa",
                    "Usas la Fuerza para manipular la mente de un soldado para que ataque a sus compañeros. (-9 de Fuerza)",
                    "Descansar. (Recupera 2 de Resistencia, 2 de Fuerza y 2 de Salud)"
                };
                if (salud < saludMaxima)
                {
                    opciones.Add("Usar un botiquín para recuperar salud.(Recupera 20 de Salud)");
                }

                int eleccion = MostrarMenuYPedirOpcion(opciones);

                switch (eleccion)
                {
                    case 1:
                        EscribirParrafoCentrado("Desenfundas tu blaster. Apuntando a los soldados, disparas con precisión y logras eliminarlos a algunos, pero son demasiados y logran abatirte. Has perdido la batalla.");
                        Pausa();
                        FinalPerdedor();
                        return;
                    case 2:
                        Sala2(); // Llamada nueva escena
                        return;
                    case 3:
                        if (fuerza < 9)
                        {
                            EscribirCentrado("Te has quedado sin Fuerza y no puedes manipular la mente de nadie.");
                            Notificacion(ModificarRecurso(ref fuerza, -9, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        }
                        else
                        {
                            EscribirCentrado("Usas la Fuerza para manipular la mente de un soldado y lo haces atacar a sus compañeros.");
                            Notificacion(ModificarRecurso(ref fuerza, -9, fuerzaMaxima, "Fuerza"), colorNotificacion);
                            Pausa();
                            Sala1_3();
                            return;
                        }
                        Pausa();
                        break;
                    case 4:
                        Descansar(2);
                        Pausa();
                        break;
                    case 5:
                        if (numeroBotiquines > 0)
                        {
                            UsarBotiquin(20);
                        }
                        else
                        {
                            EscribirCentrado("No tienes botiquines suficientes.");
                        }
                        Pausa(); 
                        break;
                }
            }
        }

        private static void Sala1_2()
        {
            bool escenaCompletada = false;

            while (!escenaCompletada)
            {
                Console.Clear();
                MostrarEstado();
                EscribirParrafoCentrado("Tras pillar por sorpresa a los soldados imperiales, el soldado que engañaste con el truco mental, es capaz de abatir a varios compañeros suyos antes de que lo eliminen. Un soldado te ve y al final tienes que entrar en batalla, pero por suerte el núemro de soldado es mucho menor al anterior. Con gran habilidad y punteria logras ganar aunque muy mal herido. Ahora el Imperio sabe que estás aquí y en breve llegarán refuerzos.");
                EscribirCentrado("¿Qué deseas hacer");
                List<string> opciones = new List<string>
                {
                    "Sentir con la Fuerza. -4 de Fuerza",
                    "Registrar los cuerpos de los soldados",
                    "Descansar. (Recupera 2 de Resistencia, 2 de Fuerza y 2 de Salud)"
                };
                if (salud < saludMaxima)
                {
                    opciones.Add("Usar un botiquín para recuperar salud.(Recupera 20 de Salud)");
                }

                int eleccion = MostrarMenuYPedirOpcion(opciones);

                switch (eleccion)
                {
                    case 1:
                        if (fuerza < 4)
                        {
                            EscribirCentrado("Te has quedado sin Fuerza y no puedes sentir nada.");
                            Notificacion(ModificarRecurso(ref fuerza, -4, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        }
                        else
                        {
                            EscribirCentrado("Usas la Fuerza y sientes que hay una sala contigua donde el susurro es cada vez más fuerte.");
                            Notificacion(ModificarRecurso(ref fuerza, -4, fuerzaMaxima, "Fuerza"), colorNotificacion);
                            Pausa();
                            Sala1_3();
                            return;
                        }
                        Pausa();
                        break;
                    case 2:
                        if (registro == 0)
                        {
                            EscribirParrafoCentrado("Resgistras a fondo todos los soladados de asalto.");
                            Notificacion(ModificarRecurso(ref numeroBotiquines, numeroBotiquinesMaximo, numeroBotiquinesMaximo, "Botiquín"), colorNotificacion);
                            Notificacion(ModificarRecurso(ref munición, municiónMaxima, municiónMaxima, "Munición"), colorNotificacion);
                            registro++;
                        }
                        else
                        {
                            EscribirCentrado("Ya no encuentras nada de valor.");
                        }
                        Pausa();
                        break;
                    case 3:
                        Descansar(2);
                        Pausa();
                        break;
                    case 4:
                        if (numeroBotiquines > 0)
                        {
                            UsarBotiquin(20);
                        }
                        else
                        {
                            EscribirCentrado("No tienes botiquines suficientes.");
                        }
                        Pausa();  
                        break;
                }
            }
        }

        private static void Sala1_3()
        {
            bool escenaCompletada = false;

            while (!escenaCompletada)
            {
                Console.Clear();
                MostrarEstado();
                EscribirParrafoCentrado("Tras pillar por sorpresa a los soldados imperiales, el soldado que engañaste con el truco mental, es capaz de abatir a varios compañeros suyos antes de que lo eliminen. Un soldado te ve y al final tienes que entrar en batalla, pero por suerte el núemro de soldado es mucho menor al anterior. Con gran habilidad y punteria logras ganar aunque muy mal herido. Ahora el Imperio sabe que estás aquí y en breve llegarán refuerzos.");
                EscribirCentrado("¿Qué deseas hacer");
                List<string> opciones = new List<string>
                {
                    "Ir a la sala contigua.",
                    "Registrar los cuerpos de los soldados",
                    "Descansar. (Recupera 2 de Resistencia, 2 de Fuerza y 2 de Salud)"
                };
                if (salud < saludMaxima)
                {
                    opciones.Add("Usar un botiquín para recuperar salud.(Recupera 20 de Salud)");
                }
                
                int eleccion = MostrarMenuYPedirOpcion(opciones);

                switch (eleccion)
                {
                    case 1:
                        Sala2(); // Llamada a la escena nueva
                        return;
                    case 2:
                        if (registro == 0)
                        {
                            EscribirParrafoCentrado("Resgistras a fondo todos los soladados de asalto.");
                            Notificacion(ModificarRecurso(ref numeroBotiquines, numeroBotiquinesMaximo, numeroBotiquinesMaximo, "Botiquín"), colorNotificacion);
                            Notificacion(ModificarRecurso(ref munición, municiónMaxima, municiónMaxima, "Munición"), colorNotificacion);
                            registro++;
                        }
                        else
                        {
                            EscribirCentrado("Ya no encuentras nada de valor.");
                        }
                        Pausa();
                        break;
                    case 3:
                        Descansar(2);
                        Pausa();
                        break;
                    case 4:
                        if (numeroBotiquines > 0)
                        {
                            UsarBotiquin(20);
                        }
                        else
                        {
                            EscribirCentrado("No tienes botiquines suficientes.");
                        }
                        Pausa(); 
                        break;
                }
            }
        }

        private static void Sala2()
        {
            EscribirParrafoCentrado("Entras en la sala contigua y sientes cómo, desde una gran veta de cristales, algo te llama. A medida que te acercas ve un brillo intenso y el susurro se convierte en un resonar fuerte. Al llegar a su altura, encuentras lo que venias buscando. Un cristal Kyber y es de color...");
            List<string> opciones = new List<string>
            {
                "Verde",
                "Azul",
                "Morado",
                "amarillo"
            };

            int eleccion = MostrarMenuYPedirOpcion(opciones);

            switch (eleccion)
            {
                case 1:
                    colorSable = ConsoleColor.Green;
                    break;
                case 2:
                    colorSable = ConsoleColor.Blue;
                    break;
                case 3:
                    colorSable = ConsoleColor.DarkMagenta;
                    break;
                case 4:
                    colorSable = ConsoleColor.DarkYellow;
                    break;
            }
            MiniJuegoCristalKyber();
            Pausa();
            Sala2_1();
            return;
        }

        private static void Sala2_1()
        {
            Notificacion("Salud Máxima ha aumentado", colorNotificacion);
            Notificacion("Fuerza Máxima ha aumentado", colorNotificacion);
            Notificacion("Resistencia Máxima ha aumentado", colorNotificacion);
            fuerzaMaxima += 10;
            saludMaxima += 50;
            resistenciaMaxima += 10;
            Notificacion(ModificarRecurso(ref salud, saludMaxima, saludMaxima, "Salud"), colorNotificacion);
            Notificacion(ModificarRecurso(ref fuerza, fuerzaMaxima, fuerzaMaxima, "Fuerza"), colorNotificacion);
            Notificacion(ModificarRecurso(ref resistencia, resistenciaMaxima, resistenciaMaxima, "Resistencia"), colorNotificacion);
            Pausa();

            bool escenaCompletada = false;

            while (!escenaCompletada)
            {
                Console.Clear();
                MostrarEstado();
                EscribirParrafoCentrado("Una vez armado tu sable laser, lo empuñas y lo enciendes. BZZZZZ. Sientes com la Fuerza emana del cristal y conecta contigo. Sientes que te habla, guía tus movimientos y...\n"
                + " En ese instante sientes como una fuerza oscura y tenebrosa interrumpe tu concentración. En la puerta de la sala, hay una silueta oscura. Con una armadura completa gris oscura y detrás, cientos de soldados imperiales apuntandote.");
                EscribirCentrado("¿Qué haces?");
                List<string> opciones = new List<string>
                {
                    "Sentir Fuerza (-4 de Fuerza)",
                    "Atacas con el sable laser a tus enemigos",
                    "Usar el empujón de la Fuerza para derribar a tus enemigos. (-10 Fuerza)"
                };

                int eleccion = MostrarMenuYPedirOpcion(opciones);

                switch (eleccion)
                {
                    case 1:
                        EscribirCentrado("Sientes el impulso de querer saltar hacia el techo de la sala. Percibes que puedes escapar por ahí.");
                        Notificacion(ModificarRecurso(ref fuerza, -4, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        Pausa();
                        Sala2_2();
                        return;
                    case 2:
                        EscribirParrafoCentrado("Al abalanzarte contra los soldados, estos empiezan a disparar a discrepción contra ti. Ágilmente logras desviar con tu sable los disparos de los blaster," +
                        " pero la silueta oscura alza la mano y te atrapa con la fuerza mientra te estrangula. Los soldados aporvechan ese momento para dispararte sin piedad y logran abatirte.");
                        Pausa();
                        FinalPerdedor();
                        return;
                    case 3:
                        EscribirParrafoCentrado("Usando la Fuerza, generas y lanzas una gran ola que despide por los aires a los soldados de asaltos. Estos quedan inconscientes al ser golpeado fuertemente por el suelo o los muros de hielo y roca." +
                        "La silueta oscura levanta la mano y evita el empujón.");
                        Notificacion(ModificarRecurso(ref fuerza, -10, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        Pausa();
                        Sala2_3();
                        return;
                }
            }
        }

        private static void Sala2_2()
        {
            bool escenaCompleta = false;

            while (!escenaCompleta)
            {
                Console.Clear();
                MostrarEstado();
                EscribirCentrado("Sientes el impulso de querer saltar hacia el techo de la sala. Percibes que puedes escapar por ahí.");
                EscribirCentrado("¿Qué haces?");
                List<string> opciones = new List<string>
                {
                    "Saltar con la Fuerza hacia el techo (-10 de Fuerza)",
                    "Atacas con el sable laser a tus enemigos",
                    "Usar el empujón de la Fuerza para derribar a tus enemigos. (-10 Fuerza)"
                };
    
                int eleccion = MostrarMenuYPedirOpcion(opciones);
    
                switch (eleccion)
                {
                    case 1:
                        Notificacion(ModificarRecurso(ref fuerza, -10, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        Pausa();
                        FinalVencedor();
                        return;
                    case 2:
                        EscribirParrafoCentrado("Al abalanzarte contra los soldados, estos empiezan a disparar a discrepción contra ti. Ágilmente logras desviar con tu sable los disparos de los blaster," +
                        " pero la silueta oscura alza la mano y te atrapa con la fuerza mientra te estrangula. Los soldados aporvechan ese momento para dispararte sin piedad y logran abatirte.");
                        Pausa();
                        FinalPerdedor();
                        return;
                    case 3:
                        EscribirParrafoCentrado("Usando la Fuerza, generas y lanzas una gran ola que despide por los aires a los soldados de asaltos. Estos quedan inconscientes al ser golpeado fuertemente por el suelo o los muros de hielo y roca." +
                        "La silueta oscura levanta la mano y evita el empujón.");
                        Notificacion(ModificarRecurso(ref fuerza, -10, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        Pausa();
                        Sala2_4();
                        return;
                }
            }
        }

        private static void Sala2_3()
        {
            bool escenaCompleta = false;

            while (!escenaCompleta)
            {
                Console.Clear();
                MostrarEstado();
                EscribirParrafoCentrado("Usando la Fuerza, generas y lanzas una gran ola que despide por los aires a los soldados de asaltos. Estos quedan inconscientes al ser golpeado fuertemente por el suelo o los muros de hielo y roca." +
                        "La silueta oscura levanta la mano y evita el empujón.");
                EscribirCentrado("¿Qué haces?");
                List<string> opciones = new List<string>
                {
                    "Sentir Fuerza (-4 de Fuerza)",
                    "Atacas con el sable laser",
                    "Descansar. (Recupera 5 de Resistencia, 5 de Fuerza y 5 de Salud)"
                };
                if (salud < saludMaxima)
                {
                    opciones.Add("Usar un botiquín para recuperar salud.(Recupera 40 de Salud)");
                }
    
                int eleccion = MostrarMenuYPedirOpcion(opciones);
                
                switch (eleccion)
                {
                    case 1:
                        EscribirParrafoCentrado("Sientes el impulso de querer saltar hacia el techo de la sala. Percibes que puedes escapar por ahí.");
                        Notificacion(ModificarRecurso(ref fuerza, -4, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        Pausa();
                        Sala2_5();
                        return;
                    case 2:
                        EscribirParrafoCentrado("Te abalanzas contra la persona que lleva la armadura oscura. Enciende su sable laser de color rojo. Tras unos cuantos intercambios de golpes, te das cuenta que tu entranamiento en el manejo del sable es muy inferior y" +
                        "acabas con heridas.");
                        Notificacion(ModificarRecurso(ref fuerza, -5, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        Notificacion(ModificarRecurso(ref salud, -10, saludMaxima, "Salud"), colorNotificacion);
                        Notificacion(ModificarRecurso(ref resistencia, -10, resistenciaMaxima, "Resistencia"), colorNotificacion);
                        Pausa();
                        break;
                    case 3:
                        Descansar(5);
                        EscribirCentrado("Mientras te tomas un respiro, la figura oscura empieza a hablar (con voz femenina y enlatada)");
                        Pausa();
                        Sala2_6();
                        return;
                    case 4:
                        if (numeroBotiquines > 0)
                        {
                            UsarBotiquin(40);
                        }
                        else
                        {
                            EscribirCentrado("No tienes botiquines suficientes.");
                        }
                        EscribirCentrado("Mientras te curas, la figura oscura empieza a hablar (con voz femenina y enlatada)");
                        Pausa();
                        Sala2_6();
                        return;
                }
            }
        }

        private static void Sala2_4()
        {
            bool escenaCompletada = false;

            while (!escenaCompletada)
            {
                Console.Clear();
                MostrarEstado();
                EscribirParrafoCentrado("Usando la Fuerza, generas y lanzas una gran ola que despide por los aires a los soldados de asaltos. Estos quedan inconscientes al ser golpeado fuertemente por el suelo o los muros de hielo y roca." +
                        "La silueta oscura levanta la mano y evita el empujón.");
                EscribirCentrado("¿Qué haces?");
                List<string> opciones = new List<string>
                {
                    "Saltar con la Fuerza hacia el techo (-10 de Fuerza)",
                    "Atacas con el sable laser",
                    "Descansar. (Recupera 5 de Resistencia, 5 de Fuerza y 5 de Salud)"
                };
                if (salud < saludMaxima)
                {
                    opciones.Add("Usar un botiquín para recuperar salud.(Recupera 40 de Salud)");
                }
    
                int eleccion = MostrarMenuYPedirOpcion(opciones);
    
                switch (eleccion)
                {
                    case 1:
                        Notificacion(ModificarRecurso(ref fuerza, -10, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        Pausa();
                        FinalVencedor();
                        return;
                    case 2:
                        EscribirParrafoCentrado("Te abalanzas contra la persona que lleva la armadura oscura. Enciende su sable laser de color rojo. Tras unos cuantos intercambios de golpes, te das cuenta que tu entranamiento en el manejo del sable es muy inferior y" +
                        "acabas con heridas.");
                        Notificacion(ModificarRecurso(ref fuerza, -5, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        Notificacion(ModificarRecurso(ref salud, -10, saludMaxima, "Salud"), colorNotificacion);
                        Notificacion(ModificarRecurso(ref resistencia, -10, resistenciaMaxima, "Resistencia"), colorNotificacion);
                        Pausa();
                        break;
                    case 3:
                        Descansar(5);
                        EscribirCentrado("Mientras te tomas un respiro, la figura oscura empieza a hablar (con voz femenina y enlatada)");
                        Pausa();
                        Sala2_5();
                        return;
                    case 4:
                        if (numeroBotiquines > 0)
                        {
                            UsarBotiquin(40);
                            Pausa();
                            EscribirCentrado("Mientras te curas, la figura oscura empieza a hablar (con voz femenina y enlatada)");
                            Pausa();
                            Sala2_5();
                            return;
                        }
                        else
                        {
                            EscribirCentrado("No tienes botiquines suficientes.");
                            Pausa();
                        }
                        break;
                }
            }
        }

        private static void Sala2_5()
        {
            bool escenaCompletada = false;
            int descansos = 0;

            while (!escenaCompletada)
            {
                Console.Clear();
                MostrarEstado();
                Console.ForegroundColor = ConsoleColor.Red;
                EscribirParrafoCentrado($"Tus trucos de Jedi no funcionarán conmigo. Se quien eres, eres {player}, padawan de la maestra Ivil Grip. Yo..." +
                "MATE A TU MAESTRA...");
                EscribirCentrado("¿Qué haces?");
                List<string> opciones = new List<string>
                {
                    "Saltar con la Fuerza hacia el techo (-10 de Fuerza)",
                    "Atacas con el sable laser (odio)",
                    "Descansar. (Recupera 5 de Resistencia, 5 de Fuerza y 5 de Salud)"
                };
                if (salud < saludMaxima)
                {
                    opciones.Add("Usar un botiquín para recuperar salud.(Recupera 40 de Salud)");
                }
    
                int eleccion = MostrarMenuYPedirOpcion(opciones);
    
                switch (eleccion)
                {
                    case 1:
                        Notificacion(ModificarRecurso(ref fuerza, -10, fuerzaMaxima, "Fuerza"), colorNotificacion);
                        Pausa();
                        FinalVencedor();
                        return;
                    case 2:
                        FinalAlternativo();
                        return;
                    case 3:
                        if (descansos > 2)
                        {
                            EscribirParrafoCentrado("La mujer te ataca con ferocidad y rapidez. No te da tiempo a esqivarla y te atraviesa el pecho con su sable laser. Mientras escuchas el chisporrotear del sable calcinando tu cuerpo" +
                            " escuchas como se rie. Apaga su sable y caes al suelo. Mientras percibes como tu vida se va escapando vuelves a oir esa voz metalica femenina...");
                            Pausa();
                            Console.ForegroundColor = ConsoleColor.Red;
                            EscribirParrafoCentrado($"JA, JA, JA. Adios {player}, saluda a Ilvi de mi parte. Seguro que tenéis mucho de que hablar. Ja, ja, ja.");
                            Pausa();
                            FinalPerdedor();
                            return;
                        }
                        else
                        {
                            Descansar(5);
                            descansos++;
                        }
                        Pausa();
                        break;
                    case 4:
                        EscribirParrafoCentrado("La mujer te ataca con ferocidad y rapidez. No te da tiempo a esqivarla y te atraviesa el pecho con su sable laser. Mientras escuchas el chisporrotear del sable calcinando tu cuerpo" +
                        " escuchas como se rie. Apaga su sable y caes al suelo. Mientras percibes como tu vida se va escapando vuelves a oir esa voz metalica femenina...");
                        Pausa();
                        Console.ForegroundColor = ConsoleColor.Red;
                        EscribirParrafoCentrado($"JA, JA, JA. Adios {player}, saluda a Ilvi de mi parte. Seguro que tenéis mucho de que hablar. Ja, ja, ja.");
                        Pausa();
                        FinalPerdedor();
                        return;
                }
            }
        }

        private static void Sala2_6()
        {
            bool escenaCompletada = false;

            int descansos = 0;

            while (!escenaCompletada)
            {
                Console.Clear();
                MostrarEstado();
                Console.ForegroundColor = ConsoleColor.Red;
                EscribirParrafoCentrado($"Tus trucos de Jedi no funcionarán conmigo. Se quien eres, eres {player}, padawan de la maestra Ivil Grip. Yo..." +
                "MATE A TU MAESTRA...");
                EscribirCentrado("¿Qué haces?");
                List<string> opciones = new List<string>
                {
                    "Sentir Fuerza (-4 de Fuerza)",
                    "Atacas con el sable laser (odio)",
                    "Descansar. (Recupera 5 de Resistencia, 5 de Fuerza y 5 de Salud)"
                };
                if (salud < saludMaxima)
                {
                    opciones.Add("Usar un botiquín para recuperar salud.(Recupera 40 de Salud)");
                }
    
                int eleccion = MostrarMenuYPedirOpcion(opciones);
                
                switch (eleccion)
                {
                    case 1:
                        
                        if (fuerza > 3)
                        {
                            EscribirParrafoCentrado("Sientes el impulso de querer saltar hacia el techo de la sala. Percibes que puedes escapar por ahí.");
                            Notificacion(ModificarRecurso(ref fuerza, -4, fuerzaMaxima, "Fuerza"), colorNotificacion);
                            Pausa();
                            Sala2_5();
                            return;
                        }
                        else
                        {
                            EscribirCentrado("No tienes poder suficiente para usar la Fuerza.");
                        }
                        Pausa();
                        break;
                    case 2:
                        FinalAlternativo();
                        return;
                    case 3:
                        if (descansos > 4)
                        {
                            EscribirParrafoCentrado("La mujer te ataca con ferocidad y rapidez. No te da tiempo a esqivarla y te atraviesa el pecho con su sable laser. Mientras escuchas el chisporrotear del sable calcinando tu cuerpo" +
                            " escuchas como se rie. Apaga su sable y caes al suelo. Mientras percibes como tu vida se va escapando vuelves a oir esa voz metalica femenina...");
                            Pausa();
                            Console.ForegroundColor = ConsoleColor.Red;
                            EscribirParrafoCentrado($"JA, JA, JA. Adios {player}, saluda a Ilvi de mi parte. Seguro que tenéis mucho de que hablar. Ja, ja, ja.");
                            Pausa();
                            FinalPerdedor();
                            return;
                        }
                        else
                        {
                            Descansar(5);
                            descansos++;
                        }
                        Pausa();
                        break;
                    case 4:
                        EscribirParrafoCentrado("La mujer te ataca con ferocidad y rapidez. No te da tiempo a esqivarla y te atraviesa el pecho con su sable laser. Mientras escuchas el chisporrotear del sable calcinando tu cuerpo" +
                        " escuchas como se rie. Apaga su sable y caes al suelo. Mientras percibes como tu vida se va escapando vuelves a oir esa voz metalica femenina...");
                        Console.ForegroundColor = ConsoleColor.Red;
                        EscribirParrafoCentrado($"JA, JA, JA. Adios {player}, saluda a Ilvi de mi parte. Seguro que tenéis mucho de que hablar. Ja, ja, ja.");
                        Pausa();
                        FinalPerdedor();
                        break;
                }
            }
        }

        private static void Despedida()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            EscribirCentrado("\n¡Que la Fuerza te acompañe!");
            Pausa();
        }

        private static bool PreguntarReinicio()
        {
            EscribirCentrado("\n--------------------------------------------------------");
            EscribirCentrado("¿Quieres volver a jugar? (S/N)");
            string resp = Console.ReadLine()?.Trim().ToUpper();
            return resp == "S" || resp == "SI";
        }

        private static void FinalVencedor()
        {
            Console.Clear();
            EscribirParrafoCentrado("Saltas con fé hacia el techo usando el poder de la Fuerza. Creyendo que vas a chocar cierras los ojos, pero lo único que sientes es frío." +
            "Has atravesado el techo de la sala. Era un espejismo óptico formado por los reflejos de los cristales. Al posarte en el suelo, sales corriendo y te pierdes en la nieve" +
            " salvando así tu vida y dando esperanza a la galaxia.");
            EscribirCentrado("¡ENHORABUENA HAS GANADO!");
            Pausa();
        }

        private static void FinalPerdedor()
        {
            Console.Clear();
            Notificacion($"\nHas perdido toda tu salud. El Imperio ha ganado esta vez.", ConsoleColor.Red);
            Notificacion("GAME OVER", ConsoleColor.Red);
            Pausa();
        }

        private static void FinalAlternativo()
        {
            Console.Clear();
            MostrarEstado();
            EscribirParrafoCentrado("Enciendes tu sable laser y te lanzas a luchar contra tu enemigo. Desatas todo tu odio en tus golpes. A cada cual más poderoso, más certero y potente." +
            " Después de una lucha intensa, le cortas un brazo y rompes su sable. La mujer cae al suelo debido a sus heridas. Se arrastra por el suelo. Con odio, arrancas el casco de la cabeja de la mujer con una fuerte patada." +
            "La mujer se da la vuelta. Reconoces ese rostro... Esa mujer era tu mejor amiga en el templo Jedi cuando erais niños. Su nombre es Efferal Dol. Con voz timida y desgarrada, te suplica que no la mates, que perdones sus pecados." +
            " tenía miedo y sucumbio al lado oscuro...");
            List<string> opciones = new List<string>
            {
                "Vengas la muerte de tu maestra y la matas...",
                "Apagas tu sable de luz, te vas y la dejas con vida..."
            };


            int eleccion = MostrarMenuYPedirOpcion(opciones);

            switch (eleccion)
            {
                case 1:
                    Console.Clear();
                    MostrarEstado();
                    EscribirParrafoCentrado("Le clavas tu sable en la garganta para que deje de hablar mientras dices...");
                    EscribirParrafoCentrado("Ya no importa el pasado. Matates a mi maestra y ahora estas pagando esa traición.");
                    colorSable = ConsoleColor.DarkRed;
                    Pausa();
                    Console.Clear();
                    MostrarEstado();
                    EscribirParrafoCentrado("En ese momento tu sable se torna de un color rojo carmesí. Entiendes las consecuencias de tus actos, ya no hay vuelta atrás. Has caido en el lado oscuro." +
                    " Arrebatas la armadura de la mujer y te la pones...");
                    Pausa();
                    break;
                case 2:
                    Console.Clear();
                    MostrarEstado();
                    EscribirParrafoCentrado("Te das cuenta de tus actos, de como el odio y el lado oscuro se están apoderando de ti. Por fortuna te das cuenta antes de que sea demasiado tarde." +
                    " Apagas el sable. Te das media vuelta y...");
                    Pausa();
                    FinalVencedor();
                    break;
            }
        }

        private static void MostrarEstado()
        {
            Notificacion($"\nEstado del jugador {player}: | Vida: {salud}/{saludMaxima} | Fuerza: {fuerza}/{fuerzaMaxima} | Resistencia: {resistencia}/{resistenciaMaxima} | Munición: {munición}/{municiónMaxima} | Botiquines: {numeroBotiquines}/{numeroBotiquinesMaximo}\n", colorSable);
        }

        private static string ModificarRecurso(ref int recurso, int cantidad, int maximo, string nombreRecurso)
        {
            int valorAnteriro = recurso;
            recurso = Math.Clamp(recurso + cantidad, 0, maximo);
            int diferenciaReal = recurso - valorAnteriro;
            if (diferenciaReal == 0)
            {
                return $"({nombreRecurso} al máximo)";
            }

            string signo = diferenciaReal > 0 ? "+" : "-";
            return $"({signo}{diferenciaReal} de {nombreRecurso})";
            
        }

        private static void ResetearEstadisticas()
        {
            salud = 100;
            saludMaxima = 100;
            fuerza = 10;
            fuerzaMaxima = 10;
            resistencia = 10;
            resistenciaMaxima = 10;
            munición = 100;
            municiónMaxima = 100;
            numeroBotiquines = 2;
            colorSable = ConsoleColor.White;
        }

        private static void EscribirCentrado(string texto)
        {
            int anchoConsola = Console.WindowWidth;
            string[] lineas = texto.Split('\n');
            foreach (string linea in lineas)
            {
                // Calculamos los espacios necesarios a la izquierda para centrar 
                int espacios = Math.Max(0, (anchoConsola - linea.Length) / 2);
                Console.WriteLine(new string(' ', espacios) + linea);
            }
        }

        private static void EscribirParrafoCentrado(string texto, int anchoMaximo = 60)
        {
            int anchoConsola = Console.WindowWidth;
            // Margen izquierdo para centrar el bloque de 60 caracteres
            int margenIzquierdo = Math.Max(0, (anchoConsola - anchoMaximo) / 2);
            string sangria = new string(' ', margenIzquierdo);

            string[] palabras = texto.Split(' ');
            string lineaActual = "";

            foreach (string palabra in palabras)
            {
                if ((lineaActual + palabra).Length > anchoMaximo)
                {
                    Console.WriteLine(sangria + lineaActual.TrimEnd());
                    lineaActual = "";
                }
                lineaActual += palabra + " ";
            }
            if (!string.IsNullOrWhiteSpace(lineaActual))
            {
                Console.WriteLine(sangria + lineaActual.TrimEnd());
            }
        }

        private static void Pausa()
        {
            Notificacion($"\n[Presiona ENTER para continuar...]", colorNotificacion);
            Console.ReadLine();
        }

        private static int PedirOpcion(int opcionMaxima)
        {
            int opcion;
            bool esValida = false;

            do
            {
                Notificacion($"Elige una opción entre 1 y {opcionMaxima}): ", colorNotificacion);

                string input = Console.ReadLine()?.Trim();

                // Validar que la entrada sea un número entero
                if (int.TryParse(input, out opcion) && opcion >= 1 && opcion <= opcionMaxima)
                {
                    esValida = true; // La opción es válida
                }
                else
                {
                    Notificacion("Error: Debes ingresar un número válido.", colorErrores);
                }
            } while (!esValida);
            return opcion;
        }

        private static int MostrarMenuYPedirOpcion(List<String> opciones)
        {
            Console.WriteLine();
            int longitudMaxima = 0;

            for (int i = 0; i < opciones.Count; i++)
            {
                string textoCompleto = $"{i+1}. {opciones[i]}";
                if (textoCompleto.Length > longitudMaxima)
                {
                    longitudMaxima = textoCompleto.Length;
                }
            }

            int anchoConsola = Console.WindowWidth;
            int margenIzquierdo = Math.Max(0, (anchoConsola - longitudMaxima) / 2);

            string sangrado = new string (' ', margenIzquierdo);

            List<String> lineasNumeradas = new List<string>();

            Console.ForegroundColor = colorOpciones;
            for (int i = 0; i < opciones.Count; i++)
            {
                Console.WriteLine($"{sangrado}{i+1}. {opciones[i]}");
            }
            Console.ResetColor();
            return PedirOpcion(opciones.Count);
        }

        private static void Notificacion(string texto, ConsoleColor colorNotificacion)
        {
            ConsoleColor colorAnteriro = Console.ForegroundColor;
            Console.ForegroundColor = colorNotificacion;
            EscribirCentrado(texto);
            Console.ForegroundColor = colorAnteriro;
        }

        private static void MiniJuegoCristalKyber()
        {
            Console.Clear();
            Console.ForegroundColor = colorSable;
            EscribirCentrado("SINTONIZACIÓN DEL CRISTAL KYBER");
            EscribirParrafoCentrado("Sostienes el cristal Kyber en la palma de tu mano. Para que encaje en la empuñadura, debes sintonizar tu mente con su frecuencia (un número del 1 al 10). Tienes 3 intentos. Cada fallo desestabilizará tu concentración (-2 Fuerza)\n");

            bool sintonizado = false;
            Random random = new Random();

            do
            {


                int frecuenciaCorrecta = random.Next(1, 11); // Número aleatoria entre 1 y 10
                int intentosRestantes = 3;


                while (intentosRestantes > 0 && !sintonizado)
                {
                    MostrarEstado();
                    EscribirCentrado($"\n[Intentos: {intentosRestantes}] Introduce la frecuencia de la Fuerza (1-10): ");

                    string entrada = Console.ReadLine()?.Trim();

                    if (int.TryParse(entrada, out int intento) && intento >= 1 && intento <= 10)
                    {
                        if (intento == frecuenciaCorrecta)
                        {
                            sintonizado = true;
                        }
                        else if (intento < frecuenciaCorrecta)
                        {
                            Notificacion("La resonancia del cristal es MÁS ALTA. Debes elevar tu concentración.", ConsoleColor.DarkRed);
                            intentosRestantes--;
                            Notificacion(ModificarRecurso(ref fuerza, -2, fuerzaMaxima, "Fuerza"), colorNotificacion);
                            Pausa();
                        }
                        else
                        {
                            Notificacion("La resonancia del cristal es MÁS BAJA. Debes calmar la intensidad de tu flujo.", ConsoleColor.DarkRed);
                            intentosRestantes--;
                            Notificacion(ModificarRecurso(ref fuerza, -2, fuerzaMaxima, "Fuerza"), colorNotificacion);
                            Pausa();
                        }
                    }
                    else
                    {
                        Notificacion("Por favor, introduce un número válido entre el 1 y el 10", colorErrores);
                        Pausa();
                    }
                }
                if (sintonizado)
                {
                    Notificacion("¡CRISTAL SINTONIZADO CON ÉXITO!", colorSable);
                    Notificacion("El cristal encaja a la perfección en el transmisor del sable.", colorSable);
                    Pausa();
                }
                else
                {
                    Notificacion("Has perdido la concentración. Vuelves a meditar...", colorNotificacion);
                    Pausa();
                }
            } while (!sintonizado);
        }

        private static void Descansar(int cantidad)
        {
            EscribirParrafoCentrado($"Decides descansar un momento.");
            Notificacion(ModificarRecurso(ref resistencia, cantidad, resistenciaMaxima, "Resistencia"), colorNotificacion);
            Notificacion(ModificarRecurso(ref fuerza, cantidad, fuerzaMaxima, "Fuerza"), colorNotificacion);
            Notificacion(ModificarRecurso(ref salud, cantidad, saludMaxima, "Salud"), colorNotificacion);
        }

        private static void UsarBotiquin(int cantidad)
        {
            numeroBotiquines--;
            EscribirCentrado($"Usas un botiquín para recuperar Salud.");
            Notificacion(ModificarRecurso(ref salud, cantidad, saludMaxima, "Salud"), colorNotificacion);
        }
    }
}