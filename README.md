# Livraria Web API
Este é um projeto de uma API RESTful para uma Livraria, desenvolvido utilizando .NET 8. A API oferece operações CRUD para gerenciar livros em um banco de dados, usando o EntityFramework Core para mapeamento objeto-relacional e banco de dados SQL SERVER.

## Endpoints da API
### Autor
`GET /api/Autor/ListarAutores:` Retorna todos os autores.

`GET /api/Autor/BuscarAutorPorIdLivro/{idLivro}:` Retorna um autor específico pelo ID do livro.

`POST /api/Autor/CriarAutor:` Adiciona um novo autor.

`PUT /api/Autor/EditarAutor:` Atualiza um autor existente.

`DELETE /api/Autor/ExcluirAutor/{idAutor}:` Remove um autor pelo ID.
### Livro
`GET /api/Livro/ListarLivros:` Retorna todos os livros.

`GET /api/Livro/BuscarLivroPorId/{idLivro}:` Retorna um livro específico pelo ID.

`GET /api/Livro/BuscarLivroPorIdAutor/{idAutor}:` Retorna um livro específico pelo ID do autor.

`POST /api/Livro/CriarLivro:` Adiciona um novo livro.

`PUT /api/Livro/EditarLivro:` Atualiza um livro existente.

`DELETE /api/Livro/ExcluirLivro/{idLivro}:` Remove um livro pelo ID.


