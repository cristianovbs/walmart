function postaAJAX(r, murl) {
    //exibe um spin (firula)
    $("#progresso").show();
    //obtemos o código do estado
    var codigo = $(r).attr("data-id");
    //fazemos o post
    $.ajax({
        url: murl,
        data: "{id:" + codigo + "}",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (retorno) {
            $('#row-' + codigo).fadeOut('slow');
        },
        error: function (erro) {
            alert('erro: ' + erro.statusText);
        },
        complete: function () {
            $("#progresso").hide();
        }
    });
}