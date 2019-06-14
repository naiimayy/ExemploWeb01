CREATE TABLE frutas (
	id INT PRIMARY KEY IDENTITY(1,1),
	nome VARCHAR(100),	
	preco DECIMAL (8,2)
);


INSERT INTO frutas (nome,preco) VALUES
('Kiwi' , 10.00) , 
('Banana' , 15.00);