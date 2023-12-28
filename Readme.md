# Welcome Mail Service

Desenvolvido usando C# com DotNet, trata-se de um micro-serviço para envio de e-mails, feito com o entuito de ser implementado junto a aplicação de Stock Managment, onde ao ser registrado um novo usuário na aplicação será disparado um e-mail de boas-vindas.
## Variáveis de Ambiente

Para rodar esse projeto, você vai precisar adicionar as seguintes variáveis de ambiente no seu .env

- `MAIL_FROM`

- `MAIL_FROM_PASSWORD`

Assim como demostrado no arquivo de exemplo `.env.example`

## Documentação da API

#### Enviar email para o usuário

```http
  POST /NewbieUsers
```

| Corpo   | Tipo       | Descrição                           |
| :---------- | :--------- | :---------------------------------- |
| `email` | `string` | **Obrigatório**. Endereço de e-mail do novo usuário |
| `name` | `string` | **Obrigatório**. Nome do novo usuário |

## Autores

- [@oLeoBarreto](https://github.com/oLeoBarreto)

