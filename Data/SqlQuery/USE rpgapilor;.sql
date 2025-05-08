USE rpgapilor;

select * from armas;


select p.Id, p.Nome, a.Nome, a.dano 
from Personagens as p, armas as a 
WHERE
a.personagemId = p.Id;