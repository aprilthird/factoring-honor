var configuration = function () {

    var frm = null;
    var saveBtn = null;
    
    var form = {
        submit: {
            edit: function (formElement) {
                let data = $(formElement).serialize();
                saveBtn.start();
                $(formElement).find("input, select").prop("disabled", true);
                $.ajax({
                    url: _app.parseUrl("/configuracion/actualizar"),
                    method: "post",
                    data: data
                })
                    .always(function () {
                        saveBtn.stop();
                        $(formElement).find("input, select").prop("disabled", false);
                    })
                    .done(function (result) {
                        _app.show.notification.edit.success();
                    })
                    .fail(function (error) {
                        if (error.responseText) {
                            $("#form_alert_text").html(error.responseText);
                            $("#form_alert").removeClass("d-none").addClass("show");
                        }
                        _app.show.notification.edit.error();
                    });
            }
        },
        reset: {
            edit: function () {
                frm.resetForm();
                $("#form").trigger("reset");
                $("#form_alert").removeClass("show").addClass("d-none");
            }
        }
    };

    var select2 = {
        init: function () {
            this.currencyType.init();
            this.rateType.init();
        },
        currencyType: {
            init: function () {
                $.ajax({
                    url: _app.parseUrl("/tipos-de-cambio/listar")
                }).done(function (result) {
                    let data = $.map(result,
                        function (obj) {
                            obj.id = obj.id || obj.id;
                            obj.text = `${obj.name} (${obj.shortName})`;
                            return obj;
                        });
                    $(".select2-currency-types")
                        .select2({
                            data: data,
                            placeholder: "Tipo de Cambio",
                            minimumResultsForSearch: -1
                        });
                    $(".select2-currency-types").val(currencyTypeId).trigger("change");
                });
            }
        },
        rateType: {
            init: function () {
                $(".select2-rate-types")
                    .select2({
                        placeholder: "Tipo de Tasa",
                        minimumResultsForSearch: -1
                    });
                $(".select2-rate-types").val(rateType).trigger("change");
            }
        }
    };

    var validate = {
        init: function () {
            addForm = $("#form").validate({
                submitHandler: function (formElement, e) {
                    e.preventDefault();
                    form.submit.edit(formElement);
                }
            });
        }
    };

    var ladda = {
        init: function () {
            saveBtn = Ladda.create($("#btn_save")[0]);
        }
    };

    return {
        init: function () {
            validate.init();
            select2.init();
            ladda.init();
        }
    };
}();

$(function () {
    configuration.init();
});