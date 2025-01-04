# Golden Raspberry Awards API

API RESTful para consultar informações sobre os vencedores da categoria "Pior Filme" do Golden Raspberry Awards. A aplicação carrega dados de um arquivo CSV, armazena-os em um banco de dados em memória e expõe endpoints para consulta.

---

## **Pré-requisitos**

Antes de começar, certifique-se de ter os seguintes itens instalados:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Git](https://git-scm.com/)
- Um editor de texto ou IDE, como [Visual Studio Code](https://code.visualstudio.com/) ou [Visual Studio](https://visualstudio.microsoft.com/).

---

## **Configuração do Projeto**

1. Clone o repositório:
   ```bash
   git clone https://github.com/viniciuslopes/GoldenRaspberryAPI.git
   cd GoldenRaspberryAPI

2. Instale as dependências do projeto:
   ```bash
   dotnet restore
  

3. Certifique-se de que o arquivo CSV necessário está no diretório Resources:
    - O arquivo esperado é movielist.csv.<br/>
    - O caminho padrão é: GoldenRaspberryAPI/Resources/movielist.csv.
---

### Como Executar a Aplicação

1. Limpe, compile e execute o projeto:
   ```bash
   dotnet clean
   dotnet run

2. A aplicação estará disponível no endereço:
   http://localhost:5000

3. Acesse a documentação Swagger para interagir com os endpoints:
   ```bash
   http://localhost:5000/swagger
---

### Endpoints Disponíveis
1. Obter Intervalos de Prêmios
URL: GET /api/movies/award-intervals

Exemplo de Resposta:

json

{
  "min": [
    {
      "producer": "Producer A",
      "interval": 1,
      "previousWin": 2000,
      "followingWin": 2001
    }
  ],
  "max": [
    {
      "producer": "Producer B",
      "interval": 10,
      "previousWin": 1990,
      "followingWin": 2000
    }
  ]
}

---

### Como Executar os Testes
1. Navegue até o diretório raiz do projeto.

2. Execute os testes com o comando:
   ```bash
   dotnet test

3. O resultado dos testes será exibido no terminal.

---

### Estrutura do Projeto

plaintext

GoldenRaspberryAPI/
├── Controllers/               # Controladores da API
├── Data/                      # Banco de dados em memória
├── Model/                     # Modelos utilizados no projeto
├── Services/                  # Lógica de negócio
├── Tests/                     # Testes de integração
├── Resources/movielist.csv    # Arquivo CSV com os dados
└── README.md                  # Instruções do projeto

---

### Tecnologias Utilizadas
   ASP.NET Core Web API<br/>
   Entity Framework Core (banco de dados em memória)<br/>
   CsvHelper (leitura de arquivos CSV)<br/>
   Swagger (documentação da API)

---

### Observações
   A aplicação foi projetada para ser portátil. Nenhuma instalação de banco de dados externo é necessária.
   O banco de dados é populado automaticamente ao iniciar a aplicação.

---

### Contribuição
   Contribuições são bem-vindas! Siga os passos abaixo para contribuir:

1. Faça um fork do repositório.
2. Crie uma branch para sua feature:
   ```bash
   git checkout -b minha-feature

3. Faça suas alterações e commite:
   ```bash
   git commit -m "Adiciona nova feature"

4. Faça um push para sua branch:
   ```bash
   git push origin minha-feature

5. Abra um Pull Request no repositório original.

---

### Licença
Este projeto está licenciado sob a MIT License.

---
