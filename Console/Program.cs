// Console.WriteLine(new MissingPassword().Challenge(new MissingPassword.Settings() { Mode = MissingPassword.Mode.CountZerosAfterMovement }));
// Console.WriteLine(new MissingPassword().Challenge(new MissingPassword.Settings() { Mode = MissingPassword.Mode.CountZerosWheneverEncountered }));

// Console.WriteLine(await new GiftShop().Challenge(new GiftShop.Settings() { Mode = GiftShop.Mode.PartIsRepeatedExactlyOnce }));
// Console.WriteLine(await new GiftShop().Challenge(new GiftShop.Settings() { Mode = GiftShop.Mode.PartIsRepeatedAtLeastOnce }));

Console.WriteLine($"Should be 17554: {await new AdventOfCode2025.Day3.Elevators().Challenge(new AdventOfCode2025.Day3.Elevators.Settings() { BatteriesToEnable = 2} )}");
Console.WriteLine($"Should be X: {await new AdventOfCode2025.Day3.Elevators().Challenge(new AdventOfCode2025.Day3.Elevators.Settings() { BatteriesToEnable = 12} )}");
