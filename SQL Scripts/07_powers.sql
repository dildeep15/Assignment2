USE SuperHeroesDB;
INSERT INTO Power (Name, Description)
VALUES('CanFly', 'Hero has the ablility to fly'),
('WearSuit','Hero wears a suit'),
('SpiderWeb','Hero can shoot spider web'),
('BatMobile','Hero has a batmobile');

INSERT INTO SuperheroPower(SuperheroId, PowerId)
Values(1,1),
(1,2),
(2,2),
(2,3),
(3,3),
(3,4);