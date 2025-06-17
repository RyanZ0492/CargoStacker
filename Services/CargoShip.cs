//using CargoStacker.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace CargoStacker.Services
//{
//    public class CargoShip
//    {
//        public List<Container> Containers { get; private set; }
//        public int MaxWeight { get; private set; }

//        public CargoShip(int maxWeight)
//        {
//            MaxWeight = maxWeight;
//            Containers = new List<Container>();
//        }

//        public bool AddContainer(Container container, List<Container> stackedOn = null)
//        {
//            StackingService manager = new StackingService();

//            if (stackedOn != null)
//            {
//                foreach (var bottomContainer in stackedOn)
//                {
//                    if (!manager.CanStack(bottomContainer, container))
//                    {
//                        Console.WriteLine("Cannot stack this container.");
//                        return false;
//                    }
//                }
//            }

//            if (manager.CanBePlacedInFront(container))
//            {
//                Console.WriteLine("This container must be placed in the first row.");
//            }

//            Containers.Add(container);
//            return true;
//        }


//        public bool IsBalanced()
//        {
//            int leftWeight = Containers.Where((c, i) => i % 2 == 0).Sum(c => c.Weight);
//            int rightWeight = Containers.Where((c, i) => i % 2 == 1).Sum(c => c.Weight);
//            int totalWeight = Containers.Sum(c => c.Weight);

//            return Math.Abs(leftWeight - rightWeight) <= totalWeight * 0.2;
//        }
//    }
//}
