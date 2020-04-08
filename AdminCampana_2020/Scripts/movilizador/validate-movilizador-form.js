
$(document).ready(function () {

    $('#StrApellidoPaterno').prop('disabled', true);
    $('#StrApellidoMaterno').prop('disabled', true);
    $('#StrNumeroCelular').prop('disabled', true);
    $('#IdOcupaciones').prop('disabled', true);
    $('#StrCalle').prop('disabled', true);
    $('#IdColonia').prop('disabled', true);
    $('#StrNumeroInterior').prop('disabled', true);
    $('#StrNumeroExterior').prop("disabled", true);

    $('#btnEnviar').prop('disabled', true);
    $('#StrNombre').keyup(function () {
        let data = $(this).val();

        if (data == "") {
            $('#StrApellidoPaterno').prop('disabled', true);
            $('#StrApellidoMaterno').prop('disabled', true);
            $('#StrNumeroCelular').prop('disabled', true);
            $('#IdOcupaciones').prop('disabled', true);
            $('#StrCalle').prop('disabled', true);
            $('#IdColonia').prop('disabled', true);
            $('#StrNumeroInterior').prop('disabled', true);
            $('#StrNumeroExterior').prop("disabled", true);
            //$('#StrApellidoPaterno').val("");
            //$('#StrApellidoMaterno').val("");
            //$('#StrNumeroCelular').val("");
            //$('#IdOcupaciones').val(0);
            //$('#StrCalle').val("");
            //$('#IdColonia').val(0);
            //$('#StrNumeroInterior').val("");
            //$('#StrNumeroExterior').val("");

        } else {
            $('#StrApellidoPaterno').prop('disabled', false);
        }
    });

    $('#StrApellidoPaterno').keyup(function () {

        let data = $(this).val();

        if (data == "") {
            
            $('#StrApellidoMaterno').prop('disabled', true);
            $('#StrNumeroCelular').prop('disabled', true);
            $('#IdOcupaciones').prop('disabled', true);
            $('#StrCalle').prop('disabled', true);
            $('#IdColonia').prop('disabled', true);
            $('#StrNumeroInterior').prop('disabled', true);
            $('#StrNumeroExterior').prop("disabled", true);

            //$('#StrApellidoPaterno').val("");
            //$('#StrApellidoMaterno').val("");
            //$('#StrNumeroCelular').val("");
            //$('#IdOcupaciones').val(0);
            //$('#StrCalle').val("");
            //$('#IdColonia').val(0);
            //$('#StrNumeroInterior').val("");
            //$('#StrNumeroExterior').val("");

        } else {
            $('#StrApellidoMaterno').prop('disabled', false);
        }
    });

    $('#StrApellidoMaterno').keyup(function () {
        let data = $(this).val();

        if (data == "") {
            
            $('#StrNumeroCelular').prop('disabled', true);
            $('#IdOcupaciones').prop('disabled', true);
            $('#StrCalle').prop('disabled', true);
            $('#IdColonia').prop('disabled', true);
            $('#StrNumeroInterior').prop('disabled', true);
            $('#StrNumeroExterior').prop("disabled", true);
            
            //$('#StrNumeroCelular').val("");
            //$('#IdOcupaciones').val(0);
            //$('#StrCalle').val("");
            //$('#IdColonia').val(0);
            //$('#StrNumeroInterior').val("");
            //$('#StrNumeroExterior').val("");

        } else {
            $('#StrNumeroCelular').prop('disabled', false);
        }
    });

    $('#StrNumeroCelular').keyup(function () {
        let data = $(this).val();

        if (data == "") {

            //$('#StrNumeroCelular').prop('disabled', true);
            $('#IdOcupaciones').prop('disabled', true);
            $('#StrCalle').prop('disabled', true);
            $('#IdColonia').prop('disabled', true);
            $('#StrNumeroInterior').prop('disabled', true);
            $('#StrNumeroExterior').prop("disabled", true);

            //$('#StrNumeroCelular').val("");
            //$('#IdOcupaciones').val(0);
            //$('#StrCalle').val("");
            //$('#IdColonia').val(0);
            //$('#StrNumeroInterior').val("");
            //$('#StrNumeroExterior').val("");

        } else {
            $('#IdOcupaciones').prop('disabled', false);
        }
    });


    $('#IdOcupaciones').change(function () {
        let data = $(this).val();

        if (data == "") {
            $('#StrApellidoPaterno').prop('disabled', true);
            $('#StrApellidoMaterno').prop('disabled', true);
            $('#StrNumeroCelular').prop('disabled', true);
            $('#IdOcupaciones').prop('disabled', true);
            $('#StrCalle').prop('disabled', true);
            $('#IdColonia').prop('disabled', true);
            $('#StrNumeroInterior').prop('disabled', true);
            $('#StrNumeroExterior').prop("disabled", true);
            //$('#StrApellidoPaterno').val("");
            //$('#StrApellidoMaterno').val("");
            //$('#StrNumeroCelular').val("");
            //$('#StrCalle').val("");
            //$('#IdColonia').val(0);
            //$('#StrNumeroInterior').val("");
            //$('#StrNumeroExterior').val("");

        } else {
            toastr.success("Ocupación Seleccionada", "Campaña dice", { timeOut: 1000, closeButton: true });
            $('#StrCalle').prop('disabled', false);
        }
    });


    $('#StrCalle').keyup(function () {
        let data = $(this).val();

        if (data == "") {
            $('#IdColonia').prop('disabled', true);
            $('#StrNumeroInterior').prop('disabled', true);
            $('#StrNumeroExterior').prop("disabled", true);
            $('#StrCalle').val("");
            $('#IdColonia').val(0);
            //$('#StrNumeroInterior').val("");
            //$('#StrNumeroExterior').val("");

        } else {
            
            $('#IdColonia').prop('disabled', false);
        }
    });


    $('#IdColonia').change(function () {
        let data = $(this).val();

        if (data == "") {
            $('#StrCalle').prop('disabled', true);
            $('#IdColonia').prop('disabled', true);
            $('#StrNumeroInterior').prop('disabled', true);
            $('#StrNumeroExterior').prop("disabled", true);
            //$('#StrNumeroInterior').val("");
            //$('#StrNumeroExterior').val("");

        } else {
            toastr.success("Ocupación Seleccionada", "Campaña dice", { timeOut: 1000, closeButton: true });
            $('#StrNumeroInterior').prop('disabled', false);
        }
    });

    $('#StrNumeroInterior').keyup(function () {
        let data = $(this).val();

        if (data == "") {
            
            $('#StrNumeroExterior').prop("disabled", true);
            //$('#StrNumeroExterior').val("");

        } else {

            $('#StrNumeroExterior').prop('disabled', false);
            $('#btnEnviar').prop('disabled', false);
        }
        
    });

    


})