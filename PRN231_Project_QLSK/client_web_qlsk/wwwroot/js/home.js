$(document).ready(function () {
    debugger
    var token = localStorage.getItem('jwt');
    var user = localStorage.getItem('username');
    console.log(token)
    console.log(user)
    $.ajax({
        url: 'http://localhost:5100/api/Event/getTop12',
        method: 'GET',
        data: { username: user },
        headers: {
            'Authorization': 'Bearer ' + token
        },
        success: function (response) {
            debugger
            var list = $('#listEvent');
            list.empty();
            
            if (response.length < 1) {
                var html = `<h4 class = "text-center">Không có sự kiên nào </h4>`;
                list.append(html);
            } else {
                response.forEach(function (event) {
                    var now = new Date();
                    var startTime = new Date(event.startTime);
                    var endTime = new Date(event.endTime);
                    var statusHtml = '';
                    if (now < startTime) {
                        statusHtml = '<div class="mb-2" style="color: gray"><i class="fa-solid fa-circle" style="color: gray"></i> Sắp diễn ra</div>';
                    }
                    if (now >= startTime && now <= endTime) {
                        statusHtml = '<div class="mb-2" style="color: green"><i class="fa-solid fa-circle" style="color: green"></i> Đang diễn ra</div>';
                    } 
                var html = `
                    <div class="col-12 col-md-4 col-xl-3">
                        <div class="course-box blog grid-blog">
                            <div class="blog-image mb-0">
                                <a href="blog-details.html"><img class="img-fluid" src="${event.image}" alt="Post Image"></a>
                            </div>
                            <div class="course-content">
                             <div class = "d-block">
                              <span class="date"> <i class="fa-solid fa-clock"></i> <strong> Thời gian bắt đầu : ${event.startTime}</strong> </span>
                               <span class="date"> <i class="fa-solid fa-location-dot"></i> <strong>Địa điểm : ${event.location}</strong></span>
                               
                               </div>
                                <a href="blog-details.html"> <span class="course-title">${event.title}</span></a>
                                ${statusHtml}
                                <div class="row">
                                    <div class="col">
                                        <a href="#" class="text-success">
                                            <i class="far fa-edit"></i> Sửa
                                        </a>
                                    </div>
                                    <div class="col text-end">
                                        <a href="javascript:void(0);" class="text-danger">
                                            <i class="far fa-trash-alt"></i> Xoá
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                `;
                list.append(html);
            });
            }
        },
        error: function (xhr, status, error) {
            debugger
            window.location.href = '/login';
            var response = JSON.parse(xhr.responseText);
        }
    });
});

