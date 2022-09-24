var password = document.getElementById("password")
    , Confirmpassword = document.getElementById("Confirmpassword");

function validatePassword() {
    if (password.value != Confirmpassword.value) {
        Confirmpassword.setCustomValidity("Senhas diferentes!");
    } else {
        Confirmpassword.setCustomValidity('');
    }
}

password.onchange = validatePassword;
Confirmpassword.onkeyup = validatePassword;