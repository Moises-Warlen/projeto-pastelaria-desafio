var usuarios = (function () {
    // Configurações e URLs para comunicação com o servidor
    var config = {
        urls: {
            getGridUsuarios: '',
            buscarTelaAtualizarUsuario: '',
            deletarUsuario: '',
            deletarTelefone: '',
            deletarEndereco: '',
            post: '',
            linhaGridTelefone: '',
            linhaGridEndereco: '',
            getGridTarefas: '',
            buscarTelaAddTarefa: '',
            concluirTarefa: '',
            excluirTarefa: ''
        }
    };

    // Função para buscar e exibir o grid de usuários
    var getGridUsuarios = function () {
        $.get(config.urls.getGridUsuarios).done(function (data) {
            $('#div-grid-usuarios').html(data);
        }).fail(function () {
            // Adicione aqui um tratamento de erro, se necessário
        });
    };

    // Função para buscar e exibir o grid de tarefas
    var getGridTarefas = function () {
        $.get(config.urls.getGridTarefas).done(function (data) {
            $('#div-grid-tarefas').html(data);
        }).fail(function () {
            // Adicione aqui um tratamento de erro, se necessário
        });
    };

    // Função para buscar a tela de edição de um usuário
    var buscarTelaAtualizarUsuario = function (id) {
        $.get(config.urls.buscarTelaAtualizarUsuario, { id: id }).done(function (data) {
            $('#div-editar-usuario').html(data);
            $('#div-grid-usuarios').hide();
            $('#div-add-usuario').hide();
        }).fail(function (xhr) {
            alert(xhr.responseText);
        });
    };

    // Função para buscar a tela de adição de uma tarefa
    var buscarTelaAddTarefa = function (id) {
        $.get(config.urls.buscarTelaAddTarefa, { id: id }).done(function (data) {
            $('#div-editar-tarefa').hide();
            $('#div-grid-tarefas').hide();
            $('#div-add-tarefa').html(data);
        }).fail(function (xhr) {
            alert(xhr.responseText);
        });
    };

    // Função para marcar uma tarefa como concluída
    var concluirTarefa = function (id) {
        $.get(config.urls.concluirTarefa, { id: id }).done(function (data) {
            $('#div-grid-tarefas').html(data);
        }).fail(function (xhr) {
            alert(xhr.responseText);
        });
    };

    // Função para excluir uma tarefa
    var excluirTarefa = function (id) {
        $.get(config.urls.excluirTarefa, { id: id }).done(function (data) {
            $('#div-grid-tarefas').html(data);
        }).fail(function (xhr) {
            alert(xhr.responseText);
        });
    };

    // Função para preencher um formulário com os dados de um contato
    function preecherFormDadosContato(idForm, model) {
        $('div#' + idForm).find('input[name], select[name]').each(function () {
            var campo = $(this);
            campo.val(model[campo.attr('name')]).trigger('change');
        });
    }

    // Função para buscar e exibir a tela de adição de um telefone de usuário
    var buscarTelaAdicionarTelefone = function (btn) {
        if (btn) {
            var tr = $(btn).closest('tr');
            var model = montaObjetoDadoContatoPorLinha($(tr));
            preecherFormDadosContato('formTelefone', model);
            tr.remove();
        }
        $('div#formTelefone').show();
    };

    // Função para buscar e exibir a tela de adição de um endereço de usuário
    var buscarTelaAdicionarEndereco = function (btn) {
        if (btn) {
            var tr = $(btn).closest('tr');
            var model = montaObjetoDadoContatoPorLinha($(tr));
            preecherFormDadosContato('formEndereco', model);
            tr.remove();
        }
        $('div#formEndereco').show();
    };

    // Função para montar um objeto de dados de contato a partir de um formulário
    function montaObjetoDadoContatoPorForm(form) {
        var model = {};
        form.find('input[name], select[name]').each(function () {
            var campo = $(this);
            model[campo.attr('name')] = campo.val();
        });
        return model;
    }

    // Função para inserir um telefone no grid de telefones
    var inserirTelefone = function () {
        var form = $('div#formTelefone'),
            model = montaObjetoDadoContatoPorForm(form);

        $.post(config.urls.linhaGridTelefone, { telefone: model }).done(function (tr) {
            $('div#div-grid-telefone tbody').append(tr);
            form.hide();
        }).fail(function () {
            console.log('Erro ao inserir telefone');
        });
    };

    // Função para inserir um endereço no grid de endereços
    var inserirEndereco = function () {
        var form = $('div#formEndereco'),
            model = montaObjetoDadoContatoPorForm(form);

        $.post(config.urls.linhaGridEndereco, { endereco: model }).done(function (tr) {
            $('div#div-grid-endereco tbody').append(tr);
            form.hide();
        }).fail(function () {
            console.log('Erro ao inserir endereço');
        });
    };

    // Função para excluir um usuário
    var deletarUsuario = function (id) {
        $.get(config.urls.deletarUsuario, { id: id }).done(function (data) {
            $('#div-grid-usuarios').html(data);
        }).fail(function (xhr) {
            alert(xhr.responseText);
        });
    };

    // Função para excluir um telefone
    var deletarTelefone = function (id) {
        $.get(config.urls.deletarTelefone, { id: id }).done(function (data) {
            $('tr[data-id="' + id + '"]').remove();
        }).fail(function (xhr) {
            alert(xhr.responseText);
        });
    };

    // Função para excluir um endereço
    var deletarEndereco = function (id) {
        $.get(config.urls.deletarEndereco, { id: id }).done(function (data) {
            $('tr[data-id="' + id + '"]').remove();
        }).fail(function (xhr) {
            alert(xhr.responseText);
        });
    };

    // Função inicial para carregar os dados necessários ao iniciar a página
    var init = function ($config) {
        config = $config;
        getGridUsuarios();
        getGridTarefas();
    };

    // Função para montar um objeto de dados de contato a partir de uma linha de tabela
    function montaObjetoDadoContatoPorLinha(linha) {
        var dadoContato = {};
        linha.find('td[data-name]').each(function () {
            var td = $(this);
            dadoContato[td.data('name')] = td.text();
        });
        return dadoContato;
    }

    // Função para salvar os dados do usuário
    var salvar = function () {
        var form = $('div#formUsuario'),
            model = {
                IdUsuario: $('input[name=IdUsuario]').val(),
                Telefones: [],
                Enderecos: []
            };

        form.find('input[name], select[name]').each(function () {
            var campo = $(this);
            model[campo.attr('name')] = campo.val();
        });

        $('div#div-grid-telefone table tbody tr').each(function () {
            var tr = $(this);
            model.Telefones.push(montaObjetoDadoContatoPorLinha(tr));
        });

        $('div#div-grid-endereco table tbody tr').each(function () {
            var tr = $(this);
            model.Enderecos.push(montaObjetoDadoContatoPorLinha(tr));
        });

        $.post(config.urls.post, model).done(function () {
            console.log('Dados salvos com sucesso');
            location.reload();
        }).fail(function () {
            console.log('Erro ao salvar dados');
        });
    };

    // Retorna um objeto com todas as funções que podem ser acessadas externamente
    return {
        init: init,
        getGridUsuarios: getGridUsuarios,
        buscarTelaAtualizarUsuario: buscarTelaAtualizarUsuario,
        deletarUsuario: deletarUsuario,
        deletarTelefone: deletarTelefone,
        deletarEndereco: deletarEndereco,
        salvar: salvar,
        inserirTelefone: inserirTelefone,
        inserirEndereco: inserirEndereco,
        buscarTelaAdicionarTelefone: buscarTelaAdicionarTelefone,
        buscarTelaAdicionarEndereco: buscarTelaAdicionarEndereco,
        getGridTarefas: getGridTarefas,
        buscarTelaAddTarefa: buscarTelaAddTarefa,
        concluirTarefa: concluirTarefa,
        excluirTarefa: excluirTarefa
    };
})();
