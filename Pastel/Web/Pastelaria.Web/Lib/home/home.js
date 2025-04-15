var home = (function () {
    var config = {
        urls: {
            getGrid: '',
            post: ''
        }
    };

    var getGrid = function () {
        $.get(config.urls.getGrid).done(function (data) {
            $('#div-grid-testes').html(data);
        }).fail();

    }


    var post = function () {
        debugger;
        var model = {
            id: $("#id").val(),
            descricao: $("#descricao").val()
        };

        $.post(config.urls.post,
           model
        ).done(function () {
        }).fail();
    }


    var init = function ($config) {
        config = $config;
    };

    return {
        init: init,
        getGrid: getGrid,
        post: post
    }
})();