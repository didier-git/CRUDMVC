

$(document).ready(function () {

    var fecha;
    var lugar;

    $(document).on("change", "#datapicker", function () {

        lugar = $("#select").val(); // Sin lugar
        fecha = $(this).val();//'2022/07'

            var response = $.ajax
                ({
                    method: "POST",
                    url: '/Compras/ComprasPorFiltro',
                    data: {
                        fecha: fecha,
                        lugar: lugar
                    }
                }).done(function (data) {
                    if (data.length != 0) {
                        renderCompras(data);
                    } else {

                        $("#bodyTable").html("<p>No hay compras en los filtros seleccionados</p>");
                    }


                });


       


       
    });

    $(document).on("change", "#select", function () {

        var lugar = $(this).val();
        var fecha = $("#datapicker").val();
        if (lugar != "Todos") {
            
                $.ajax({
                    method: "POST",
                    url: "/Compras/ComprasPorFiltro",
                    data: { fecha : fecha,lugar: lugar }
                }).done(function (data) {
                    if (data.length != 0) {
                        renderCompras(data);
                    } else {
                        $("#bodyTable").html("<p>No hay compras segun los filtros seleccionados</p>");
                    }
                    
                });

        } else {
            $.ajax({
                method: "GET",
                url: "/Compras/IndexJ"
            }).done(function (data) {
                renderCompras(data);
            });
        }





    });





});

function renderCompras(data) {

    var datos;



    for (let i = 0; i < data.length; i++) {

        let date = new Date(data[i].fechaDeCompra);

        var fechaEstandar = date.toISOString().substring(0, 10);

        var fechaFinal = fechaEstandar.replace(/-/g, '/');

        var monto = parseFloat(data[i].monto);


        datos += `<tr>
                        <td>${data[i].descripcion}</td>
                        <td>${fechaFinal}</td>
                        <td>${monto}</td>
                        <td>${data[i].lugarDeCompra}</td>
                        <td>
                        <a href="Compras/Edit/${data[i].id}">Edit</a> |
                        <a href="Compras/Details/${data[i].id}">Details</a> |
                        <a href="Compras/Delete/${data[i].id}">Delete</a>
                        </td>
                    </tr>`;
    }

    $("#bodyTable").html(datos);
}




