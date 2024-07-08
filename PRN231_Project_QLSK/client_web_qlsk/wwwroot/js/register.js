function register() {
    debugger
    window.location.href = "/register"
}

function login() {
    window.location.href = "/login"
}
function checkpass() {
    var pass = $('#passCreate').val();
    var repass = $('#rePassCreate').val();
    debugger
    if (pass == repass) {
        return true;
    } else {
        return false;
    }
}
function submitRegiter() {
    debugger
    var username = $('#usernameCreate').val();
    var pass = $('#passCreate').val();
    var email = $('#emailCreate').val();
    debugger
    var userRegister = {
        Username: username,
        Password: pass,
        Email: email 
    }
    if (checkpass()) {
        $.ajax({
            url: 'http://localhost:5100/api/Authentication/register',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(userRegister ),
            success: function (res) {
                debugger
                    window.location.href = '/login';
                    alert(res.message);
            },
            error: function (xhr, status, error,res) {
                debugger
                var response = JSON.parse(xhr.responseText);
                alert(response.message);
            }
        });
    } else {
        alert('Mật khẩu không khớp.');
    }
   
}


function setToken(token) {
    debugger
    const now = new Date();
    const item = {
        value: token,
        expiry: now.getTime() + (60 * 60 * 1000) // Thời gian hết hạn 60 phút sau khi tạo
    };
    localStorage.setItem('jwt', JSON.stringify(item));
}


