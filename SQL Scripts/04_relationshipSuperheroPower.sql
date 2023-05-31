USE SuperHeroesDB;
/*
Many to Many relationship between Superhero & Tables	
*/
CREATE TABLE SuperheroPower
(
	SuperheroId int NOT NULL,
	PowerId int NOT NULL,
	CONSTRAINT PK_Superhero_Power PRIMARY KEY (SuperheroId, PowerId),
	CONSTRAINT FK_Superhero
		FOREIGN KEY (SuperheroId) REFERENCES Superhero (Id),
	CONSTRAINT FK_Power
		FOREIGN KEY (PowerId) REFERENCES Power (Id)
);
