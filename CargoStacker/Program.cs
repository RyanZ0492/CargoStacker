using CargoStacker.Models;
using CargoStacker.Services;
using System;

class Program
{
    static void Main()
    {
        CargoShip ship = new CargoShip(200000); // Max 200 tons

        ship.AddContainer(new StandardContainer(30000)); // 30 tons
        ship.AddContainer(new ValuableContainer(25000)); // 25 tons

        Console.WriteLine($"Ship Balanced? {ship.IsBalanced()}");
    }
}
