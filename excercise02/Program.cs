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

// Orbital Data for Earth (around the Sun)
OrbitalData earthOrbit = new OrbitalData(
	celestialBody: earth,
	orbitalPeriod: 1.0,
	semiMajorAxis: 1.0,
	eccentricity: 0.0167
);

// Orbital Data for Moon (around Earth)
OrbitalData moonOrbit = new OrbitalData(
	celestialBody: moon,
	orbitalPeriod: 0.0748,
	semiMajorAxis: 0.0026,
	eccentricity: 0.0549
);

// Orbital Data for Halley's Comet
OrbitalData halleyOrbit = new OrbitalData(
	celestialBody: halley,
	orbitalPeriod: 76.0,
	semiMajorAxis: 17.8,
	eccentricity: 0.967
);

Console.WriteLine("\n--- Orbital Data ---");
Console.WriteLine(earthOrbit);
Console.WriteLine(moonOrbit);
Console.WriteLine(halleyOrbit);
