USE SuperHeroesDB;
/*
One Superhero to many assistant relationship
*/
ALTER TABLE Assistant
	ADD SuperheroId int NOT NULL,
	CONSTRAINT FK_SuperheroAssistant
	FOREIGN KEY (SuperheroId) REFERENCES Superhero(Id); 
