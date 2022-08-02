

$(document).ready(function() {

    var fecha;

        $(document).on("change", "#datapicker", function() {

            fecha = $("#datapicker").val();


            var response = $.ajax
                ({
                    method: "POST",
                    url: '/Compras/CompraPorFecha',
                    data: { fechaRequest: fecha }
                }).done(function (data) {
                    if (data.length != 0) {
                         renderCompras(data);
                    } else {

                        $("#bodyTable").html("<p>No hay compras en esta fecha</p>");
                    }


                });
        });


   


});

function renderCompras(data) {

    var datos;
   
    

    for (let i = 0; i < data.length;i++) {

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
                        <a href="Compras/Edit/${data[i].id}">Edit</a> 
                        <a href="Compras/Details/${data[i].id}">Details</a>
                        <a href="Compras/Delete/${data[i].id}">Delete</a>
                        </td>
                    </tr>`;
    }

    $("#bodyTable").html(datos);
}


        
   
