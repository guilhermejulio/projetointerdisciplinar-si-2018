using projetointerdisciplinar;
using System;
using System.Collections.Generic;

namespace projetointerdisciplinas
{
    class Program
    {
        public static List<string> destinos = new List<string>() 
            {"Belo Horizonte", "Salvador", "Orlando","New York","Rio de Janeiro", "São Paulo", "Hong Kong","Bangkok","Londres","Brasilia","Campinas","Recife","Fortaleza","Curitiba","Florianopolis","Belém","Vitoria","Goiania","Natal","Maceio","São Luís","Teresina","Uberlandia"};
        /// <summary>
        /// Metodo auxiliar que imprime os voos que tem vagas de assento ou na fila.
        /// </summary>
        private static void PrintFlights(Airline linhaX)
        {
            Console.Clear();
            Console.Write("\nLista de voos com vagas: ");
            foreach (KeyValuePair<int, Fligth> airlineFlights in linhaX.airlineFlights)
            {
                if (airlineFlights.Value.status == "Existem vagas")
                {
                    Console.Write("\n\nVoo Numero: " + airlineFlights.Key);
                    Console.Write("\nDestino: " + airlineFlights.Value.destino);
                    Console.Write("\nNumero de assentos vagos: " + airlineFlights.Value.EmptySeats());
                }
                else
                {
                    if (airlineFlights.Value.status == "Vagas na fila")
                    {
                        Console.Write("\n\nVoo Numero: " + airlineFlights.Key);
                        Console.Write("\nDestino: " + airlineFlights.Value.destino);
                        Console.Write("\nNumero de assentos vagos: 0");
                        Console.Write("\nNumero de vagas na fila: " + airlineFlights.Value.NumberSeatsQueue());
                    }
                }
            }
        }

        /// <summary>
        /// Metodo que inicializa dos dados do programa de reserva de passagens aereas.
        /// </summary>
        private static void InicializarDados(Airline linhaAerea)
        {
            //Para fins didaticos, gerar passageiros aleatorios para preencher 70% das vagas ou seja 7 passageiros por voo
            Random r = new Random();

            foreach (int numberFlight in linhaAerea.airlineFlights.Keys)
            {
                for (int cont = 1; cont <= 7; cont++)
                {
                    int passenger_ID = r.Next(100000000, 999999999); //gerando cpf aleatorio 9 digitos
                    linhaAerea.MakeReservation(numberFlight, passenger_ID.ToString()); // realizando reserva com assento aleatorio
                }
            }
            
            
            
        }
        /// <summary>
        /// Metodo que exibe um menu interativo para o usuario reservar sua passagem.
        /// </summary>
        private static void MenuReserva(Airline linhaX)
        {
            Console.Clear();
            Console.Write("\nDigite seu CPF com (9) digitos: ");
            string passenger_ID = Console.ReadLine();
            PrintFlights(linhaX); // Chamada do metodo que imprime todos os voos que possuem vagas ou possuem vagas na fila
            Console.Write("\n\nDigite o voo que deseja: ");
            int flightNumber = int.Parse(Console.ReadLine());

            Console.Clear();

            if (linhaX.ExistsFlight(flightNumber)) // Regra 1 - Verficiação se o voo Existe
            {
                if (!linhaX.airlineFlights[flightNumber].hasReservation(passenger_ID)) //Regra 4 - Verificar se o passageiro já tem reserva no voo
                {
                    if (linhaX.airlineFlights[flightNumber].FullFligth())  // Regra 2 - Verficiar se o Voo está lotado se estiver reserva na fila
                    {
                        if (linhaX.airlineFlights[flightNumber].BookInLine(passenger_ID))
                        {
                            Console.Write("\nA rezerva na fila de espera foi realizada com sucesso!!\nPrescione <enter>...");
                        }
                        else //Regra 3 -  Verificar se a fila está lotada, dentro do metodo reservar na fila.
                        {
                            Console.Write("\nErro, reserva não realizada.\nO voo e a fila de espera estão lotados!!\nPrescione <enter>...");
                        }
                    }
                    else  //se o voo não está lotado, tem assentos disponiveis
                    {
                        Console.Clear();
                        //chamada do metodo para imprimir os assentos vagos
                        Console.Write("\nAssentos disponiveis: " + linhaX.airlineFlights[flightNumber].VacantSeats());

                        Console.Write("\n\nEscolha um assento, ou prescione <enter> para assento aleatorio: ");
                        string s = Console.ReadLine();

                        int seat;

                        if (int.TryParse(s, out seat)) //se o usuario escolheu um assento
                        {
                            if (linhaX.MakeReservation(flightNumber, passenger_ID, seat)) // Se reservou
                            {
                                Console.Clear();
                                Console.Write("\nRezerva realizada com sucesso!");
                            }
                            else //se não reservou
                            {
                                Console.Clear();
                                Console.Write("A reserva não pode ser efetuada, entrada de dados incorreta!!\nPrescione <enter>...");
                            }
                        }
                        else //se o usuario deseja um assento aleatorio.
                        {
                            Console.Clear();
                            if (linhaX.MakeReservation(flightNumber, passenger_ID)) //Se reservou
                            {
                                Console.Write("\nRezerva realizada com sucesso!!\nPrescione <enter>");
                            }
                            else //Se não reservou
                            {
                                Console.Write("A reserva não pode ser efetuada, entrada de dados incorreta!!\nPrescione <enter>...");
                            }
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.Write("\n\nErro, já existe reserva para este CPF no voo\nPrescione <enter>...");
                }

            }
            else // Se o voo informado não existe
            {
                Console.Clear();
                Console.Write("\n\nO voo escolhido não existe!");
            }

        }

        /// <summary>
        /// Menu principal
        /// </summary>
        public static void MenuPrincipal(ref int opcao)
        {
            Console.Clear();
            Console.Write("\n|-------------------------------------------|");
            Console.Write("\n| Sistema de Reservas de Passangens aereas  |");
            Console.Write("\n|-------------------------------------------|");
            Console.Write("\n| Menu principal:                           |");
            Console.Write("\n|                                           |");
            Console.Write("\n| [1] - Inicializar Dados                   |");
            Console.Write("\n| [2] - Incluir reserva                     |");
            Console.Write("\n| [3] - Excluir reserva                     |");
            Console.Write("\n| [4] - Imprimir reserva                    |");
            Console.Write("\n| [0] - Encerrar programa                   |");
            Console.Write("\n|                                           |");
            Console.Write("\n|-------------------------------------------|");
            Console.Write("\n\nDigite a opção desejada: ");
            
            bool valido = int.TryParse(Console.ReadLine(), out opcao);
            if (!valido || (opcao < 0 || opcao > 4) )
            {
                Console.Clear();
                Console.Write("\nA entrada digitada está incorreta, aperte <enter> e tente de novo.");
                Console.ReadKey();
                MenuPrincipal(ref opcao);
            }
        }

        /// <summary>
        ///  Menu de impressão
        /// </summary>
        public static void MenuImpressao(ref int opcao)
        {
            Console.Clear();
            Console.Write("\n|-------------------------------------------|");
            Console.Write("\n| Sistema de Impressão                      |");
            Console.Write("\n|-------------------------------------------|");
            Console.Write("\n| Opções:                                   |");
            Console.Write("\n|                                           |");
            Console.Write("\n| [1] - Imprimir dados de um voo            |");
            Console.Write("\n| [2] - Imprimir ocupação de todos voos     |");
            Console.Write("\n| [3] - Imprimir todos dados de todos voos  |");
            Console.Write("\n|                                           |");
            Console.Write("\n|-------------------------------------------|");
            Console.Write("\n\nDigite a opção desejada: ");
            bool valido = int.TryParse(Console.ReadLine(), out opcao);
            if (!valido || (opcao < 1 || opcao > 3))
            {
                Console.Clear();
                Console.Write("\nA entrada digitada está incorreta, aperte <enter> e tente de novo.");
                Console.ReadKey();
                MenuImpressao(ref opcao);
            }
        }

        /// <summary>
        /// Modulo principal
        /// </summary>
        static void Main(string[] args)
        {
            int opcao=0;
            Airline linhaAerea = new Airline(destinos);
            bool dadosInicializados = false; //variavel para controlar se os dados ja foram inicializados.
            do
            {
                MenuPrincipal(ref opcao);

                switch (opcao)
                {
                    case 1: //inicializar dados 
                        if (dadosInicializados)
                        {
                            Console.Clear();
                            Console.WriteLine("Os dados ja foram inicializados!!!\n\nPrescione <enter>... ");
                        }
                        else
                        {
                            Console.Clear();
                            InicializarDados(linhaAerea);
                            dadosInicializados = true;
                            Console.WriteLine("Dados inicializados, prescione <enter>...");
                        }
                        break;
                    case 2:   //incluir reserva
                        if (dadosInicializados)
                            MenuReserva(linhaAerea);
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Erro! Os dados ainda não foram inicializados, utilize a opção inicializar dados!!\nPrescione <enter>...");
                        }
                        break;

                    case 3: //excluir reserva
                        if (dadosInicializados)
                        {
                            Console.Clear();
                            Console.Write("\nDigite o numero do voo: ");
                            int voo = int.Parse(Console.ReadLine());

                            if (linhaAerea.ExistsFlight(voo)) //Regra 1 - Verificar se o voo existe.
                            {
                                Console.Write("\nDigite o CPF: ");
                                string cpf = Console.ReadLine();

                                if (linhaAerea.DeleteReservation(voo, cpf)) //Regra 2 - Se o passageiro existe no voo, dentro do metodo.
                                {
                                    Console.Clear();
                                    Console.Write("\nReserva excluida com sucesso!!\n\nPrescione <enter>...");
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.Write("\nO passageiro não está no voo informado!!\n\nPrescione <enter>...");
                                }
                            }
                            else
                            {
                                Console.Clear();
                                Console.Write("\n\nO voo digitado não existe...");
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Erro! Os dados ainda não foram inicializados, utilize a opção inicializar dados!!\nPrescione <enter>...");
                        }

                        break;
                    case 4: //Imprimir reserva, submenu                        
                        MenuImpressao(ref opcao);


                        switch (opcao)
                        {
                            case 1: //Imprimir dados de um voo especifico
                                Console.Clear();
                                Console.Write("\nDigite o numero do voo: ");
                                int numVoo = int.Parse(Console.ReadLine());
                                if (linhaAerea.ExistsFlight(numVoo))
                                {
                                    linhaAerea.Print(numVoo);
                                }
                                break;
                            case 2: //Imprimir ocupação de determinado voo
                                Console.Clear();
                                linhaAerea.PrintOccupation();
                                break;
                            case 3: //Imprimir todos dados de todos voos 
                                linhaAerea.PrintAll();
                                break;
                        }





                        break;
                }


                Console.ReadKey();
            } while (opcao > 0);
        }
    }
}
