var feeReceipt = function () {

    var feeReceiptDatatable = null;
    
    var options = {
        responsive: true,
        ajax: {
            url: _app.parseUrl("/recibos/listar"),
            dataSrc: ""
        },
        columns: [
            {
                title: "N°",
                data: "number"
            },
            {
                title: "RUC Emisor",
                data: "issuingRuc"
            },
            {
                title: "Fecha de Descuento",
                data: "discountDate"
            },
            {
                title: "Fecha de Pago",
                data: "paymentDate"
            },
            {
                title: "Monto del Recibo",  
                data: "totalAmmount",
                render: function (data) {
                    return data.toMoney();
                }
            },
            {
                title: "Descuento",
                data: "discount",
                render: function (data) {
                    return data.toMoney();
                }
            },
            {
                title: "Valor Recibido",
                data: "receivedAmmount",
                render: function (data) {
                    return data.toMoney();
                }
            },
            {
                title: "Valor Entregado",
                data: "deliveredAmmount",
                render: function (data) {
                    return data.toMoney();
                }
            },
            {
                title: "TCEA %",
                data: "totalAnualEffectiveCost",
                render: function (data) {
                    return data.toPercent();
                }
            },
            {
                title: "Opciones",
                data: null,
                orderable: false,
                render: function (data, type, row) {
                    var tmp = "";
                    tmp += `<button data-id="${row.id}" class="btn btn-info btn-sm btn-detail">`;
                    tmp += `<i class="fa fa-eye"></i></button> `;
                    tmp += `<button data-id="${row.id}" class="btn btn-danger btn-sm btn-delete">`;
                    tmp += `<i class="fa fa-trash"></i></button>`;
                    return tmp;
                }
            }
        ]
    };

    var datatable = {
        init: function () {
            feeReceiptDatatable = $("#fee_receipt_datatable").DataTable(options);
            this.initEvents();
        },
        reload: function () {
            feeReceiptDatatable.ajax.reload();
        },
        rateDetail: {
            init: function () {
                $("#rate_table").DataTable({
                    dom: 'frt<"bottom row" <"col-md-6" l><"col-md-6" p>>',
                    responsive: true,
                });
            }
        },
        initEvents: function () {
            feeReceiptDatatable.on("click",
                ".btn-edit",
                function () {
                    let $btn = $(this);
                    let id = $btn.data("id");
                    form.load.edit(id);
                });

            feeReceiptDatatable.on("click",
                ".btn-delete",
                function () {
                    let $btn = $(this);
                    let id = $btn.data("id");
                    Swal.fire({
                        title: "¿Está seguro?",
                        text: "El recibo será eliminado permanentemente",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonText: "Sí, eliminarlo",
                        confirmButtonClass: "btn btn-danger m-btn m-btn--custom",
                        cancelButtonText: "Cancelar",
                        showLoaderOnConfirm: true,
                        allowOutsideClick: () => !swal.isLoading(),
                        preConfirm: () => {
                            return new Promise((resolve) => {
                                $.ajax({
                                    url: _app.parseUrl(`/recibos/eliminar/${id}`),
                                    type: "delete",
                                    success: function (result) {
                                        datatable.reload();
                                        swal({
                                            type: "success",
                                            title: "Completado",
                                            text: "El recibo ha sido eliminado con éxito",
                                            confirmButtonText: "Excelente"
                                        });
                                        setTimeout(function () {
                                            location.reload();
                                        }, 3000);
                                    },
                                    error: function (errormessage) {
                                        swal({
                                            type: "error",
                                            title: "Error",
                                            confirmButtonClass: "btn btn-danger m-btn m-btn--custom",
                                            confirmButtonText: "Entendido",
                                            text: "Ocurrió un error al intentar eliminar el recibo"
                                        });
                                    }
                                });
                            });
                        }
                    });
                });
        }
    };

    return {
        init: function () {
            datatable.init();
            datatable.rateDetail.init();
        }
    };
}();

$(function () {
    feeReceipt.init();
});