
function ValidarCorreo(correo) {
    var objRegExp = /^((([A-Za-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([A-Za-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([A-Za-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([A-Za-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([A-Za-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([A-Za-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([A-Za-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([A-Za-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([A-Za-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([A-Za-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/;
    return objRegExp.test(correo);
}

function ValidarCampos() {

            $(".ErrorIcon").addClass("hide");
            var dat = true;            
            var vNombre = document.getElementById('<%=txtNombre.ClientID%>').value;
            var vApellido = document.getElementById('<%=txtApellido.ClientID%>').value;
            var vSexo = document.getElementById('<%=cboSexo.ClientID%>').value;
            var vFecha = document.getElementById('<%=txtFechaNacimiento.ClientID%>').value;
            var vNroDocumento = document.getElementById('<%=txtNroDocumento.ClientID%>').value;
            var vEmail = document.getElementById('<%=txtEmail.ClientID%>').value;
            var vUsuario = document.getElementById('<%=txtUsuario.ClientID%>').value;
            var vClave = document.getElementById('<%=txtClave.ClientID%>').value;
            var vConfirmaClave = document.getElementById('<%=txtConfirmaClave.ClientID%>').value;
            var vTerminos = $('#<%=cbTerminos.ClientID%>').attr('checked');
            
            if (vNombre == "") {
                $("#ValNom").removeClass("hide");
                dat = false;
            }
            if (vApellido == ""){
                $("#ValApe").removeClass("hide");
                dat = false;
            }            
            if (vSexo == "-"){
                $("#ValSexo").removeClass("hide");
                dat = false;
            }
            if (vFecha == "")
                {$("#ValFecha").removeClass("hide");
                dat = false;
            }            
            if (vNroDocumento == ""){
                $("#ValNroDocumento").removeClass("hide");
                dat = false;
            }
            if (vEmail == ""){
                $("#ValEmail").removeClass("hide");
                dat = false;
            }
            
            if (!ValidarCorreo(vEmail)){
                $("#ValEmail").removeClass("hide");
                dat = false;
            }
            
            if (vUsuario == ""){
                $("#ValUsuario").removeClass("hide");
                dat = false;
            }
            if (vClave == ""){
                $("#ValClave").removeClass("hide");
                dat = false;
            }
            else
            {   if (vClave != vConfirmaClave) {
                    $("#ValConfirmaClave").removeClass("hide");
                    dat = false;
                }
            }
            
            if(dat){
                if(vTerminos == true){
                    document.getElementById("<%=btnRegistrar.ClientID%>").click();
                }
                else
                {
                    $("#ValTerminos").removeClass("hide");
                    dat = false;
                }
            
            }
    }


