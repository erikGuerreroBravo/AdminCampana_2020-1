/// <reference path="../datatablejs/jquery.js" />
/// <reference path="../toastr.min.js" />



$(document).ready(function () {

    //$('#MyDataTable').DataTable();

    $('#idNombres').attr('disabled', true);
    $('#btnConsultar').attr('disabled', true);
    $('#idRol').change(function () {
        var idArea = $('#idRol').val();

        $('#btnConsultar').attr('disabled', true);

        $.ajax({
            type: "Get",
            url: "/Manager/GetDatosUsuarioCoordinador?idArea=" + idArea,
            dataType: "Json",
            success: function (data) {

                sessionStorage.clear();//limpiamos el sessionstorage
                toastr.success("Área Seleccionada", "Campaña dice", { timeOut: 1000, closeButton: true });
                $("#idNombres").find('option').remove();///removemos loq ue hay en el selct
                let opcion = '<option value="' + 0 + '">' + "--Seleccionar--" + '</option>';
                $("#idNombres").attr('disabled', false);
                $("#idNombres").append(opcion);
                $.each(data, function (i) {
                    var opcionhtml = '<option value="' +
                        data[i].Id + '">' + data[i].Nombres + " " + data[i].Apellidos + '</option>';
                    $("#idNombres").append(opcionhtml);

                });
                sessionStorage.consulta = JSON.stringify(data);

            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("No se pudo procesar la información de forma correcta, intenta de nuevo por favor", "Campaña dice", { timeOut: 1000, closeButton: true });
                console.log(textStatus);
            }
        })
    });

    $('#idNombres').change(function () {

        let nombres = $('#idNombres').val();
        if (nombres == '')
        {
            $('#btnConsultar').attr('disabled', true);
        }

        let consultaJson = $.parseJSON(sessionStorage.consulta);
        $('#btnConsultar').attr('disabled', false);
        $.each(consultaJson, function (i) {
            $('#Nombres').val(consultaJson[i].Nombres);
            $('#Apellidos').val(consultaJson[i].Apellidos);
            $('#email').val(consultaJson[i].Email);
            $('#area').val(consultaJson[i].UsuarioRoles[i].Rol.Nombre);
        });

    });

    $('#btnConsultar').click(function () {
        var id = $('#idNombres').val();

        $.ajax({
            type: "Get",
            url: "/Manager/GetAllDataByCoordinador?idCoordinador=" + id,
            dataType: "Json",
            success: function (response) {

                sessionStorage.clear();//limpiamos el sessionstorage
                toastr.success("Coordinador Seleccionado", "Campaña dice", { timeOut: 1000, closeButton: true });

                BindTable(response);

            },
            error: function (xhr, textStatus, errorThrown) {
                toastr.error("No se pudo procesar la información de forma correcta, intenta de nuevo por favor", "Campaña dice", { timeOut: 1000, closeButton: true });
                console.log(textStatus);
            }
        })
    });

    var BindTable = function (response)
    {
        $('#MyDataTable').DataTable({
            "bFilter": false,
            "bLengthChange": false,
            "bSearchable": false,
            "bSortable": false,
            "aaData": response,
            "aoColumns": [
                { "mData":"Nombres"},
                { "mData": "Apellidos"},
                { "mData": "NombreMovilizado" },
                { "mData": "ApellidoPaternoMovilizado"},
                { "mData": "ApellidoMaternoMovilizado"},
                                
            ]
        });
    };

   
});