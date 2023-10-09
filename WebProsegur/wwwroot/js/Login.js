
$(document).ready(function () {

   
        $(document).on('click', '.login_btn', function (e) {
            var objRequest =
            {
                name: $("#txtname").val(),
                password: $("#txtpassword").val()
            };

var data = objRequest;
            $.ajax({
                
                type: 'POST',
                url: 'https://localhost:7046/api/Usuarios/Login',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(data),
                crossDomain: true,
                dataType: "json",
                success: function (data) {

                    if (data.isSuccess) {
                        window.location = "/Home/Index?name=" + data.result.name + "&idrol=" + data.result.idRol;
                    }
                    else {
                        alert(data.message);
                    }

                },
                complete: function (e) {

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert("Ocurrió un error al Aprobar o Rechazar. Contacte con su administrador.")
                },
            });
        });

});