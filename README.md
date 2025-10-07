## Felipe Hideki Iwasa - Rm98323
## Enzo Vasconcelos - Rm550702

Passo 1: Configuração do Banco de Dados Oracle

Criação das Tabelas:

As tabelas Cliente, Produto, Pedido e a tabela de relacionamento PedidoProduto foram criadas no banco de dados Oracle.

As tabelas armazenam informações de clientes, produtos, pedidos e a relação entre produtos e pedidos (quantidade de produtos em um pedido).

Passo 2: Lógica de Negócio - Services

Serviço ClienteService:

Responsável pela validação do login dos clientes.

O método ValidarLoginAsync recebe o login e senha e verifica se o cliente existe no banco de dados. Se o login for válido, retorna true, caso contrário, retorna false.

Serviço ProdutoService:

Responsável pelas operações CRUD de produtos.

Inclui métodos para criar, atualizar, excluir e listar os produtos, além de garantir que o estoque do produto nunca seja negativo.

Serviço PedidoService:

Responsável pelas operações CRUD de pedidos.

Um pedido pode ser criado, atualizado e excluído. No entanto, um pedido entregue não pode ser alterado ou excluído.

A relação entre pedidos e produtos é feita através da tabela PedidoProduto, onde os produtos são associados ao pedido e a quantidade é armazenada.

Passo 3: Configuração do MVC no Cp2_CsharpUI

Controlador LoginController:

A página inicial de login é gerenciada pelo LoginController, que exibe a view de login e processa o login.

Quando o usuário fornece o login e senha, o controlador chama o método ValidarLoginAsync do ClienteService para validar as credenciais. Se forem válidas, o usuário é redirecionado para a página principal (Home).

Controlador HomeController:

Após um login bem-sucedido, o usuário é redirecionado para a página inicial onde ele pode visualizar os produtos e pedidos.

Esse controlador pode ser expandido para gerenciar a navegação entre as páginas de produtos, pedidos, etc.

Passo 4: Views (Interface de Usuário - UI)

Página de Login:

A view Login/Index.cshtml exibe um formulário de login simples com campos para login e senha.

Se o login for inválido, uma mensagem de erro é exibida ao usuário. Caso contrário, ele é redirecionado para a página inicial.

Página Inicial:

A view Home/Index.cshtml exibe a página inicial com um bem-vindo após um login bem-sucedido. Aqui o usuário poderá acessar as informações sobre os produtos e pedidos.

Swagger:

O Swagger UI foi configurado para documentar a API no Cp2_CsharpUI, onde você pode visualizar e testar os endpoints da API de forma interativa.

Passo 5: Fluxo do Programa

O usuário acessa a página de login.

Ele insere suas credenciais (login e senha).

O controlador LoginController valida as credenciais com o ClienteService.

Se o login for válido, o usuário é redirecionado para a página inicial onde ele pode visualizar os produtos e pedidos.

O Swagger UI oferece uma interface para testar a API diretamente no navegador.

O usuário pode interagir com o CRUD de pedidos e produtos.

Resumo do Fluxo:

Login → Validação com ClienteService → Redireciona para a página inicial

CRUD de Produtos → Adicionar/Atualizar/Excluir produtos (via ProdutoService)

CRUD de Pedidos → Criar/Atualizar/Excluir pedidos (via PedidoService)

Swagger → Documentação e testes da API
