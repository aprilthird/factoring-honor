var newFeeReceipt = function () {

    var bankCostDatatable = null;
    var frm = null;
    var saveBtn = null;
    var calculateBtn = null;
    var calculated = false;
    var isRucBtn = null;
    var dbRucBtn = null;

    var options = {
        dom: 'rt<"bottom row" <"col-md-6" l><"col-md-6" p>>',
        responsive: true,
        ajax: {
            url: _app.parseUrl("/costos/listar"),
            dataSrc: "",
            data: function (d) {
                d.bankId = $("#BankId").val();
                return d;
            }
        },
        columns: [
            {
                title: "Tipo",
                data: "isFinal",
                render: function (data) {
                    return data ? "Final" : "Inicial";
                }
            },
            { title: "Nombre", data: "name" },
            {
                title: "Valor",
                data: "convertedValue",
                render: function (data) {
                    return data.toMoney();
                }
            }
        ]
    };

    var datatable = {
        init: function () {
            if (bankCostDatatable) {
                this.reload();
                return;
            }
            bankCostDatatable = $("#bank_cost_datatable").DataTable(options);
        },
        reload: function () {
            bankCostDatatable.ajax.reload();
        }
    };

    var form = {
        submit: {
            add: function (formElement) {
                let data = $(formElement).serialize();
                saveBtn.start();
                $("#btn_save").prop("disabled", true);
                $(formElement).find("input, select").prop("disabled", true);
                $.ajax({
                    url: _app.parseUrl("/recibos/crear"),
                    method: "post",
                    data: data
                })
                    .always(function () {
                        saveBtn.stop();
                        $("#btn_save").prop("disabled", false);
                        $(formElement).find("input, select").prop("disabled", false);
                    })
                    .done(function (result) {
                        _app.show.notification.add.success();
                    })
                    .fail(function (error) {
                        if (error.responseText) {
                            $("#form_alert_text").html(error.responseText);
                            $("#form_alert").removeClass("d-none").addClass("show");
                        }
                        _app.show.notification.add.error();
                    });
            },
            calculate: function (formElement) {
                let data = $(formElement).serialize();
                calculateBtn.start();
                $("#btn_calculate").prop("disabled", true);
                $(formElement).find("input, select").prop("disabled", true);
                $.ajax({
                    url: _app.parseUrl("/recibos/calcular"),
                    method: "post",
                    data: data
                })
                    .always(function () {
                        calculateBtn.stop();
                        $(formElement).find("input, select").prop("disabled", false);
                    })
                    .done(function (result) {
                        calculated = true;
                        $("#btn_calculate").prop("disabled", true);
                        $("#calculate_container").show();
                        $("#btn_save").show();

                        $("#calc_days").text(result.daysDiff);
                        $("#calc_rate").text(result.termEffectiveRate.toPercent());
                        $("#calc_discount_rate").text(result.termDiscountRate.toPercent());
                        $("#calc_discount").text(result.discount.toMoney());
                        $("#calc_initial_cost").text(result.totalInitialCost.toMoney());
                        $("#calc_final_cost").text(result.totalFinalCost.toMoney());
                        $("#calc_received_ammount").text(result.receivedAmmount.toMoney());
                        $("#calc_delivered_ammount").text(result.deliveredAmmount.toMoney());
                        $("#calc_total_cost_rate").text(result.totalAnualEffectiveCost.toPercent());
                        _app.show.notification.success("Cálculo realizado satisfactoriamente.");
                    })
                    .fail(function (error) {
                        $("#btn_calculate").prop("disabled", false);
                        if (error.responseText) {
                            $("#form_alert_text").html(error.responseText);
                            $("#form_alert").removeClass("d-none").addClass("show");
                        }
                        _app.show.notification.add.error("No se pudo realizar el cálculo.");
                    });
            }
        },
        reset: {
            add: function () {
                frm.resetForm();
                $("#form").trigger("reset");
                $("#form_alert").removeClass("show").addClass("d-none");
            }
        }
    };

    var select2 = {
        init: function () {
            this.bank.init();
        },
        bank: {
            init: function () {
                $.ajax({
                    url: _app.parseUrl("/bancos/listar")
                }).done(function (result) {
                    let data = $.map(result,
                        function (obj) {
                            obj.id = obj.id || obj.id;
                            obj.text = obj.text || obj.name;
                            obj.rate = obj.convertedValue || obj.rateValue;
                            return obj;
                        });
                    $(".select2-banks")
                        .on("change", function () {
                            datatable.init();
                            $("#bank_rate").val(data[$(this).get(0).selectedIndex].rate.toPercent(2));
                        })
                        .select2({
                            data: data,
                            placeholder: "Banco"
                        });
                    $(".select2-banks").trigger("change");
                });
            }
        }
    };

    var validate = {
        init: function () {
            addForm = $("#form").validate({
                submitHandler: function (formElement, e) {
                    e.preventDefault();
                    if (calculated) {
                        form.submit.add(formElement);
                    }
                    else {
                        form.submit.calculate(formElement);
                    }
                }
            });
        }
    };

    var datepicker = {
        init: function () {
            $(".datepicker").datepicker();
        }
    };

    var ladda = {
        init: function () {
            saveBtn = Ladda.create($("#btn_save")[0]);
            calculateBtn = Ladda.create($("#btn_calculate")[0]);
            isRucBtn = Ladda.create($("#btn_issuing_ruc")[0]);
            dbRucBtn = Ladda.create($("#btn_debtor_ruc")[0]);
        }
    };

    var events = {
        init: function () {
            $("#calculate_container").hide();
            $("#btn_save").hide();
            $("#btn_issuing_detail").prop("disabled", true);
            $("#btn_debtor_detail").prop("disabled", true);
            $("#btn_issuing_ruc").on("click", function () {
                isRucBtn.start();
                $.ajax({
                    url: _app.parseUrl(`/ruc/${$("#IssuingRuc").val()}`),
                    method: "GET"
                })
                    .always(function () {
                        isRucBtn.stop();
                        $("#issuing_name").val("");
                    })
                    .done(function (result) {
                        _app.show.notification.success("RUC Validado");
                        $("#issuing_name").val(result.name);
                        $("#issuing_modal_ruc").text($("#IssuingRuc").val());
                        $("#issuing_modal_name").text(result.name);
                        $("#issuing_modal_status").text(result.status);
                        $("#issuing_modal_condition").text(result.condition);
                        $("#issuing_modal_ubigeo").text(result.ubigeo);
                        $("#issuing_modal_department").text(result.department);
                        $("#issuing_modal_province").text(result.province);
                        $("#issuing_modal_district").text(result.district);
                        $("#issuing_modal_address").text(result.address);
                        $("#btn_issuing_detail").prop("disabled", false);
                        $("#issuing_name").val(result.name);

                    }).fail(function (error) {
                        if (error.responseText) {
                            _app.show.notification.error(error.responseText);
                        }
                        else {
                            _app.show.notification.error();
                        }
                    });
            });
            $("#btn_debtor_ruc").on("click", function () {
                dbRucBtn.start();
                $.ajax({
                    url: _app.parseUrl(`/ruc/${$("#DebtorRuc").val()}`),
                    method: "GET"
                })
                    .always(function () {
                        dbRucBtn.stop();
                        $("#debtor_name").val("");
                    })
                    .done(function (result) {
                        _app.show.notification.success("RUC Validado");
                        $("#debtor_name").val(result.name);
                        $("#debtor_modal_ruc").text($("#DebtorRuc").val());
                        $("#debtor_modal_name").text(result.name);
                        $("#debtor_modal_status").text(result.status);
                        $("#debtor_modal_condition").text(result.condition);
                        $("#debtor_modal_ubigeo").text(result.ubigeo);
                        $("#debtor_modal_department").text(result.department);
                        $("#debtor_modal_province").text(result.province);
                        $("#debtor_modal_district").text(result.district);
                        $("#debtor_modal_address").text(result.address);
                        $("#btn_debtor_detail").prop("disabled", false);
                        $("#debtor_name").val(result.name);
                    }).fail(function (error) {
                        if (error.responseText) {
                            _app.show.notification.error(error.responseText);
                        }
                        else {
                            _app.show.notification.error();
                        }
                    });
            });
            $("#IssuingRuc").on("input", function () {
                $("#issuing_name").val("");
                $("#btn_issuing_detail").prop("disabled", true);
            });
            $("#DebtorRuc").on("input", function () {
                $("#debtor_name").val("");
                $("#btn_debtor_detail").prop("disabled", true);
            });
            $("#form").find("input, select").on("change input", function () {
                calculated = false;
                $("#btn_calculate").prop("disabled", false);
                $("#btn_save").hide();
                $("#calculate_container").hide();
            });
        }
    };

    return {
        init: function () {
            events.init();
            validate.init();
            select2.init();
            datepicker.init();
            ladda.init();
        }
    };
}();

$(function () {
    newFeeReceipt.init();
});