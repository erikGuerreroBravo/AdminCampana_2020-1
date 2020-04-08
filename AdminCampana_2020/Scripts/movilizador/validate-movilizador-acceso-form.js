$(document).ready(function () {

    //$('#Nombres').prop('disabled', true);
    $('#Apellidos').prop('disabled', true);
    $('#Email').prop('disabled', true);
    $('#Clave').prop('disabled', true);
    $('#IdRol').prop('disabled', true);
    $('#btnEnviar').prop('disabled', true);
   
    $('#Nombres').keyup(function () {
        let data = $(this).val();

        if (data == "") {
            $('#Apellidos').prop('disabled', true);
            $('#Email').prop('disabled', true);
            $('#Clave').prop('disabled', true);
            $('#IdRol').prop('disabled', true);
            $('#btnEnviar').prop('disabled', true);           

        } else {
            $('#Apellidos').prop('disabled', false);
        }
    });

    $('#Apellidos').keyup(function () {
        let data = $(this).val();

        if (data == "") {
            
            $('#Email').prop('disabled', true);
            $('#Clave').prop('disabled', true);
            $('#IdRol').prop('disabled', true);
            $('#btnEnviar').prop('disabled', true);

        } else {
            $('#Email').prop('disabled', false);
        }
    });

    $('#Email').keyup(function () {
        let data = $(this).val();

        if (data == "") {
            $('#Clave').prop('disabled', true);
            $('#IdRol').prop('disabled', true);
            $('#btnEnviar').prop('disabled', true);

        } else {
            $('#Clave').prop('disabled', false);
        }
    });


    $('#Clave').keyup(function () {
        let data = $(this).val();

        if (data == "") {
            $('#IdRol').prop('disabled', true);
            $('#btnEnviar').prop('disabled', true);

        } else {
            //$('#IdRol').prop('disabled', false);
            $('#btnEnviar').prop('disabled', false);
        }
    });

    //$('#IdRol').change(function () {
    //    let data = $(this).val();

    //    if (data == "") {
    //        $('#btnEnviar').prop('disabled', true);
            
    //    } else {
    //        toastr.info("Área Seleccionada", "Campaña dice", { timeOut: 1000, closeButton: true });
    //        $('#btnEnviar').prop('disabled', false);
    //    }
    //});


})