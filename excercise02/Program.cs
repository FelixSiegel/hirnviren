using RaumfahrtMission;

Star sun = new("Sun", 10001, SpectralClass.G, -26.74f);
Planet earth = new("Earth", 20001, 1.0f, 10001);
Moon moon = new("Moon", 30001, 0.0748f, 20001);
var halley = new Planet(
	name: "Halley's Comet",
	catalogNumber: 40001,
	orbitalPeriod: 76.0f,
	catalogNumberReference: 10001
);

Console.WriteLine(sun);
Console.WriteLine(earth);
Console.WriteLine(moon);
Console.WriteLine(halley);
