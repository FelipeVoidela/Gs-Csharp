# GS-Csharp – API REST (.NET 8) – Comunidades de Aprendizagem Colaborativa e Global

Projeto do tema “O Futuro do Trabalho”, focado em comunidades colaborativas de aprendizagem com os recursos: Professor, Aluno, Comunidade, Curso e Inscrição. A API segue boas práticas REST, possui versionamento (v1 e v2), documentação Swagger e persistência em Oracle via Entity Framework Core.

Principais recursos
- Boas práticas REST: verbos HTTP corretos (GET, POST, PUT, DELETE) e status codes padronizados.
- Versionamento: rotas /api/v1 e /api/v2. Selecionável no Swagger.
- Persistência: Oracle Database + EF Core 8 (mapeamento e migrations).
- Swagger/OpenAPI: documentação interativa para v1 e v2.
- Clean Code: organização por camadas e DTOs.

Stack
- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core 8
- Oracle.EntityFrameworkCore + Oracle.ManagedDataAccess.Core
- API Versioning (AspNetApiVersioning)
- Swashbuckle (Swagger)

Estrutura de pastas (simplificada)
- GS-Csharp/
  - Application/DTOs
  - Controllers/
    - v1: Alunos, Professores, Comunidades, Cursos, Inscricoes
    - v2: Alunos, Professores, Comunidades, Cursos (enriquecido), Inscricoes
  - Domain/Entities: Aluno, Professor, Comunidade, Curso, Inscricao
  - Infrastructure/Persistence: AppDbContext, DesignTimeDbContextFactory
  - Oracle/OracleSettings.cs
  - Program.cs

Banco de dados (Oracle)
- Tabelas mapeadas: ALUNOS, PROFESSORES, COMUNIDADES, CURSOS, INSCRICOES.
- String de conexão externa: arquivo GS-Csharp/appsettings.Oracle.json (não commitado). Exemplo:
  User Id=SEU_USUARIO;Password=SUA_SENHA;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=HOST)(PORT=1521))(CONNECT_DATA=(SID=orcl)))

Migrations (EF Core)
1) Instale a ferramenta EF (se necessário)
   dotnet tool install --global dotnet-ef --version 8.0.10
2) Criar a migration inicial
   dotnet ef migrations add InitialCreate --project GS-Csharp/GS-Csharp.csproj
3) Aplicar no Oracle
   dotnet ef database update --project GS-Csharp/GS-Csharp.csproj

Observações
- O Program.cs chama db.Database.Migrate() no startup. Depois de criada a migration, ao iniciar a API as pendências são aplicadas automaticamente.
- Certifique-se de conectar no CLIENTE Oracle com o mesmo usuário do appsettings.Oracle.json para ver as tabelas.

Como executar
1) Pré‑requisitos: .NET 8 SDK e acesso a um Oracle (host/porta/SID ou ServiceName, usuário, senha).
2) Configure GS-Csharp/appsettings.Oracle.json com sua ConnectionString.
3) Restaurar e compilar
   dotnet restore
   dotnet build GS-Csharp/GS-Csharp.csproj
4) Rodar a API
   dotnet run --project GS-Csharp/GS-Csharp.csproj
5) Swagger
   - https://localhost:PORT/swagger
   - Seletor no topo para “GS-Csharp v1” e “GS-Csharp v2”.

Rotas (v1)
- Alunos:    GET/POST /api/v1/alunos, GET/PUT/DELETE /api/v1/alunos/{id}
- Professores: GET/POST /api/v1/professores, GET/PUT/DELETE /api/v1/professores/{id}
- Comunidades: GET/POST /api/v1/comunidades, GET/PUT/DELETE /api/v1/comunidades/{id}
- Cursos:     GET/POST /api/v1/cursos, GET/PUT/DELETE /api/v1/cursos/{id}
- Inscrições: GET/POST /api/v1/inscricoes, DELETE /api/v1/inscricoes/{id}

Rotas (v2)
- Mesmas rotas de v1 para Alunos/Professores/Comunidades/Cursos/Inscrições.
- Melhorias:
  - Cursos v2: GET retorna também TotalInscritos por curso; GET por id idem.
  - Inscrições v2: suporta filtros por alunoId e cursoId em GET; evita duplicidade no POST.

Status codes padrão
- 200 OK (GET), 201 Created (POST), 204 No Content (PUT/DELETE), 400 Bad Request, 404 Not Found, 409 Conflict (duplicidade de inscrição v2), 500 para erros inesperados.

Problemas comuns
- “No migrations were applied”: crie a migration (ver seção Migrations).
- Erro ao copiar binário na build: feche a execução anterior antes de recompilar.
- HTTPS local: dotnet dev-certs https --trust.

Licença
- Uso educacional/acadêmico.
