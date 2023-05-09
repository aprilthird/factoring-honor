var bank = function () {

    var bankDatatable = null;
    var addForm = null;
    var addBtn = null;
    var editForm = null;
    var editBtn = null;

    var options = {
        responsive: true,
        ajax: {
            url: _app.parseUrl("/bancos/listar"),
            dataSrc: ""
        },
        columns: [
            { title: "Nombre", data: "name" },
            { title: "Nombre Corto", data: "shortName" },
            { title: "Tipo de Tasa", data: "rateTypeText" },
            { title: "Período de Tasa", data: "rateTermText" },
            {
                title: "Valor de Tasa",
                data: "rateValue",
                render: function (data) {
                    return data.toPercent(2);
                }
            },
            {
                title: "Opciones",
                data: null,
                orderable: false,
                render: function (data, type, row) {
                    var tmp = "";
                    tmp += `<button data-id="${row.id}" class="btn btn-info btn-sm btn-edit">`;
                    tmp += `<i class="fa fa-edit"></i></button> `;
                    tmp += `<button data-id="${row.id}" class="btn btn-danger btn-sm btn-delete">`;
                    tmp += `<i class="fa fa-trash"></i></button>`;
                    return tmp;
                }
            }
        ]
    };

    var datatable = {
        init: function () {
            bankDatatable = $("#bank_datatable").DataTable(options);
            this.initEvents();
        },
        reload: function () {
            bankDatatable.ajax.reload();
        },
        initEvents: function () {
            bankDatatable.on("click",
                ".btn-edit",
                function () {
                    let $btn = $(this);
                    let id = $btn.data("id");
                    form.load.edit(id);
                });

            bankDatatable.on("click",
                ".btn-delete",
                function () {
                    let $btn = $(this);
                    let id = $btn.data("id");
                    Swal.fire({
                        title: "¿Está seguro?",
                        text: "El banco será eliminado permanentemente",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonText: "Sí, eliminarla",
                        confirmButtonClass: "btn btn-danger m-btn m-btn--custom",
                        cancelButtonText: "Cancelar",
                        showLoaderOnConfirm: true,
                        allowOutsideClick: () => !swal.isLoading(),
                        preConfirm: () => {
                            return new Promise((resolve) => {
                                $.ajax({
                                    url: _app.parseUrl(`/bancos/${id}`),
                                    type: "delete",
                                    success: function (result) {
                                        datatable.reload();
                                        swal({
                                            type: "success",
                                            title: "Completado",
                                            text: "El banco ha sido eliminado con éxito",
                                            confirmButtonText: "Excelente"
                                        });
                                    },
                                    error: function (errormessage) {
                                        swal({
                                            type: "error",
                                            title: "Error",
                                            confirmButtonClass: "btn btn-danger m-btn m-btn--custom",
                                            confirmButtonText: "Entendido",
                                            text: "Ocurrió un error al intentar eliminar el banco"
                                        });
                                    }
                                });
                            });
                        }
                    });
                });

        }
    };

    var form = {
        load: {
            edit: function (id) {
                _app.loader.show();
                $.ajax({
                    url: _app.parseUrl(`/bancos/${id}`)
                })
                    .done(function (result) {
                        let formElements = $("#edit_form");
                        formElements.find("[name='Id']").val(result.id);
                        formElements.find("[name='Name']").val(result.name);
                        formElements.find("[name='ShortName']").val(result.shortName);
                        formElements.find("[name='RateValue']").val(result.rateValue.toPercent(2));
                        formElements.find("[name='RateType']").val(result.rateType).trigger("change");
                        formElements.find("[name='RateTerm']").val(result.rateTerm).trigger("change");
                        $("#edit_modal").modal("show");
                    })
                    .always(function () {
                        _app.loader.hide();
                    });
            }
        },
        submit: {
            add: function (formElement) {
                let data = $(formElement).serialize();
                addBtn.start();
                $(formElement).find("input, select").prop("disabled", true);
                $.ajax({
                    url: _app.parseUrl("/bancos/crear"),
                    method: "post",
                    data: data
                })
                    .always(function () {
                        addBtn.stop();
                        $(formElement).find("input, select").prop("disabled", false);
                    })
                    .done(function (result) {
                        datatable.reload();
                        $("#add_modal").modal("hide");
                        _app.show.notification.add.success();
                    })
                    .fail(function (error) {
                        if (error.responseText) {
                            $("#add_alert_text").html(error.responseText);
                            $("#add_alert").removeClass("d-none").addClass("show");
                        }
                        _app.show.notification.add.error();
                    });
            },
            edit: function (formElement) {
                let data = $(formElement).serialize();
                editBtn.start();
                $(formElement).find("input, select").prop("disabled", true);
                let id = $(formElement).find("input[name='Id']").val();
                $.ajax({
                    url: _app.parseUrl(`/bancos/editar/${id}`),
                    method: "put",
                    data: data
                })
                    .always(function () {
                        editBtn.stop();
                        $(formElement).find("input, select").prop("disabled", false);
                    })
                    .done(function (result) {
                        datatable.reload();
                        $("#edit_modal").modal("hide");
                        _app.show.notification.edit.success();
                    })
                    .fail(function (error) {
                        if (error.responseText) {
                            $("#edit_alert_text").html(error.responseText);
                            $("#edit_alert").removeClass("d-none").addClass("show");
                        }
                        _app.show.notification.edit.error();
                    });
            }
        },
        reset: {
            add: function () {
                addForm.resetForm();
                $("#add_form").trigger("reset");
                $("#add_alert").removeClass("show").addClass("d-none");
            },
            edit: function () {
                editForm.resetForm();
                $("#edit_form").trigger("reset");
                $("#edit_alert").removeClass("show").addClass("d-none");
            }
        }
    };

    var select2 = {
        init: function () {
            this.rateTypes.init();
            this.rateTerms.init();
        },
        rateTypes: {
            init: function () {
                $(".select2-rate-types").select2({
                    placeholder: "Tipo de Tasa",
                    minimumResultsForSearch: -1
                });
            }
        },
        rateTerms: {
            init: function () {
                $(".select2-rate-terms").select2({
                    placeholder: "Período de Tasa",
                    minimumResultsForSearch: -1
                });
            }
        }
    };

    var validate = {
        init: function () {
            addForm = $("#add_form").validate({
                submitHandler: function (formElement, e) {
                    e.preventDefault();
                    form.submit.add(formElement);
                }
            });

            editForm = $("#edit_form").validate({
                submitHandler: function (formElement, e) {
                    e.preventDefault();
                    form.submit.edit(formElement);
                }
            });
        }
    };

    var modals = {
        init: function () {
            $("#add_modal").on("hidden.bs.modal",
                function () {
                    form.reset.add();
                });

            $("#edit_modal").on("hidden.bs.modal",
                function () {
                    form.reset.edit();
                });
        }
    };

    var ladda = {
        init: function () {
            addBtn = Ladda.create($("#add_form button[type='submit']")[0]);
            editBtn = Ladda.create($("#edit_form button[type='submit']")[0]);
        }
    };

    var events = {
        init: function () {
            $("#add_form [name='RateType']").attr("id", "add_rate_type");
            $("#edit_form [name='RateType']").attr("id", "edit_rate_type");
            $("#add_form [name='RateTerm']").attr("id", "add_rate_term");
            $("#edit_form [name='RateTerm']").attr("id", "edit_rate_term");
        }
    };

    return {
        init: function () {
            events.init();
            datatable.init();
            select2.init();
            validate.init();
            modals.init();
            ladda.init();
        }
    };
}();

$(function () {
    bank.init();
});