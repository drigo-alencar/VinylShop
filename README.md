# VinylShop


# Intro

Queria agradecer ao pessoal da BeBlue pela oportunidade, espero que gostem do projeto :smile:!

[Aqui](https://documenter.getpostman.com/view/1543073/S11NMwjd) tem a documentacao da api.

Espero o contato de voces! =)

# Como fazer o projeto funcionar

Assumindo que o git clone ja tenha sido feito, e ja possua uma chave da API do Spotify basta seguir esses passos:

## Antes de tudo...
Voce vai precisar criar um user secret...
Pra que isso? Assim voce nao precisa ficar preocupado de alguem usar sua chave de API do Spotify ou de subir por engano no GitHub
E so seguir essa receita que voce ficara bem...rs

### Metodo 1
Se voce estiver usando Visual Studio vai ter que seguir esta lista de passos enorme:

1.  click direito sobre o projeto `BeBlue.Api.VinylShop.Presentation`
2.  click na opcao `Manage User Secrets`
3.  o VS vai abrir um arquivo `json`
4.  coloque o seguinte conteudo neste arquivo 
`{
  "SpotifySettings": {
    "ClientId": "SEU_CLIENT_ID_AQUI",
    "Secret": "SEU_SECRET_AQUI"
  }
}`
5. salve e pronto

### Metodo 2
Aqui voce vai suar um pouco mas vamos la:

1. va ate a pasta onde se encontra o `BeBlue.Api.VinylShop.Presentation.csproj` abra ele com seu editor de texto favorido ~~VSCode~~ :smile:
2. crie um guid com qualquer ferramenta que quiser
3. adicione isso `<UserSecretsId>Seu Guid</UserSecretsId>` ao `csproj` logo abaixo da tag `TargetFramework`
4. abra um shell no diretorio do `csproj` e execute `dotnet user-secrets SpotifySettings:ClientId = "SEU_CLIENT_ID_AQUI"`
4. e mais uma vez `dotnet user-secrets SpotifySettings:Secret = "SEU_SECRET_AQUI"`
5. Esse e so pra lista parecer grande, parabens.

## Database

O projeto utiliza o MongoDB como database, se estiver usando localmente configure o endereco e porta no arquivo `appsettings.json`

## Build & Run :running: :see_no_evil:
Agora a parte boa:

### Sem Docker

1. no diretorio `BeBlue.Api.VinylShop.Presentation`
2. execute `dotnet build -o app`
3. e agora execute `dotnet app\BeBlue.Api.VinylShop.Presentation.dll`

### Com Docker
1. no diretorio da solution `BeBlue.Api.VinylShop.sln`
2. execute `docker-compose up`