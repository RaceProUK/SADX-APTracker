﻿using System.Text;
using RPS.SADX.PopTracker.Generator.Utilities;

Console.OutputEncoding = Encoding.UTF8;

var dataPackage = await DataPackageLoader.LoadDataPackage();
if (dataPackage is null)
    Console.WriteLine($"Error loading datapackage");
else
{
    Console.WriteLine($"Datapackage checksum: {dataPackage.Checksum}");
    Console.WriteLine($"Number of locations found: {dataPackage.LocationNameToId.Count}");
    Console.WriteLine($"Number of items found: {dataPackage.ItemNameToId.Count}");
    Console.WriteLine();

    Console.WriteLine("Generating logic scripts…");
    await AccessRulesGenerator.Generate();
    Console.WriteLine("Logic scripts generated");

    Console.WriteLine($"Generating locations…");
    await LocationGenerator.Generate(dataPackage.LocationNameToId);
    Console.WriteLine("Locations generated");

    Console.WriteLine("Generating items…");
    await ItemGenerator.Generate(dataPackage.ItemNameToId);
    Console.WriteLine("Items generated");
}