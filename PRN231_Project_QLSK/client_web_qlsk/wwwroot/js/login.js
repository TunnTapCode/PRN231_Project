function register() {
    debugger
    window.location.href = "/register"
}

function login() {
    window.location.href = "/login"
}

function submitLogin() {
    debugger
    var username = $('#username').val();
    var password = $('#password').val();
    debugger
    $.ajax({
        url: 'http://localhost:5100/api/Authentication/login',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({ Username: username, Password: password }),
        success: function (response) {
            debugger
            localStorage.setItem('jwt', response.token);
            localStorage.setItem('username', response.username);
            window.location.href = '/home';
        },
        error: function (xhr, status, error) {
            var response = JSON.parse(xhr.responseText);
            alert(response.message);
        }
    });
}


function setToken(token) {
    debugger
    const now = new Date();
    const item = {
        value: token,
        expiry: now.getTime() + (1 * 60 * 1000) // Thời gian hết hạn 1 phút sau khi tạo
    };
  
}