## Integrantes grupo CPA

- André Rohregger Machado - RM98110- TURMA 2TDSPV

- Daniele Vargas de Lima -RM99400- TURMA 2TDSPY

- Daniel Alves de Souza -RM552310- TURMA 2TDSA

- Marcela S Moreira -RM552051- TURMA 2TDSPY

- Nathalia Braga do Nascimento - RM552221- TURMA 2TDSPY

## O projeto

O projeto foi desenvolvido no contexto de uma API para previsao do clima, oferecendo dados como temperatura, umidade e previsões de chuva.

## API

A API utilizada é a do Open Weather Map, que fornece informaçoes de previsao climática.
![image](https://github.com/user-attachments/assets/a345eb72-b087-4854-9c78-232986118851)

## Modelo do ML.NET

O modelo utilizado na prática de Machine Learning visa prever se irá ou nao chover em um determinado dia, utilizando um csv com dados históricos.

![image](https://github.com/user-attachments/assets/19cb0bba-9642-4b0c-83f6-ad2dafc7179d)

## Práticas de Clean Code e SOLID utilizadas

- Nomes descritivos: utilizei nomes que descrevem claramente o propósito das classes e métodos, como os métodos Recomendar e ObterPrevisao
- Tratamento de erros: dentro dos controllers utilizei tratamentos de erros em cada uma das requisicoes, entao se ocorrer algum erro será notificado o motivo
- Interface Segregation Principle: criei interfaces especificas em vez de uma única interface grande
- Dependency Inversion Principle: realizei injecao de dependencias
- Open/Closed Principle: utilizei interfaces e heranças

## Testes implementados

- Método adicionar previsao: testa a adiçao de uma previsao de clima e verifica se as propriedas retornadas correspondem às esperadas
- Método editar previsao: testa se a edicao de uma previsao de clima foi bem sucedida
- Método obter previsao por ID: verifica se ao informar o ID de uma previsao ela será retornada
- Método obter todas as previsoes: verifica se ao solicitar o retorno de todas as previsoes a lista toda será retornada
