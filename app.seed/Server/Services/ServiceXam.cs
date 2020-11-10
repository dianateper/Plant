using Models.Model;
using Server.Contracts;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    class ServiceXam : IContractXam
    {
        MachineRepository MachineRepository = new MachineRepository();
        PositionRepository PositionRepository = new PositionRepository();

        public LinkedList<Position> GetPath(Position start, Position end)
        {
            List<Position> positions = PositionRepository.GetAllPosition();
            
            Dijkstra.GraphBuilder graphBuilder = new Dijkstra.GraphBuilder();
            Dijkstra.Graph<Position> graph = SetGraph(positions);
            
            return graphBuilder.Search(start, end, graph);
        }

        Dijkstra.Graph<Position> SetGraph(List<Position> positions)
        {
            Dijkstra.Graph<Position> graph = new Dijkstra.Graph<Position>();

            positions.ForEach(i =>
            {
                graph.AddNode(i);
            });

            positions.ForEach(i =>
            {
                positions.ForEach(j =>
                {
                    if (
                    i.X == j.X + 1 ||
                    i.X == j.X - 1 ||
                    i.Y == j.Y + 1 ||
                    i.Y == j.Y - 1
                    )
                    {
                        graph.AddEdge(i, j, 1);
                    }
                    else if ((i.X == j.X + 1 && i.Y == j.Y + 1) ||
                    (i.X == j.X - 1 && i.Y == j.Y + 1) ||
                    (i.X == j.X + 1 && i.Y == j.Y - 1) ||
                    (i.X == j.X - 1 && i.Y == j.Y - 1))
                    {
                        graph.AddEdge(i, j, Math.Sqrt(2));
                    }
                });
            });

            return graph;
        }



        public List<Machine> GetAllMachines()
        {
            return MachineRepository.GetAllMachines();
        }


        public string Greeting()
        {
            return "Hello!";
        }
    }
}
